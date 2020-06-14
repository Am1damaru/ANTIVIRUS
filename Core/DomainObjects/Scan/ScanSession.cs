using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainObjects.Base;
using Core.DomainObjects.scan;
using Core.DomainObjects.ScanObjectAbstraction;

namespace Core.DomainObjects.Scan
{
    public class ScanSession
    {
        public ScanEngine scan;

        public string Initiator;

        public ScanReport report;

        public ScanSession(string path, string initiator)
        {
            ScanEngine scan = new ScanEngine();
            Initiator = initiator;
            report = new ScanReport(initiator);
            ScanObjectBuilder build = new ScanObjectBuilder(path);
            List<ScanObject> scanObjects = build.GetObjects();

            foreach(var sObj in scanObjects)
            {
                report.AddRecord(scan.StartScanObject(sObj));
                report.scannedObjects++;
            }

        }

        public ScanReport getReport()
        {
            
            try
            {
                Directory.Delete(@"C:\amw\av\", true);
            }
            catch
            {

            }
            return report;
            
        }





    }
}
