using System;
using System.Threading;
using System.Windows.Forms;

namespace UbiLauncher
{
    public partial class MainForm : Form
    {
        enum ProgramRunningMode
        {
            Production = 0,
            Manage = 1
        }

        UbiProcessList _processes = new UbiProcessList();
        bool SaveNeeded = false;

        Thread RunningStatusCheckThread;
        bool ThreadRunningFlag = true;

        ProgramRunningMode RunningMode;

        public MainForm(string[] args)
        {
            InitializeComponent();

            // 시작할 때, 설정 파일 읽음
            LoadSettings();


            // 종료할 때, 설정 저장하려고...
            FormClosing += MainForm_FormClosing;


            // 버튼 초기화
            btnStartAll.Enabled = _processes.ProcessList.Count > 0;
            btnStopAll.Enabled = false;

            // 이렇게 안 해 주면, row 만들었을 때, 버튼 Text가 설정이 안됨
            dgv.Columns[(int)GlobalData.DataColumnIndex.Start].DefaultCellStyle.NullValue = GlobalData.ColumnTitles[(int)GlobalData.DataColumnIndex.Start];
            dgv.Columns[(int)GlobalData.DataColumnIndex.Stop].DefaultCellStyle.NullValue = GlobalData.ColumnTitles[(int)GlobalData.DataColumnIndex.Stop];


            //
            dgv.RowsAdded += dgv_RowsAdded;

            // Path 더블클릭 시 파일 브라우징 하게...
            dgv.CellDoubleClick += dgv_CellDoubleClick;

            // Start/Stop 버튼 클릭 처리를 위해...
            dgv.CellClick += dgv_CellClick;

            // Cell Change 시 SaveNeeded 처리를 위해
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;


            // decide program running mode, default mode = PRODUCTION,
            // mode = MANAGE When NO SETTINGS AVALIABLE...
            DecideMainProgramRunningMode(args);

            // Start Program Running Status Check Thread
            StartProgramRunningStatusCheckThread();
        }



        #region [Decide Main Program Running Mode]

        private void DecideMainProgramRunningMode(string[] args)
        {
            RunningMode = ProgramRunningMode.Production;

            if (args.Length > 0)
            {
                foreach (string mode in args)
                {
                    if (ProgramRunningMode.Production.ToString().ToUpper() == mode.ToUpper())
                    {
                        RunningMode = ProgramRunningMode.Production;
                        break;
                    }
                    else if (ProgramRunningMode.Manage.ToString().ToUpper() == mode.ToUpper())
                    {
                        RunningMode = ProgramRunningMode.Manage;
                        break;
                    }
                }
            }

            // settings file 이 없거나, 설정이 아무것도 없으면, Manage 모드로 감...
            if (_processes.ProcessList.Count <= 0)
            {
                RunningMode = ProgramRunningMode.Manage;
            }

            labelRunningMode.Text += RunningMode.ToString();
        }

        #endregion



        #region [Running Check Thread]

        private void StartProgramRunningStatusCheckThread()
        {
            RunningStatusCheckThread = new Thread(new ThreadStart(ProgramRunningStatusCheckThreadFunc));
            ThreadRunningFlag = true;
            RunningStatusCheckThread.Start();

            // leave log
            GlobalData.WriteLog("RunningStatusCheckThread.Start()");
        }

        private void ProgramRunningStatusCheckThreadFunc()
        {
            while (ThreadRunningFlag)
            {
                foreach (UbiProcess p in _processes.ProcessList)
                {
                    p.UpdateStatus();

                    if (p.Use && p.Status == UbiProcess.ProcessStatus.Stopped)
                    {
                        if (RunningMode == ProgramRunningMode.Production)
                            p.Start();
                    }
                }

                Thread.Sleep(GlobalData.RunningStatusCheckThread_CheckingInterval);
            }

            GlobalData.WriteLog("RunningStatusCheckThread Ends...");
        }

        #endregion



        #region [Process Running Status Changed Event]

        void proc_RunningStatusChanged(object sender, EventArgs e)
        {
            RunningStatusChangedEventArgs arg = e as RunningStatusChangedEventArgs;
            if (null != arg)
            {
                try
                {
                    // get row index by GUID
                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        if (dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value.ToString() == arg.GUID)
                        {
                            dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Status].Value = arg.RunningStatus.ToString();
                            dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Status].Style.BackColor = GlobalData.StatusColor[(int)arg.RunningStatus];

                            // leave a log line
                            UbiProcess proc = sender as UbiProcess;
                            GlobalData.WriteLog(proc.MakeLogString());

                            // no further loop
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Dealing with Cross-Thread
            btnStartAll.Invoke(new Action(() => btnStartAll.Enabled = _processes.StoppedProcessesCount > 0));
            btnStopAll.Invoke(new Action(() => btnStopAll.Enabled = _processes.RunningProcessesCount > 0));

        }

        #endregion



        #region [Form Events]

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            // confirm to exit
            if (_processes.ProcessList.Count > 0)
            {
                if (MessageBox.Show("This Will Close All Programs", GlobalData.SettingsFilePath, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (SaveNeeded)
            {
                switch (MessageBox.Show("Save Settings?", GlobalData.SettingsFilePath, MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        SaveSettings();
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        break;

                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }

            ThreadRunningFlag = false;
            RunningStatusCheckThread.Join();

            // terminate all the processes
            foreach (UbiProcess p in _processes.ProcessList)
            {
                p.Stop();
            }

            GlobalData.WriteLog("MainForm_FormClosing()");

        }

        void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells[GlobalData.DataColumnIndex.Use.ToString()].Value = true;
        }

        void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < (int)GlobalData.DataColumnIndex.Status)
            {
                // get its guid
                string guid = dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.GUID].Value == null ?
                        string.Empty :
                        dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.GUID].Value.ToString();

                UbiProcess proc = _processes.GetByGUID(guid);
                if (null != proc)
                {
                    proc.Use = (bool)dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Use].Value;

                    proc.Title = dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Title].Value == null ?
                        string.Empty :
                        dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Title].Value.ToString();

