using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.DomainObjects.Scan
{
    public class ZipScanObjectBuilder
    {

        public bool CheckZIPFile(string path)
        {
            try
            {
                var firstFourBytes = new byte[4];
                using (var fileStream = File.Open(path, FileMode.Open))
                {
                    fileStream.Read(firstFourBytes, 0, 4);
                }

                string[] strBytesZip= new string[] { "50", "4B", "03", "04" };

                byte[] bytes = new byte[4];

                int i = 0;
                foreach (string s in strBytesZip)
                {
                    bytes[i] = (byte)Convert.ToInt32(s, 16);
                    i++;
                }

                return firstFourBytes.SequenceEqual(bytes);
            }
            catch (Exception ex)
            {

            }
            return false;

        }





    }
}
