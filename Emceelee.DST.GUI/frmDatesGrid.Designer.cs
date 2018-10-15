namespace Emceelee.DST.GUI
{
    partial class frmDatesGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkIsDST = new System.Windows.Forms.CheckBox();
            this.cboTimezones = new System.Windows.Forms.ComboBox();
            this.rbSpring = new System.Windows.Forms.RadioButton();
            this.rbFall = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblContractHour = new System.Windows.Forms.Label();
            this.nudContractHour = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContractHour)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1196, 595);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // chkIsDST
            // 
            this.chkIsDST.AutoSize = true;
            this.chkIsDST.Location = new System.Drawing.Point(308, 10);
            this.chkIsDST.Name = "chkIsDST";
            this.chkIsDST.Size = new System.Drawing.Size(170, 17);
            this.chkIsDST.TabIndex = 2;
            this.chkIsDST.Text = "Source Data Adjusted for DST";
            this.chkIsDST.UseVisualStyleBackColor = true;
            this.chkIsDST.CheckedChanged += new System.EventHandler(this.chkIsDST_CheckedChanged);
            // 
            // cboTimezones
            // 
            this.cboTimezones.FormattingEnabled = true;
            this.cboTimezones.Location = new System.Drawing.Point(12, 8);
            this.cboTimezones.Name = "cboTimezones";
            this.cboTimezones.Size = new System.Drawing.Size(290, 21);
            this.cboTimezones.TabIndex = 3;
            this.cboTimezones.SelectedIndexChanged += new System.EventHandler(this.cboTimezones_SelectedIndexChanged);
            // 
            // rbSpring
            // 
            this.rbSpring.AutoSize = true;
            this.rbSpring.Checked = true;
            this.rbSpring.Location = new System.Drawing.Point(516, 8);
            this.rbSpring.Name = "rbSpring";
            this.rbSpring.Size = new System.Drawing.Size(96, 17);
            this.rbSpring.TabIndex = 4;
            this.rbSpring.TabStop = true;
            this.rbSpring.Text = "Spring Forward";
            this.rbSpring.UseVisualStyleBackColor = true;
            this.rbSpring.CheckedChanged += new System.EventHandler(this.rbSpring_CheckedChanged);
            // 
            // rbFall
            // 
            this.rbFall.AutoSize = true;
            this.rbFall.Location = new System.Drawing.Point(619, 8);
            this.rbFall.Name = "rbFall";
            this.rbFall.Size = new System.Drawing.Size(69, 17);
            this.rbFall.TabIndex = 5;
            this.rbFall.TabStop = true;
            this.rbFall.Text = "Fall Back";
            this.rbFall.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblContractHour);
            this.panel1.Controls.Add(this.nudContractHour);
            this.panel1.Controls.Add(this.chkIsDST);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1196, 35);
            this.panel1.TabIndex = 6;
            // 
            // lblContractHour
            // 
            this.lblContractHour.AutoSize = true;
            this.lblContractHour.Location = new System.Drawing.Point(721, 11);
            this.lblContractHour.Name = "lblContractHour";
            this.lblContractHour.Size = new System.Drawing.Size(73, 13);
            this.lblContractHour.TabIndex = 4;
            this.lblContractHour.Text = "Contract Hour";
            // 
            // nudContractHour
            // 
            this.nudContractHour.Location = new System.Drawing.Point(800, 8);
            this.nudContractHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudContractHour.Name = "nudContractHour";
            this.nudContractHour.Size = new System.Drawing.Size(52, 20);
            this.nudContractHour.TabIndex = 3;
            this.nudContractHour.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudContractHour.ValueChanged += new System.EventHandler(this.nudContractHour_ValueChanged);
            // 
            // frmDatesGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 630);
            this.Controls.Add(this.rbFall);
            this.Controls.Add(this.rbSpring);
            this.Controls.Add(this.cboTimezones);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "frmDatesGrid";
            this.Text = "DST Example";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContractHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkIsDST;
        private System.Windows.Forms.ComboBox cboTimezones;
        private System.Windows.Forms.RadioButton rbSpring;
        private System.Windows.Forms.RadioButton rbFall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudContractHour;
        private System.Windows.Forms.Label lblContractHour;
    }
}

