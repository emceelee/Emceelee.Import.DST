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
        private DateSpan SpringForwardSpan = new DateSpan()
        {
            Start = new DateTime(2018, 3, 11, 0, 0, 0, DateTimeKind.Unspecified),
            End = new DateTime(2018, 3, 12, 0, 0, 0, DateTimeKind.Unspecified)
        };
        private DateSpan FallBackSpan = new DateSpan()
        {
            Start = new DateTime(2018, 11, 4, 0, 0, 0, DateTimeKind.Unspecified),
            End = new DateTime(2018, 11, 5, 0, 0, 0, DateTimeKind.Unspecified)
        };
        private DateTime SpringForwardHour = new DateTime(2018, 3, 11, 2, 0, 0, DateTimeKind.Unspecified);
        private DateTime FallBackHour = new DateTime(2018, 11, 4, 2, 0, 0, DateTimeKind.Unspecified);

        private bool IsSpringForward { get { return rbSpring.Checked; } }
        private bool IsSourceDataAdjustedDST
        {
            get
            {
                return SelectedTimezone.SupportsDaylightSavingTime && chkIsDST.Checked;
            }
        }
        private TimeZoneInfo SelectedTimezone { get { return (TimeZoneInfo)cboTimezones.SelectedValue; } }
        private int ContractHour { get { return (int) nudContractHour.Value; } }

        private List<EntityExample> Data { get; set; }

        public frmDatesGrid()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            cboTimezones.DataSource = TimeZoneInfo.GetSystemTimeZones();
            cboTimezones.SelectedIndex = cboTimezones.Items.IndexOf(TimeZoneInfo.Local);
            SetDSTEnabled();

            RefreshData();
            dataGridView1.Columns["RowColor"].Visible = false;
        }

        private void SetDSTEnabled()
        {
            chkIsDST.Enabled = ((TimeZoneInfo)cboTimezones.SelectedValue).SupportsDaylightSavingTime;
        }

        private void RefreshData()
        {
            Data = GetData();
            dataGridView1.DataSource = Data;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void DetermineContractData()
        {
            foreach(var entity in Data)
            {
                var contractData = entity.DisplayEffectiveDateStart.ParseContractDateTimeInfo(ContractHour);
                entity.ContractHour = contractData.ContractHour;
                entity.ContractDay = contractData.ContractDay;
                entity.ContractMonth = contractData.ContractMonth;
            }
        }

        private List<EntityExample> GetData()
        {
            var tz = SelectedTimezone;

            var result = new List<EntityExample>();

            DateSpan span = IsSpringForward ? SpringForwardSpan : FallBackSpan;
            DateTime hour = IsSpringForward ? SpringForwardHour : FallBackHour;

            result.AddRange(GenerateEntities(span, tz, hour));
            
            return result;
        }

        private List<EntityExample> GenerateEntities(DateSpan span, TimeZoneInfo tz, DateTime dstHour)
        {
            var adjusted = false;
            Color nextColor = Color.White;

            var result = new List<EntityExample>();

            DateTime start = span.Start;
            DateTime end = span.End;

            while (start < end)
            {
                var entity = new EntityExample()
                {
                    ImportEffectiveDateStart = start,
                    ImportEffectiveDateEnd = start.AddHours(1),
                    RowColor = nextColor
                };

                nextColor = Color.White;

                start = start.AddHours(1);

                if (IsSourceDataAdjustedDST)
                {
                    if (entity.ImportEffectiveDateEnd == dstHour)
                    {
                        if (IsSpringForward)
                        {
                            entity.RowColor = Color.Yellow;
                            entity.ImportEffectiveDateEnd = entity.ImportEffectiveDateEnd.AddHours(1);
                        }
                        else
                        {
                            if(!adjusted)
                            {
                                entity.ImportEffectiveDateEnd = entity.ImportEffectiveDateEnd.AddHours(-1);
                                start = start.AddHours(-1); //repeat the hour
                                adjusted = true;
                                entity.RowColor = Color.Yellow;
                                nextColor = Color.Red;
                            }
                        }
                    }

                    if (entity.ImportEffectiveDateEnd == dstHour.AddHours(-1))
                    {
                        if (!IsSpringForward)
                        {
                            if (!adjusted)
                            {
                                entity.RowColor = Color.Yellow;
                            }
                        }
                    }

                    if (entity.ImportEffectiveDateStart == dstHour)
                    {
                        if (IsSpringForward)
                        {
                            nextColor = Color.Yellow;
                            continue; //skip the hour
                        }
                    }
                }
                else
                {
                    if (IsSpringForward)
                    {
                        if (entity.ImportEffectiveDateStart == dstHour)
                        {
                            entity.RowColor = Color.Yellow;
                        }
                    }
                    else
                    {
                        if (entity.ImportEffectiveDateEnd == dstHour)
                        {
                            entity.RowColor = Color.Yellow;
                        }
                    }
                }
                
                var importDateTimeInfo = entity.ImportEffectiveDateStart.ParseImportDateTimeInfo(tz, ContractHour, IsSourceDataAdjustedDST);

                entity.UtcEffectiveDateStart = importDateTimeInfo.DateTimeUtc;
                entity.UtcEffectiveDateEnd = entity.ImportEffectiveDateEnd.ToUtc(tz, IsSourceDataAdjustedDST);
                entity.ContractHour = importDateTimeInfo.ContractDateTimeInfo.ContractHour;
                entity.ContractDay = importDateTimeInfo.ContractDateTimeInfo.ContractDay;
                entity.ContractMonth = importDateTimeInfo.ContractDateTimeInfo.ContractMonth;
                entity.DisplayEffectiveDateStart = entity.UtcEffectiveDateStart.ToSpecified(tz);
                entity.DisplayEffectiveDateEnd = entity.UtcEffectiveDateEnd.ToSpecified(tz);

                result.Add(entity);
            }

            return result;
        }

        public class EntityExample
        {
            public DateTime ImportEffectiveDateStart { get; set; }
            public DateTime ImportEffectiveDateEnd { get; set; }
            public DateTime UtcEffectiveDateStart { get; set; }
            public DateTime UtcEffectiveDateEnd { get; set; }
            public DateTime DisplayEffectiveDateStart { get; set; }
            public DateTime DisplayEffectiveDateEnd { get; set; }
            public int ContractHour { get; set; }
            public DateTime ContractDay { get; set; }
            public DateTime ContractMonth { get; set; }
            public Color RowColor { get; set; } = Color.Yellow;
        }

        public class DateSpan
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        private void rbSpring_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void cboTimezones_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
            SetDSTEnabled();
        }

        private void chkIsDST_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void nudContractHour_ValueChanged(object sender, EventArgs e)
        {
            DetermineContractData();
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            EntityExample entity = dgv.Rows[e.RowIndex].DataBoundItem as EntityExample;

            e.CellStyle.BackColor = entity.RowColor;
        }
    }
}
