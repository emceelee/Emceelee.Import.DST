using System;
using System.Collections.Generic;
using System.Text;

namespace Emceelee.Import.DST
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime importTime, TimeZoneInfo importTimeZone, bool correctedForDst = true)
        {
            return DateTimeUtility.ToUtc(importTime, importTimeZone, correctedForDst);
        }
        public static DateTime ToSpecified(this DateTime utcTime, TimeZoneInfo specifiedTimeZone)
        {
            return DateTimeUtility.ToSpecified(utcTime, specifiedTimeZone);
        }
        public static ContractDateTimeInfo ParseContractDateTimeInfo(this DateTime localTime, int contractHour)
        {
            return DateTimeUtility.ParseContractDateTimeInfo(localTime, contractHour);
        }
        public static ImportDateTimeInfo ParseImportDateTimeInfo(this DateTime importTime, TimeZoneInfo importTimeZone, int contractHour, bool correctedForDst = true)
        {
            return DateTimeUtility.ParseImportDateTimeInfo(importTime, importTimeZone, contractHour, correctedForDst);
        }

        public static DateTime SpecifyKind(this DateTime dateTime, DateTimeKind kind)
        {
            return DateTime.SpecifyKind(dateTime, kind);
        }
    }
}
