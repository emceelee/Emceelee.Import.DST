using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Emceelee.Import.DST;

namespace Emceelee.Import.DST.Test
{
    [TestClass]
    public class DatetimeUtilityTest
    {
        private static DateTime dateTimeInvalidDST = new DateTime(2018, 3, 11, 2, 10, 50, 3, DateTimeKind.Unspecified);
        private static DateTime dateTimeAmbiguous = new DateTime(2018, 11, 4, 1, 10, 50, 3, DateTimeKind.Unspecified);
        private static DateTime dateTimeDST = new DateTime(2018, 10, 11, 12, 10, 50, 3, DateTimeKind.Unspecified);
        private static DateTime dateTimeNonDST = new DateTime(2018, 12, 11, 12, 10, 50, 3, DateTimeKind.Unspecified);
        private static DateTime dateTimeStartOfMonth = new DateTime(2018, 10, 1, 6, 10, 50, 3, DateTimeKind.Unspecified);

        private static TimeZoneInfo GetUtc()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("UTC"); ;
        }
        private static TimeZoneInfo GetMST()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time"); ;
        }
        private static TimeZoneInfo GetArizonaMST()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time"); ;
        }

        #region #ToUtc
        [TestMethod]
        public void ToUtc_FromUtc_DST()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: true
            //Currently DST? true
            //DST-Corrected Time: 12:10:50.003
            //Offset: 0
            //UTC: 12:10:50.003

            var localDateTime = dateTimeDST.SpecifyKind(DateTimeKind.Utc);
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(localDateTime.Year, result.Year);
            Assert.AreEqual(localDateTime.Month, result.Month);
            Assert.AreEqual(localDateTime.Day, result.Day);
            Assert.AreEqual(localDateTime.Hour, result.Hour);
            Assert.AreEqual(localDateTime.Minute, result.Minute);
            Assert.AreEqual(localDateTime.Second, result.Second);
            Assert.AreEqual(localDateTime.Millisecond, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromLocal_DST()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: true
            //Currently DST? true
            //DST-Corrected Time: 12:10:50.003
            //Offset: 6
            //UTC: 18:10:50.003

            var localDateTime = dateTimeDST.SpecifyKind(DateTimeKind.Local);
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(10, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(18, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_DST()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: true
            //Currently DST? true
            //DST-Corrected Time: 12:10:50.003
            //Offset: 6
            //UTC: 18:10:50.003

            var localDateTime = dateTimeDST;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(10, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(18, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_DST_Corrected_AmbiguousLocal()
        {
            //Specified Time: 1:10:50.003
            //Corrected for DST: true
            //Currently DST? true
            //DST-Corrected Time: 1:10:50.003
            //Offset: 6
            //UTC: 7:10:50.003

            var localDateTime = dateTimeAmbiguous;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(11, result.Month);
            Assert.AreEqual(4, result.Day);
            Assert.AreEqual(7, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUtc_NonDST()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: true
            //Currently DST? false
            //DST-Corrected Time: 12:10:50.003
            //Offset: 0
            //UTC: 12:10:50.003

            var localDateTime = dateTimeNonDST.SpecifyKind(DateTimeKind.Utc);
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(localDateTime.Year, result.Year);
            Assert.AreEqual(localDateTime.Month, result.Month);
            Assert.AreEqual(localDateTime.Day, result.Day);
            Assert.AreEqual(localDateTime.Hour, result.Hour);
            Assert.AreEqual(localDateTime.Minute, result.Minute);
            Assert.AreEqual(localDateTime.Second, result.Second);
            Assert.AreEqual(localDateTime.Millisecond, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromLocal_NonDST()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: true
            //Currently DST? false
            //DST-Corrected Time: 12:10:50.003
            //Offset: 7
            //UTC: 19:10:50.003

            var localDateTime = dateTimeNonDST.SpecifyKind(DateTimeKind.Local);
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(12, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(19, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_NonDST()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: true
            //Currently DST? false
            //DST-Corrected Time: 12:10:50.003
            //Offset: 7
            //UTC: 19:10:50.003

            var localDateTime = dateTimeNonDST;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(12, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(19, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_DST_NotCorrected()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: false
            //Currently DST? true
            //DST-Corrected Time: 13:10:50.003
            //Offset: 6
            //UTC: 19:10:50.003

            //EFM reports 12:00, but real local time is 13:00
            var localDateTime = dateTimeDST;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone, false);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(10, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(19, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_NonDST_NotCorrected()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: false
            //Currently DST? false
            //DST-Corrected Time: 12:10:50.003
            //Offset: 7
            //UTC: 19:10:50.003

            var localDateTime = dateTimeNonDST;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone, false);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(12, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(19, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_DST_NotCorrected_Arizona()
        {
            //Specified Time: 12:10:50.003
            //Corrected for DST: false
            //Currently DST? false
            //DST-Corrected Time: 12:10:50.003
            //Offset: 7
            //UTC: 19:10:50.003

            var localDateTime = dateTimeDST;
            //Arizona does not support DST
            var localTimeZone = GetArizonaMST();

            var result = localDateTime.ToUtc(localTimeZone, false);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(10, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(19, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_DST_NotCorrected_InvalidLocal()
        {
            //Specified Time: 2:10:50.003
            //Corrected for DST: false
            //Currently DST? true
            //DST-Corrected Time: 3:10:50.003
            //Offset: 6
            //UTC: 9:10:50.003

            var localDateTime = dateTimeInvalidDST;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone, false);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(3, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(9, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public void ToUtc_FromUnspecified_NonDST_NotCorrected_AmbiguousLocal()
        {
            //Specified Time: 1:10:50.003
            //Corrected for DST: false
            //Currently DST? false
            //DST-Corrected Time: 1:10:50.003
            //Offset: 7
            //UTC: 8:10:50.003

            var localDateTime = dateTimeAmbiguous;
            var localTimeZone = GetMST();

            var result = localDateTime.ToUtc(localTimeZone, false);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(11, result.Month);
            Assert.AreEqual(4, result.Day);
            Assert.AreEqual(8, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        #endregion

        #region ToSpecified
        [TestMethod]
        public void ToSpecified_FromUtc()
        {
            var utcTime = dateTimeDST.SpecifyKind(DateTimeKind.Utc);
            var localTimeZone = GetMST();

            var result = utcTime.ToSpecified(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(10, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(6, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Unspecified, result.Kind);
        }

        [TestMethod]
        public void ToSpecified_Utc_FromUtc()
        {
            var utcTime = dateTimeDST.SpecifyKind(DateTimeKind.Utc);
            var localTimeZone = GetUtc();

            var result = utcTime.ToSpecified(localTimeZone);

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(10, result.Month);
            Assert.AreEqual(11, result.Day);
            Assert.AreEqual(12, result.Hour);
            Assert.AreEqual(10, result.Minute);
            Assert.AreEqual(50, result.Second);
            Assert.AreEqual(3, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Unspecified, result.Kind);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToSpecified_FromLocal()
        {
            var utcTime = dateTimeDST.SpecifyKind(DateTimeKind.Local);
            var localTimeZone = GetMST();

            var result = utcTime.ToSpecified(localTimeZone);
        }

        [TestMethod]
        public void ToSpecified_FromUnspecified()
        {
            var utcTime = dateTimeDST;
            var localTimeZone = GetMST();

            var result = utcTime.ToSpecified(localTimeZone);

            Assert.AreEqual(utcTime.Year, result.Year);
            Assert.AreEqual(utcTime.Month, result.Month);
            Assert.AreEqual(utcTime.Day, result.Day);
            Assert.AreEqual(utcTime.Hour, result.Hour);
            Assert.AreEqual(utcTime.Minute, result.Minute);
            Assert.AreEqual(utcTime.Second, result.Second);
            Assert.AreEqual(utcTime.Millisecond, result.Millisecond);
            Assert.AreEqual(DateTimeKind.Unspecified, result.Kind);
        }
        #endregion

        #region ParseContractDateTimeInfo
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseContractDateTimeInfo_FromUtc()
        {
            var localDateTime = dateTimeDST.SpecifyKind(DateTimeKind.Utc);

            var contractDateTimeInfo = localDateTime.ParseContractDateTimeInfo(9);
        }

        [TestMethod]
        public void ParseContractDateTimeInfo_FromLocal()
        {
            var localDateTime = dateTimeDST.SpecifyKind(DateTimeKind.Local);

            var result = localDateTime.ParseContractDateTimeInfo(9);

            Assert.AreEqual(12, result.ContractHour);

            Assert.AreEqual(2018, result.ContractDay.Year);
            Assert.AreEqual(10, result.ContractDay.Month);
            Assert.AreEqual(11, result.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDay.Millisecond);

            Assert.AreEqual(DateTimeKind.Utc, result.ContractDay.Kind);
            Assert.AreEqual(2018, result.ContractMonth.Year);
            Assert.AreEqual(10, result.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseContractDateTimeInfo_FromUnspecified()
        {
            var localDateTime = dateTimeDST;

            var result = localDateTime.ParseContractDateTimeInfo(9);

            Assert.AreEqual(12, result.ContractHour);

            Assert.AreEqual(2018, result.ContractDay.Year);
            Assert.AreEqual(10, result.ContractDay.Month);
            Assert.AreEqual(11, result.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractMonth.Year);
            Assert.AreEqual(10, result.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseContractDateTimeInfo_PreviousDay()
        {
            var localDateTime = dateTimeDST;

            var result = localDateTime.ParseContractDateTimeInfo(13);

            Assert.AreEqual(12, result.ContractHour);

            Assert.AreEqual(2018, result.ContractDay.Year);
            Assert.AreEqual(10, result.ContractDay.Month);
            Assert.AreEqual(10, result.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractMonth.Year);
            Assert.AreEqual(10, result.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseContractDateTimeInfo_PreviousMonth()
        {
            var localDateTime = dateTimeStartOfMonth;

            var result = localDateTime.ParseContractDateTimeInfo(7);

            Assert.AreEqual(6, result.ContractHour);

            Assert.AreEqual(2018, result.ContractDay.Year);
            Assert.AreEqual(9, result.ContractDay.Month);
            Assert.AreEqual(30, result.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractMonth.Year);
            Assert.AreEqual(9, result.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractMonth.Kind);
        }
        #endregion

        #region ParseImportDateTimeInfo
        [TestMethod]
        public void ParseImportDateTimeInfo_Utc()
        {
            var importDateTime = dateTimeDST;
            var importTimezone = GetUtc();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 9);

            Assert.AreEqual(importDateTime.Year, result.DateTimeUtc.Year);
            Assert.AreEqual(importDateTime.Month, result.DateTimeUtc.Month);
            Assert.AreEqual(importDateTime.Day, result.DateTimeUtc.Day);
            Assert.AreEqual(importDateTime.Hour, result.DateTimeUtc.Hour);
            Assert.AreEqual(importDateTime.Minute, result.DateTimeUtc.Minute);
            Assert.AreEqual(importDateTime.Second, result.DateTimeUtc.Second);
            Assert.AreEqual(importDateTime.Millisecond, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(11, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseImportDateTimeInfo_Unspecified_NonDST()
        {
            var importDateTime = dateTimeNonDST;
            var importTimezone = GetMST();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 9);

            Assert.AreEqual(2018, result.DateTimeUtc.Year);
            Assert.AreEqual(12, result.DateTimeUtc.Month);
            Assert.AreEqual(11, result.DateTimeUtc.Day);
            Assert.AreEqual(19, result.DateTimeUtc.Hour);
            Assert.AreEqual(10, result.DateTimeUtc.Minute);
            Assert.AreEqual(50, result.DateTimeUtc.Second);
            Assert.AreEqual(3, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(11, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseImportDateTimeInfo_Unspecified_NonDST_PreviousDay()
        {
            var importDateTime = dateTimeNonDST;
            var importTimezone = GetMST();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 13);

            Assert.AreEqual(2018, result.DateTimeUtc.Year);
            Assert.AreEqual(12, result.DateTimeUtc.Month);
            Assert.AreEqual(11, result.DateTimeUtc.Day);
            Assert.AreEqual(19, result.DateTimeUtc.Hour);
            Assert.AreEqual(10, result.DateTimeUtc.Minute);
            Assert.AreEqual(50, result.DateTimeUtc.Second);
            Assert.AreEqual(3, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseImportDateTimeInfo_Unspecified_DST()
        {
            var importDateTime = dateTimeDST;
            var importTimezone = GetMST();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 9);

            Assert.AreEqual(2018, result.DateTimeUtc.Year);
            Assert.AreEqual(10, result.DateTimeUtc.Month);
            Assert.AreEqual(11, result.DateTimeUtc.Day);
            Assert.AreEqual(18, result.DateTimeUtc.Hour);
            Assert.AreEqual(10, result.DateTimeUtc.Minute);
            Assert.AreEqual(50, result.DateTimeUtc.Second);
            Assert.AreEqual(3, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(12, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(11, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseImportDateTimeInfo_Unspecified_DST_NonCorrected()
        {
            var importDateTime = dateTimeDST;
            var importTimezone = GetMST();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 9, false);

            Assert.AreEqual(2018, result.DateTimeUtc.Year);
            Assert.AreEqual(10, result.DateTimeUtc.Month);
            Assert.AreEqual(11, result.DateTimeUtc.Day);
            Assert.AreEqual(19, result.DateTimeUtc.Hour);
            Assert.AreEqual(10, result.DateTimeUtc.Minute);
            Assert.AreEqual(50, result.DateTimeUtc.Second);
            Assert.AreEqual(3, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(13, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(11, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseImportDateTimeInfo_Unspecified_DST_NonCorrected_Invalid()
        {
            var importDateTime = dateTimeInvalidDST;
            var importTimezone = GetMST();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 9, false);

            Assert.AreEqual(2018, result.DateTimeUtc.Year);
            Assert.AreEqual(3, result.DateTimeUtc.Month);
            Assert.AreEqual(11, result.DateTimeUtc.Day);
            Assert.AreEqual(9, result.DateTimeUtc.Hour);
            Assert.AreEqual(10, result.DateTimeUtc.Minute);
            Assert.AreEqual(50, result.DateTimeUtc.Second);
            Assert.AreEqual(3, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(3, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(3, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(10, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(3, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }

        [TestMethod]
        public void ParseImportDateTimeInfo_Unspecified_NonDST_NonCorrected_Ambiguous()
        {
            var importDateTime = dateTimeAmbiguous;
            var importTimezone = GetMST();

            var result = importDateTime.ParseImportDateTimeInfo(importTimezone, 9, false);

            Assert.AreEqual(2018, result.DateTimeUtc.Year);
            Assert.AreEqual(11, result.DateTimeUtc.Month);
            Assert.AreEqual(4, result.DateTimeUtc.Day);
            Assert.AreEqual(8, result.DateTimeUtc.Hour);
            Assert.AreEqual(10, result.DateTimeUtc.Minute);
            Assert.AreEqual(50, result.DateTimeUtc.Second);
            Assert.AreEqual(3, result.DateTimeUtc.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.DateTimeUtc.Kind);

            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractHour);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractDay.Year);
            Assert.AreEqual(11, result.ContractDateTimeInfo.ContractDay.Month);
            Assert.AreEqual(3, result.ContractDateTimeInfo.ContractDay.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractDay.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractDay.Kind);

            Assert.AreEqual(2018, result.ContractDateTimeInfo.ContractMonth.Year);
            Assert.AreEqual(11, result.ContractDateTimeInfo.ContractMonth.Month);
            Assert.AreEqual(1, result.ContractDateTimeInfo.ContractMonth.Day);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Hour);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Minute);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Second);
            Assert.AreEqual(0, result.ContractDateTimeInfo.ContractMonth.Millisecond);
            Assert.AreEqual(DateTimeKind.Utc, result.ContractDateTimeInfo.ContractMonth.Kind);
        }
        #endregion
    }
}
