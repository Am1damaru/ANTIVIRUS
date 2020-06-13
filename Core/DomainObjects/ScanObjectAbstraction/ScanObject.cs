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

        public static string PathToZip = @"C:\amw\av\";

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
                                    Directory.CreateDirectory(PathToZip);
                                    entry.ExtractToFile(PathToZip + entry.Name);
                                }
                                catch (Exception)
                                {

                                    
                                }

                                if (ChildObjects == null)
                                {
                                    ChildObjects = new List<ScanObject>();
                                    ChildObjects.Add(new ScanObject(entry.Name, PathToZip+ entry.Name));

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

            return null;

        }

        public byte[] ReadZip(ulong Position)
        {

            using (FileStream zipToOpen = new FileStream(Name, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = archive.GetEntry(Path);


                    entry.ExtractToFile(@"C:\amw\f.exe");
                    return null;











                    /*
                    using (StreamReader buffer = new StreamReader(entry.Open(), Encoding.Default))
                    {
                        // Console.WriteLine(entry.Name);
                        byte[] bytes = new byte[8];
                        char[] ch = new char[8];
                        char[] cha = new char[Convert.ToInt32(Position)];
                        Console.WriteLine(Convert.ToInt32(Position));
                        buffer.Read(cha, 0, Convert.ToInt32(Position));

                        int readed = buffer.Read(;
                        string s = new string(ch);
                        bytes = (s);
                        return bytes;


                    }
                    */


                    /*
                     * 
                     * 
                     * 
                    using (BinaryReader reader = new BinaryReader(entry.Open()))
                    {

                        reader.BaseStream.Position = (long)Position;
                        byte[] bytes = new byte[8];
                        for (int i = 0; i < 8; i++)
                        {
                            bytes[i] = reader.ReadByte();
                        }
                        return bytes;

                    }
                    */
                }
            }

        }

    }
}