                    proc.FilePath = dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Path].Value == null ?
                        string.Empty :
                        dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Path].Value.ToString();

                    proc.ArgString = dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Args].Value == null ?
                        string.Empty :
                        dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Args].Value.ToString();
                }

                SaveNeeded = true;
            }

        }

        #endregion



        #region [Program Start/Stop Row Button]
        void StartFromGrid(int row)
        {
            // ignore when header row clicked.
            if (row < 0) return;

            // get its guid
            string guid = dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value == null ?
                    string.Empty :
                    dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value.ToString();

            UbiProcess proc = _processes.GetByGUID(guid);
            if (null != proc)
            {
                proc.Title = dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Title].Value == null ?
                    string.Empty :
                    dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Title].Value.ToString();

                proc.FilePath = dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Path].Value == null ?
                    string.Empty :
                    dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Path].Value.ToString();

                proc.ArgString = dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Args].Value == null ?
                    string.Empty :
                    dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Args].Value.ToString();
                
                proc.Start();
            }
        }

        void StopFromGrid(int row)
        {
            // get its guid
            string guid = dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value == null ?
                    string.Empty :
                    dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value.ToString();

            UbiProcess proc = _processes.GetByGUID(guid);

            if (null != proc)
            {
                proc.Stop();
            }
        }

        void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            switch (e.ColumnIndex)
            {
                case (int)GlobalData.DataColumnIndex.Start: // Start Button
                    StartFromGrid(e.RowIndex);
                    dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Stop].ReadOnly = false; // enable Stop Button
                    break;

                case (int)GlobalData.DataColumnIndex.Stop: // Stop Button
                    StopFromGrid(e.RowIndex);
                    dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Start].ReadOnly = false; // enable Start Button
                    break;
            }
        }
        #endregion



        #region [Program File Browsing]
        void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)GlobalData.DataColumnIndex.Path )
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = GlobalData.FileDialogFilter_EXE;
                dlg.Title = "Select Program File";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dlg.FileName;
                    dgv.Rows[e.RowIndex].Cells[(int)GlobalData.DataColumnIndex.Start].ReadOnly = false; // enable Start Button
                    btnStartAll.Enabled = true;
                }
            }
        }
        #endregion



        #region [Add/Delete Row]
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            int row = dgv.Rows.Add();
            string guid = Guid.NewGuid().ToString();
            UbiProcess proc = new UbiProcess(string.Empty, string.Empty, string.Empty, guid);
            if (null != proc)
            {
                _processes.ProcessList.Add(proc);
                proc.RunningStatusChanged += proc_RunningStatusChanged;

                // guid is given to the process then set to column
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value = guid;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                int row = dgv.SelectedRows[0].Index;
                if(row>=0)
                {
                    // get the process's GUID
                    string guid = dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value.ToString();

                    // stop the process
                    _processes.GetByGUID(guid).Stop();

                    // remove the process from the list
                    _processes.ProcessList.Remove(_processes.GetByGUID(guid));

                    // remove the row
                    dgv.Rows.RemoveAt(row);

                    // set flag
                    SaveNeeded = true;
                }
            }
            else
            {
                MessageBox.Show("Please Select A Row To Delete");
            }
        }
        #endregion



        #region [Start All / Stop All Buttons]
        private void btnStartAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs((int)GlobalData.DataColumnIndex.Start, row);
                dgv_CellClick(dgv, arg);
            }
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs((int)GlobalData.DataColumnIndex.Stop, row);
                dgv_CellClick(dgv, arg);
            }
        }
        #endregion



        #region [Serialize]

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        void SaveSettings()
        {
            SaveNeeded = !(_processes.SaveData(GlobalData.SettingsFilePath));
        }

        void LoadSettings()
        {
            GlobalData.WriteLog("LoadSettings()");

            _processes.ReadData(GlobalData.SettingsFilePath);

            // Set data into DataGridView
            foreach (UbiProcess proc in _processes.ProcessList)
            {
                int row = dgv.Rows.Add();
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Use].Value = proc.Use;
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Title].Value = proc.Title;
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Path].Value = proc.FilePath;
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Args].Value = proc.ArgString;

                // processes are initially stopped states
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Status].Value = proc.Status.ToString();
                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.Status].Style.BackColor = GlobalData.StatusColor[(int)UbiProcess.ProcessStatus.Stopped];

                dgv.Rows[row].Cells[(int)GlobalData.DataColumnIndex.GUID].Value = proc.GUID;

                proc.RunningStatusChanged += proc_RunningStatusChanged;
            }

            btnStartAll.Enabled = dgv.Rows.Count > 0;

            GlobalData.WriteLog(string.Format("{0} Settings Loaded", dgv.Rows.Count));
        }

        #endregion

    }
}
