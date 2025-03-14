
using System;
using System.Diagnostics;
using System.IO;

namespace UbiLauncher
{
    [Serializable]
    public class UbiProcess
    {
        public enum ProcessStatus
        {
            Stopped = 0,
            Running = 1
        }

        bool _use = true;
        public bool Use
        {
            get { return _use; }
            set { _use = value; }
        }

        string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        string _filepath = string.Empty;
        public string FilePath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        string _argstring = string.Empty;
        public string ArgString
        {
            get { return _argstring; }
            set { _argstring = value; }
        }

        string _guid = string.Empty;
        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }

        ProcessStatus _status = ProcessStatus.Stopped;
        public ProcessStatus Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    StatusChangedEvent();
                }
            }
        }

        [field: NonSerialized]
        Process _processhandle = null;
        public Process ProcessHandle
        {
            get { return _processhandle; }
            set { _processhandle = value; }
        }

        public UbiProcess()
        {
        }

        public UbiProcess(string title, string path, string arg, string guid)
        {
            Title = title;
            FilePath = path;
            ArgString = arg;
            GUID = guid;
        }

        private bool isRunning()
        {
            if (null != ProcessHandle)
            {
                try
                {
                    ProcessHandle.Refresh();
                    if (ProcessHandle.HasExited) return false;
                    else return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public ProcessStatus UpdateStatus()
        {
            Status = isRunning() ? ProcessStatus.Running : ProcessStatus.Stopped;
            return Status;
        }

        public bool Start()
        {
            if (isRunning())
            {
                return true;
            }
            else
            {
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.FileName = FilePath;
                    proc.StartInfo.Arguments = ArgString;
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(FilePath);

                    proc.Start();
                    ProcessHandle = proc;
                    Status = UbiProcess.ProcessStatus.Running;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public bool Stop()
        {
            if (!isRunning())
            {
                return true;
            }
            else
            {
                try
                {
                    if (null != ProcessHandle) ProcessHandle.Kill();
                    Status = ProcessStatus.Stopped;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        [field: NonSerialized]
        public event EventHandler RunningStatusChanged;
        public void StatusChangedEvent()
        {
            if (RunningStatusChanged != null)
            {
                RunningStatusChanged(this, new RunningStatusChangedEventArgs(this.GUID, this.Status));
            }
        }

        public string MakeLogString()
        {
            string line = string.Format("[{0}] {1} {2} {3}"
                
                ,Status.ToString()
                ,Title
                ,FilePath
                ,ArgString

                );

            return line;
        }

    }

    public class RunningStatusChangedEventArgs : EventArgs
    {
        public RunningStatusChangedEventArgs(string guid, UbiProcess.ProcessStatus status )
        {
            _guid = guid;
            _status = status;
        }

        string _guid;
        UbiProcess.ProcessStatus _status;

        public string GUID
        {
            get { return _guid; }
        }

        public UbiProcess.ProcessStatus RunningStatus
        {
            get { return _status; }
        }
    }


}
