﻿using System;

namespace Emceelee.Import.DST
{
    public static class DateTimeUtility
    {
        //Requirements:
        //For a specified time/timezone must
        //1) Figure out Contract Month, Day, Hour
        //2) Convert to UTC
        //3) Configurably handle datetimes that have not yet been adjusted for DST

        public static DateTime ToUtc(DateTime importTime, TimeZoneInfo importTimeZone, bool correctedForDst = true)
        {
            if (importTime.Kind == DateTimeKind.Utc)
            {
                return importTime;
            }
            else if (importTime.Kind == DateTimeKind.Local)
            {
                //DateTime.Kind must be Unspecified to convert to a specified Timezone
                importTime = DateTime.SpecifyKind(importTime, DateTimeKind.Unspecified);
            }
            
            //If time provided hasn't already been adjusted for DST
            if(importTimeZone.SupportsDaylightSavingTime)
            {
                if (correctedForDst)
                {
                    if (importTimeZone.IsAmbiguousTime(importTime))
                    {
                        //Not enough information to fully resolve this case
                        //Following example assumes MST.  Base UTC offset (Non-DST) = -7

                        //Import Time   isDst?      Local Time      Utc Time
                        // 1:30 AM      True        1:30 AM (MDT)   7:30 AM
                        // 1:30 AM      False       1:30 AM (MST)   8:30 AM
                        // 2:30 AM      False       2:30 AM (MST)   9:30 AM

                        //TimeZoneInfo.ConvertTimeToUtc will always assume Standard (MST) time instead of DST time
                        //Results in a gap on the 1st hour instead of 2nd
                    }
                }
                else
                {
                    //Determine if the time provided needs to be corrected for DST
                    bool isDst = false;

                    //if invalid datetime (2:30am is valid when DST becomes active)
                    if (importTimeZone.IsInvalidTime(importTime))
                    {
                        isDst = true;
                    }
                    else
                    {
                        //Gracefully handles the case when DST becomes inactive, i.e. the ambiguous local time
                        //Ambiguous time -> isDst = false
                        //Following example assumes MST.  Base UTC offset (Non-DST) = -7

                        //Import Time   isDst?      Local Time      Utc Time
                        // 12:30 AM     True        1:30 AM (MDT)   7:30 AM
                        // 1:30 AM      False       1:30 AM (MST)   8:30 AM
                        // 2:30 AM      False       2:30 AM (MST)   9:30 AM
                        isDst = importTimeZone.IsDaylightSavingTime(importTime);
                    }

                    if (isDst)
                    {
                        importTime = importTime.AddHours(1);
                    }
                }
            }

            var utcTime = TimeZoneInfo.ConvertTimeToUtc(importTime, importTimeZone);

            return utcTime;
        }

        public static DateTime ToSpecified(DateTime utcTime, TimeZoneInfo specifiedTimeZone)
        {
            if (utcTime.Kind == DateTimeKind.Unspecified)
            {
                return utcTime;
            }
            else if (utcTime.Kind == DateTimeKind.Local)
            {
                throw new ArgumentException("Cannot convert from local to specified timezone.");
            }

            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, specifiedTimeZone)
                .SpecifyKind(DateTimeKind.Unspecified); //if specified timezone is UTC, want this to remain unspecified

            return localTime;
        }

        public static ContractDateTimeInfo ParseContractDateTimeInfo(DateTime localTime, int contractHour)
        {
            if (localTime.Kind == DateTimeKind.Utc)
            {
                throw new ArgumentException("ContractDateTimeInfo cannot be parsed from a UTC DateTime.");
            }
            else if (localTime.Kind == DateTimeKind.Local)
            {
                localTime = DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);
            }

            bool isPreviousDay = localTime.Hour < contractHour;
            DateTime contractDay = new DateTime(localTime.Year, localTime.Month, localTime.Day, 0, 0, 0, DateTimeKind.Utc)
                                        .AddDays(isPreviousDay ? -1 : 0);

            return new ContractDateTimeInfo()
            {
                ContractHour = localTime.Hour,
                ContractDay = contractDay,
                ContractMonth = new DateTime(contractDay.Year, contractDay.Month, 1, 0, 0, 0, DateTimeKind.Utc)
            };
        }

        public static ImportDateTimeInfo ParseImportDateTimeInfo(DateTime importTime, TimeZoneInfo importTimeZone, int contractHour, bool correctedForDst = true)
        {
            var utcTime = ToUtc(importTime, importTimeZone, correctedForDst);
            var localTime = ToSpecified(utcTime, importTimeZone);
            var contractDateTimeInfo = ParseContractDateTimeInfo(localTime, contractHour);

            return new ImportDateTimeInfo()
            {
                DateTimeUtc = utcTime,
                ContractDateTimeInfo = contractDateTimeInfo
            };
        }

    }
}
