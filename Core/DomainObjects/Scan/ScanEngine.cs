using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainObjects.Base;
using Core.DomainObjects.Bases;
using Core.DomainObjects.scan;
using Core.DomainObjects.ScanObjectAbstraction;
using Core.DomainObjects.TST;

namespace Core.DomainObjects.Scan
{
    public class ScanEngine
    {
        public DateTime timeStart { get; }

        public DateTime timeStop { get; }

        public static long ScanObjects { get; }

        private Tree tree;

        private string path;

        public ScanEngine()
        {
            tree = new Tree();
            AVBase avBase = new AVBase();
            tree = avBase.LoadRecords();
        }


        public ScanReport StartScanObject(ScanObject obj)
        {

            if(obj.ScanRegions == null)
            {
                ulong i = 0;
                ScanReport rep = new ScanReport();
                foreach (var chobj in obj.ChildObjects)
                for (i = 0; i < chobj.SizeObject(); i++)
                {
                    byte[] prefix = new byte[8];
                    prefix = chobj.Read(i);

                    List<ThreadSignature> signatures = tree.CheckSignature(prefix);
                    if(signatures != null)
                        { 
                            foreach (var sig in signatures)
                            {
                                if (sig.SignatureFirstByteMinOffset < i && sig.SignatureFirstByteMaxOffset > i)
                                {
                                    uint dataLength = sig.Signature.DataLength;
                                    byte[] searchSign = new byte[dataLength];
                                    searchSign = chobj.Read(i);
                                    if (sig.Signature.IsMatch(searchSign))
                                    {
                                        rep.AddRecord(new ScanReport(new string(sig.ThreadName), obj.Path + chobj.Name));
                                    }
                                }
                            }
                        }
                    }
                rep.scannedObjects = i;
                return rep;
            }
            else
            {
                for (ulong i = 0; i < obj.SizeObject(); i++)
                {
                    byte[] prefix = new byte[8];
                    prefix = obj.Read(i);

                    List<ThreadSignature> signatures = tree.CheckSignature(prefix);
                    if (signatures != null)
                    {
                        foreach (var sig in signatures)
                        {
                            if (sig.SignatureFirstByteMinOffset < i && sig.SignatureFirstByteMaxOffset > i)
                            {
                                uint dataLength = sig.Signature.DataLength;
                                byte[] searchSign = new byte[dataLength];
                                searchSign = obj.Read(i);
                                if (sig.Signature.IsMatch(searchSign))
                                {
                                    return new ScanReport(new string(sig.ThreadName), obj.Path);
                                }
                            }
                        }

                    }
                }
            }
            return null;
        }
    }
}
