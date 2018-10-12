using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emceelee.Import.DST;

namespace Emceelee.DST.GUI
{
    public partial class frmDatesGrid : Form
    {
        public frmDatesGrid()
        {
            InitializeComponent();

            var data = GetData();
            dataGridView1.DataSource = data;
        }

        public List<EntityExample> GetData()
        {
            var result = new List<EntityExample>();

            DateTime start = new DateTime(2018, 3, 10, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime end = new DateTime(2018, 3, 12, 0, 0, 0, DateTimeKind.Unspecified);

            var tz = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");

            result.AddRange(GenerateEntities(start, end, tz));

            start = new DateTime(2018, 11, 3, 0, 0, 0, DateTimeKind.Unspecified);
            end = new DateTime(2018, 11, 5, 0, 0, 0, DateTimeKind.Unspecified);

            result.AddRange(GenerateEntities(start, end, tz));

            foreach (var entity in result)
            {
                var contractHour = 7;
                var tzi = entity.GetTimeZoneInfo();
                var importDateTimeInfo = entity.ImportEffectiveDateStart.ParseImportDateTimeInfo(tzi, contractHour, false);

                entity.UtcEffectiveDateStart = importDateTimeInfo.DateTimeUtc;
                entity.UtcEffectiveDateEnd = entity.ImportEffectiveDateEnd.ToUtc(tzi, false);
                entity.ContractHour = importDateTimeInfo.ContractDateTimeInfo.ContractHour;
                entity.ContractDay = importDateTimeInfo.ContractDateTimeInfo.ContractDay;
                entity.ContractMonth = importDateTimeInfo.ContractDateTimeInfo.ContractMonth;
                entity.DisplayEffectiveDateStart = entity.UtcEffectiveDateStart.ToSpecified(tzi);
                entity.DisplayEffectiveDateEnd = entity.UtcEffectiveDateEnd.ToSpecified(tzi);
            }

            return result;
        }

        public List<EntityExample> GenerateEntities(DateTime start, DateTime end, TimeZoneInfo tz)
        {
            var result = new List<EntityExample>();

            while (start < end)
            {
                result.Add(new EntityExample(tz)
                {
                    ImportEffectiveDateStart = start,
                    ImportEffectiveDateEnd = start.AddHours(1)
                });

                start = start.AddHours(1);
            }

            return result;
        }

        public class EntityExample
        {
            private TimeZoneInfo _tz;
            public TimeZoneInfo GetTimeZoneInfo() { return _tz; }
            
            public EntityExample(TimeZoneInfo tz) { _tz = tz; }

            public DateTime ImportEffectiveDateStart { get; set; }
            public DateTime ImportEffectiveDateEnd { get; set; }
            public DateTime UtcEffectiveDateStart { get; set; }
            public DateTime UtcEffectiveDateEnd { get; set; }
            public DateTime DisplayEffectiveDateStart { get; set; }
            public DateTime DisplayEffectiveDateEnd { get; set; }
            public int ContractHour { get; set; }
            public DateTime ContractDay { get; set; }
            public DateTime ContractMonth { get; set; }
            public string TimeZone { get { return _tz.DisplayName; } }
        }
    }
}
