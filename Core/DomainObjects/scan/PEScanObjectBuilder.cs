using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.DomainObjects.ScanObjectAbstraction;

namespace Core.DomainObjects.Scan
{
    public class PEScanObjectBuilder
    {


        public bool CheckPEFile(string path)
        {

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var firstTwoBytes = new byte[2];
                reader.BaseStream.Position = 0;
                for (int i = 0; i < 2; i++)
                {
                    firstTwoBytes[i] = reader.ReadByte();
                }

                return Encoding.UTF8.GetString(firstTwoBytes) == "MZ";



            }
     
        }

    }
}
