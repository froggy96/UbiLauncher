using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UbiLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;
            int cnt = 0;
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == thisprocessname) cnt++;
            }
            if (cnt > 1)
            {
                MessageBox.Show("Launcher Program Already Running...");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args));
        }
    }
}
