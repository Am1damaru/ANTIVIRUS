using System.Collections.Generic;
using System.IO;

namespace Core.DomainObjects.ScanObjectAbstraction
{

    public class ScanObject : IObjectContent //объект для сканирования
    {
        public string Name { get; }

        public string Path { get; set; }

        public ulong Length { get; set; }

        public List<ScanRegion> ScanRegions { get; }

        public List<ScanObject> ChildObjects { get; }

        public ScanObject(string Path)
        {

            this.Path = Path;
        }

        public ScanObject(string Name, string path)
        {
            this.Name = Name;
            this.Path = path;
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                ulong bytes = 1;
                while (reader.PeekChar() > -1)
                {
                    if(bytes%1024 ==0)
                    {
                        ScanRegion scans = new ScanRegion((bytes - 1024), 1024, new ScanObject(Path));
                        ScanRegions.Add(scans);
                    }
                    ScanRegion scan = new ScanRegion(bytes-(bytes%1024), (bytes % 1024), new ScanObject(Path));
                    ScanRegions.Add(scan);
                    bytes++;
                }
                this.Length = bytes-1;
            }
        }
        public ulong SizeObject()
        {
            return Length;

        }

        public byte[] Read(ulong Position)
        {
            foreach(var s in ScanRegions)
            {
                if(s.Size<Position)
                {
                    continue;
                }
                return s.Read(Position);

            }
            return null;

        }
        



    }
}
