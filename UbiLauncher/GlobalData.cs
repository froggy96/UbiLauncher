
namespace UbiLauncher
{
    public static class GlobalData
    {
        // file browse dialog setting
        public static string FileDialogFilter_EXE = "Execution File|*.exe|All Files|*.*";


        // main grid column def.
        public static string[] ColumnNames = { "Use", "program_name", "program_path", "program_args", "program_status", "btn_start", "btn_stop", "GUID" };
        public static string[] ColumnTitles = { "Use", "Title", "Path", "Args", "Status", "Start", "Stop", "GUID" };

        public enum DataColumnIndex
        {
            Use = 0,
            Title = 1,
            Path = 2,
            Args = 3,
            Status = 4,
            Start = 5,
            Stop = 6,
            GUID = 7
        }

        // settings save file path
        public static string SettingsFilePath = @"program.launch.settings";

        // log file path
        public static string LogFilePath = @"program.launch.log";



        // process running status thread : check interval (mili-sec)
        public static int RunningStatusCheckThread_CheckingInterval = 1000;

        // process running status color
        public static System.Drawing.Color[] StatusColor = { System.Drawing.Color.Orange, System.Drawing.Color.LightBlue };

        // log writing func
        public static void WriteLog(string log)
        {
            string line = string.Format("[{0}] {1}",

                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                , log

                );

            try
            {
                if (_b_logfile_enabled)
                    System.IO.File.AppendAllText(GlobalData.LogFilePath, line + System.Environment.NewLine);
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                _b_logfile_enabled = false;
            }
        }
        private static bool _b_logfile_enabled = true;

    }
}
