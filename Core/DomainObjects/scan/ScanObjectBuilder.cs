using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.DomainObjects.ScanObjectAbstraction;

namespace Core.DomainObjects.Scan
{
    public class ScanObjectBuilder
    {

        

        private List<ScanObject> ScanObjects;


        public ScanObjectBuilder(string path)
        {
            ScanObjects = new List<ScanObject>();
            
            SearchObjects(path);
        }


        private void SearchObjects(string path)
        {
            ZipScanObjectBuilder zipCheck = new ZipScanObjectBuilder();
            PEScanObjectBuilder PECheck = new PEScanObjectBuilder();
            DirectoryInfo DI = new DirectoryInfo(path);
            DirectoryInfo[] SubDir = DI.GetDirectories();
            for (int i = 0; i < SubDir.Length; ++i)
                this.SearchObjects(SubDir[i].FullName);
            FileInfo[] FI = DI.GetFiles();
            for (int i = 0; i < FI.Length; ++i)
            {
                if (PECheck.CheckPEFile((FI[i].DirectoryName) + "\\" + FI[i].Name))
                {
                    ScanObjects.Add(new ScanObject(FI[i].Name, (FI[i].DirectoryName)+"\\"+ FI[i].Name));

                }
                if (zipCheck.CheckZIPFile((FI[i].DirectoryName) + "\\" + FI[i].Name))
                {
                    ScanObjects.Add(new ScanObject((FI[i].DirectoryName) + "\\" + FI[i].Name));
                }

            } 
        }

        public List<ScanObject> GetObjects()
        {
            return ScanObjects;
        }


    }
}
