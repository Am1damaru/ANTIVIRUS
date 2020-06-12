using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainObjects.Bases;
using Core.DomainObjects.ScanObjectAbstraction;
using Core.DomainObjects.TST;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            byte[] bytes = { 52, 84, 35, 255, 242 };

            ThreadSignature[] sig = new ThreadSignature[2];

            sig[0] = new ThreadSignature("adhjfbwjefvqjhvhqwgwqvbaskjvbesdhbvksjvbwv","34 54 23 FF F2" , 15,20);
            sig[1] = new ThreadSignature("adhjfbw","53 FA 24 31" , 15, 20);

            Tree tree = new Tree();

            tree.Add(sig[0]);
            tree.Add(sig[1]);



            Console.WriteLine(BitConverter.ToString(sig[1].Signature.Hash));


            List<ThreadSignature> signat = new List<ThreadSignature>();

            signat = tree.CheckSignature(bytes);



            string path = @"C:\export.txt";

            ScanObject obj = new ScanObject("Name", path);

            Console.WriteLine(BitConverter.ToString(obj.Read(20)));





            Console.ReadKey();
        }
    }
}
