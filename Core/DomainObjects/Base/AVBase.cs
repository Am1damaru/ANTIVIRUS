using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Core.DomainObjects.Bases;
using Core.DomainObjects.TST;
using System.Threading;

namespace Core.DomainObjects.Base
{
    public class AVBase
    {
        public string BasePath = "SigBase.dat";

        private string NameBase = "BVT1702-KGK";

        public Tree LoadRecords()
        {
            Tree tree = new Tree();


            using (BinaryReader reader = new BinaryReader(File.Open(BasePath, FileMode.Open)))
            {



                reader.BaseStream.Position = 0;
                if (reader.ReadString() == NameBase)
                {
                    reader.BaseStream.Position = 25;

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        char[] Name = reader.ReadChars(20);
                        ulong MinOffset = reader.ReadUInt64();
                        ulong MaxOffset = reader.ReadUInt64();
                        uint DataLength = reader.ReadUInt32();
                        byte[] Data = reader.ReadBytes(8);
                        byte[] Hash = reader.ReadBytes(32);
                        ThreadSignature sig = new ThreadSignature(Name, MinOffset, MaxOffset, DataLength, Data, Hash);
                        tree.Add(sig);
                    }

                }
                else
                {
                    return null;
                }
            }
            return tree;


        }

        public bool AddBase(ThreadSignature sign)
        {

            ulong count;
            using (BinaryReader reader = new BinaryReader(File.Open(BasePath, FileMode.Open)))
            {
                reader.BaseStream.Position = 0;
                if (reader.ReadString() == NameBase)
                {
                    reader.BaseStream.Position = 17;
                    count = reader.ReadUInt64();
                    count++;
                }
                else
                {
                    return false;
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(BasePath, FileMode.Open)))
            {
                writer.Seek(17, 0);
                writer.Write(count);

            }


            using (BinaryWriter writer = new BinaryWriter(File.Open(BasePath, FileMode.Append)))
            {

                writer.Write(sign.ThreadName);
                writer.Write(sign.SignatureFirstByteMinOffset);
                writer.Write(sign.SignatureFirstByteMaxOffset);
                writer.Write(sign.Signature.DataLength);
                writer.Write(sign.Signature.Data);
                writer.Write(sign.Signature.Hash);
                return true;
            }



        }

        public void CreateBase()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(BasePath, FileMode.OpenOrCreate)))
            {

                writer.Write(NameBase);
                writer.Seek(17, 0);
                writer.Write((ulong)0);

            }


        }
    }



}
