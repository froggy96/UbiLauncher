using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UbiLauncher
{
    public class UbiProcessList
    {
        List<UbiProcess> _list;

        public UbiProcessList()
        {
            _list = new List<UbiProcess>();
        }

        public List<UbiProcess> ProcessList
        {
            get { return _list; }
        }

        public UbiProcess GetByGUID(string guid)
        {
            foreach (UbiProcess proc in _list)
            {
                if (proc.GUID == guid) return proc;
            }
            return null;
        }

        public int RunningProcessesCount
        {
            get
            {
                int c = 0;
                foreach (UbiProcess p in _list)
                {
                    if (p.Status == UbiProcess.ProcessStatus.Running) c++;
                }

                return c;
            }
        }

        public int StoppedProcessesCount
        {
            get
            {
                int c = 0;
                foreach (UbiProcess p in _list)
                {
                    if (p.Status == UbiProcess.ProcessStatus.Stopped) c++;
                }

                return c;
            }
        }


        #region [Serialize]
        public bool SaveData(string filepath)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    try
                    {
                        bf.Serialize(fs, _list);
                        MessageBox.Show("Saved!");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                //using (FileStream fs = new FileStream(filepath, FileMode.Create))
                //{
                //    XmlSerializer ser = new XmlSerializer(typeof(List<UbiProcess>));
                //    ser.Serialize(fs, _list);
                //    fs.Flush();
                //    fs.Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }


        public void ReadData(string filepath)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filepath, FileMode.Open))
                {
                    try
                    {
                        _list = (List<UbiProcess>)bf.Deserialize(fs);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


                //List<UbiProcess> list = new List<UbiProcess>();
                //using (FileStream fs = new FileStream(filepath, FileMode.Open))
                //{
                //    XmlSerializer ser = new XmlSerializer(typeof(List<UbiProcess>));
                //    list = (List<UbiProcess>)ser.Deserialize(fs);
                //}
                //_list = list;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
