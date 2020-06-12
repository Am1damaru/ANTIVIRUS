using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.DomainObjects.ScanObjectAbstraction;

namespace Core.DomainObjects.Scan
{
    class PEScanObjectBuilder
    {







        private bool CheckPEFile(string file)
        {
            try
            {
                var firstTwoBytes = new byte[2];
                using (var fileStream = File.Open(file, FileMode.Open))
                {
                    fileStream.Read(firstTwoBytes, 0, 2);
                }
                return Encoding.UTF8.GetString(firstTwoBytes) == "MZ";
            }
            catch (Exception ex)
            {
                
            }
            return false;

        }
        
        public ScanObject SearchContent()
        {
            ScanObject obj = new ScanObject();


        }
        
        



    }
}
