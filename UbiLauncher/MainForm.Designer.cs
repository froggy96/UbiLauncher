namespace UbiLauncher
{
    partial class MainForm
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Use = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.program_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.program_path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.program_args = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.program_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_start = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_stop = new System.Windows.Forms.DataGridViewButtonColumn();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStopAll = new System.Windows.Forms.Button();
            this.btnStartAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.labelRunningMode = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Use,
            this.program_name,
            this.program_path,
            this.program_args,
            this.program_status,
            this.btn_start,
            this.btn_stop,
            this.GUID});
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgv.Location = new System.Drawing.Point(12, 114);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 24;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(680, 476);
            this.dgv.TabIndex = 0;
            // 
            // Use
            // 
            this.Use.FillWeight = 15F;
            this.Use.HeaderText = "Use";
            this.Use.Name = "Use";
            this.Use.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // program_name
            // 
            this.program_name.FillWeight = 40F;
            this.program_name.HeaderText = "Title";
            this.program_name.MaxInputLength = 128;
            this.program_name.Name = "program_name";
            // 
            // program_path
            // 
            this.program_path.HeaderText = "Path";
            this.program_path.MaxInputLength = 512;
            this.program_path.Name = "program_path";
            this.program_path.ReadOnly = true;
            this.program_path.ToolTipText = "Double Click To Browse";
            // 
            // program_args
            // 
            this.program_args.FillWeight = 20F;
            this.program_args.HeaderText = "Args";
            this.program_args.MaxInputLength = 128;
            this.program_args.Name = "program_args";
            // 
            // program_status
            // 
            this.program_status.FillWeight = 20F;
            this.program_status.HeaderText = "Status";
            this.program_status.Name = "program_status";
            // 
            // btn_start
            // 
            this.btn_start.FillWeight = 20F;
            this.btn_start.HeaderText = "Start";
            this.btn_start.Name = "btn_start";
            this.btn_start.ReadOnly = true;
            this.btn_start.Text = "Start";
            // 
            // btn_stop
            // 
            this.btn_stop.FillWeight = 20F;
            this.btn_stop.HeaderText = "Stop";
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.ReadOnly = true;
            this.btn_stop.Text = "Stop";
            // 
            // GUID
            // 
            this.GUID.HeaderText = "GUID";
            this.GUID.MaxInputLength = 1024;
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            this.GUID.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnStopAll);
            this.groupBox1.Controls.Add(this.btnStartAll);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAddRow);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 42);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnStopAll
            // 
            this.btnStopAll.BackColor = System.Drawing.Color.Salmon;
            this.btnStopAll.Location = new System.Drawing.Point(321, 13);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(75, 23);
            this.btnStopAll.TabIndex = 4;
            this.btnStopAll.Text = "Stop All";
            this.btnStopAll.UseVisualStyleBackColor = false;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // btnStartAll
            // 
            this.btnStartAll.BackColor = System.Drawing.Color.LimeGreen;
            this.btnStartAll.Location = new System.Drawing.Point(240, 13);
            this.btnStartAll.Name = "btnStartAll";
            this.btnStartAll.Size = new System.Drawing.Size(75, 23);
            this.btnStartAll.TabIndex = 3;
            this.btnStartAll.Text = "Start All";
            this.btnStartAll.UseVisualStyleBackColor = false;
            this.btnStartAll.Click += new System.EventHandler(this.btnStartAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Double Click \"Path\" Cells To Browse Files";
            // 
            // btnAddRow
            // 
            this.btnAddRow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddRow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnAddRow.Location = new System.Drawing.Point(436, 13);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(63, 23);
            this.btnAddRow.TabIndex = 1;
            this.btnAddRow.Text = "Add";
            this.btnAddRow.UseVisualStyleBackColor = false;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDelete.BackColor = System.Drawing.Color.Orchid;
            this.btnDelete.Location = new System.Drawing.Point(505, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // labelRunningMode
            // 
            this.labelRunningMode.AutoSize = true;
            this.labelRunningMode.Font = new System.Drawing.Font("Segoe UI Emoji", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRunningMode.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelRunningMode.Location = new System.Drawing.Point(12, 9);
            this.labelRunningMode.Name = "labelRunningMode";
            this.labelRunningMode.Size = new System.Drawing.Size(135, 43);
            this.labelRunningMode.TabIndex = 2;
            this.labelRunningMode.Text = "Mode : ";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.BackColor = System.Drawing.Color.Orange;
            this.btnSave.Location = new System.Drawing.Point(575, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 601);
            this.Controls.Add(this.labelRunningMode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Process Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStopAll;
        private System.Windows.Forms.Button btnStartAll;
        private System.Windows.Forms.Label labelRunningMode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Use;
        private System.Windows.Forms.DataGridViewTextBoxColumn program_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn program_path;
        private System.Windows.Forms.DataGridViewTextBoxColumn program_args;
        private System.Windows.Forms.DataGridViewTextBoxColumn program_status;
        private System.Windows.Forms.DataGridViewButtonColumn btn_start;
        private System.Windows.Forms.DataGridViewButtonColumn btn_stop;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.Button btnSave;
    }
}

