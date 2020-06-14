using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Core.DomainObjects.scan
{
    public class ScanReport
    {
        public string initiator { get; set; }

        public DateTime timeStart { get; set; }

        public DateTime timeStop { get;  }

        public ulong scannedObjects { get; set; }

        public ulong threatsDetected { get; set; }

        public List<string> listThread { get; set; }

        private string NameVirus;

        public ScanReport()
        {
            timeStart = DateTime.Now;
            listThread = new List<string>();
        }

        public ScanReport(string Init)
        {
            listThread = new List<string>();
            timeStart = DateTime.Now;
            initiator = Init;
        }

        public ScanReport(string Name, string path)
        {
            NameVirus = Name;
            if(listThread == null)
                listThread = new List<string>();
            listThread.Add(path + "-VIRUS-" + Name);
        }
        public void AddRecord(ScanReport report)
        {

            if (report != null)
            {
                foreach (var s in report.listThread)
                {
                    listThread.Add(s);
                }
                threatsDetected = (ulong)listThread.Count;
            }
        }
    }
}
