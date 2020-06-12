using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Core.DomainObjects.Bases;
using Core.DomainObjects.TST;
using System.Threading;

namespace Core.DomainObjects.Base
{/*
    class AVBase
    {
        public string BaseName = @"C:\SigBase.dat";



        public ThreadSignature LoadRecords()
        {
            ThreadSignature sign = new ThreadSignature();



            return sign;
        }

        public void AddBase()
        {


            if (File.Exists(BaseName))
            {
                Console.WriteLine(BaseName + " already exists!");
                return;
            }




            BinaryWriter writer = new BinaryWriter(File.Open(BaseName, FileMode.OpenOrCreate));

            foreach (ThreadSignature s in thread)
            { 
                writer.Write(s.ThreadName);
                writer.Write(s.Signature.Data);
                writer.Write(s.Signature.DataLength);
                writer.Write(s.Signature.Hash);
                writer.Write(s.SignatureFirstByteMaxOffset);
                writer.Write(s.SignatureFirstByteMaxOffset);
            }



        }
    }

    */
    
}
