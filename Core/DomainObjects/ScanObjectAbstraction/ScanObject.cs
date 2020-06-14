using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Text;
using Core.DomainObjects.Scan;

namespace Core.DomainObjects.ScanObjectAbstraction
{

    public class ScanObject : IObjectContent //объект для сканирования
    {
        public string Name { get; }

        public static string PathToBase = @"C:\amw\av\";

        public string Path { get; set; }

        public ulong Length { get; set; }

        public List<ScanRegion> ScanRegions  { get; }

        public List<ScanObject> ChildObjects { get; }

        public ScanObject(string Path)
        {
            ZipScanObjectBuilder check = new ZipScanObjectBuilder();
            if(check.CheckZIPFile(Path))
            {
                this.Name = Path;
                this.Path = Path;
                using (FileStream zipToOpen = new FileStream(Path, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.Name != "" && entry.Name.EndsWith(".exe"))
                            {
                                try
                                {
                                    Directory.CreateDirectory(PathToBase);
                                    entry.ExtractToFile(PathToBase + entry.Name);
                                }
                                catch (Exception)
                                {

                                    
                                }

                                if (ChildObjects == null)
                                {
                                    ChildObjects = new List<ScanObject>();
                                    ChildObjects.Add(new ScanObject(entry.FullName, PathToBase + entry.Name));

                                }

                            }
   
                        }
                    }
                }
            }
            else
            {
                this.Path = Path;
            } 
        }

        public ScanObject(string Name, string path)
        {
            this.Name = Name;
            this.Path = path;
            ulong bytes = 1;
            if (ScanRegions == null)
            { ScanRegions = new List<ScanRegion>();}
            
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {

                while (reader.BaseStream.Position != reader.BaseStream.Length)

                {
                    if ((bytes % 1024 == 0) && (bytes >= 1024))
                    {
                        reader.ReadByte();
                        ScanRegions.Add(new ScanRegion((bytes - 1024), 1024, new ScanObject(Path)));
                        bytes++;
                        continue;
                    }
                    reader.ReadByte();
                    bytes++;
                }
                bytes--;
                ScanRegions.Add(new ScanRegion((bytes - (bytes % 1024)), (bytes % 1024), new ScanObject(Path)));
                this.Length = bytes - 1;
            }
        }
        public ulong SizeObject()
        {
            return Length;
        }

        public byte[] Read(ulong Position)
        {
            if(ScanRegions.Count != 0) 
            { 
            foreach (var s in ScanRegions)
            {
                if (s.Size < Position)
                {
                    continue;
                }
                return s.Read(Position);

            }
            }
            byte[] b = new byte[] { 0 };
            return b;

        }

    }
}
