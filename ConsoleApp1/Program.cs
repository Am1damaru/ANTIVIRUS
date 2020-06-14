using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainObjects.Bases;
using Core.DomainObjects.ScanObjectAbstraction;
using Core.DomainObjects.TST;
using Core.DomainObjects.Scan;
using Core.DomainObjects.Base;
using Core.DomainObjects.scan;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            


            ScanSession session = new ScanSession(@"C:\amw", "User");

            ScanReport report = session.getReport();








            /*
            string z;
            string a;
            byte[] q = new byte[8];
            ScanObjectBuilder scan = new ScanObjectBuilder(@"C:\");

            List<ScanObject> s = scan.GetObjects();

            
            q = s[3].ChildObjects[0].Read(0);
            q = s[2].Read(0);


            
            ScanObjectBuilder scan = new ScanObjectBuilder(@"C:\lab8");

            
            List<ScanObject> scans = scan.GetObjects();



            ThreadSignature[] sig = new ThreadSignature[2];

            sig[0] = new ThreadSignature("adhf", "69 73 20 70 72 6F 67 72", 15, 20);
            sig[1] = new ThreadSignature("adhjfbw", "53 FA 24 31 FF FF FF FF", 15, 20);

            AVBase base1 = new AVBase();


            base1.AddBase(sig[0]);

            

            byte[] bytes = { 83, 250, 36, 49, 255 ,255,255,255 };

           

            Console.WriteLine(BitConverter.ToString(sig[1].Signature.Hash));
            string path = @"C:\Users\AmidamaRU\Downloads\FP-master.zip";

            ZipScanObjectBuilder zip = new ZipScanObjectBuilder();

            bool s = zip.CheckZIPFile(path);

            AVBase base1 = new AVBase();
            base1.CreateBase();

            
            base1.AddBase(sig[1]);
            base1.AddBase(sig[0]);
            Tree tree=base1.LoadRecords();

            List<ThreadSignature> signat = new List<ThreadSignature>();

            signat = tree.CheckSignature(bytes);
            */
            /*
            

            Tree tree = new Tree();

            tree.Add(sig[0]);
            tree.Add(sig[1]);



            


            



            string path = @"C:\export.txt";

            ScanObject obj = new ScanObject("Name", path);

            Console.WriteLine(BitConverter.ToString(obj.Read(20)));

            */



            Console.ReadKey();
        }
    }
}
