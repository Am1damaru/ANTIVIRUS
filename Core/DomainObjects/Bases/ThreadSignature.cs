using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

namespace Core.DomainObjects.Bases
{
    public class ThreadSignature
    {
        public char[] ThreadName { get; set; }

        public SignatureAsBlobAndHash Signature { get; set; }

        public ulong SignatureFirstByteMinOffset { get; set; }

        public ulong SignatureFirstByteMaxOffset { get; set; }

        public ThreadSignature(string Name, string allData, ulong MinOffset, ulong MaxOffset)
        {
            ThreadName = Name.PadRight(20).ToCharArray(0, 20);

            string[] hexValuesSplit = allData.Split(' ');

            byte[] bytes = new byte[hexValuesSplit.Length];

            int i = 0;
            foreach (string s in hexValuesSplit)
            {
                bytes[i] = (byte)Convert.ToInt32(s, 16);
                i++;
            }

            Signature = new SignatureAsBlobAndHash(bytes);
            SignatureFirstByteMinOffset = MinOffset;
            SignatureFirstByteMaxOffset = MaxOffset;
        }
        public ThreadSignature(char[] Name, ulong MinOffset, ulong MaxOffset, uint DataLength, byte[] Data, byte[] Hash)
        {
            this.ThreadName = Name;
            this.SignatureFirstByteMinOffset = MinOffset;
            this.SignatureFirstByteMaxOffset = MaxOffset;
            Signature = new SignatureAsBlobAndHash(DataLength, Data, Hash);

        }

    }

    public class SignatureAsBlob
    {
        public virtual uint DataLength { get; set; }

        public byte[] Data { get; set; }

        public SignatureAsBlob(byte[] data)
        {
            DataLength = (uint)data.Length;
            Data = data;
        }


        public virtual bool IsMatch(byte[] objectData)
        {
            if (!CheckArgumentLength(objectData))
            {
                return false;
            }
            return objectData.SequenceEqual(Data);
        }

        protected virtual bool CheckArgumentLength(byte[] objectData)
            => (objectData != null && DataLength == objectData.Length);

    }

    public class SignatureAsBlobAndHash : SignatureAsBlob
    {
        private const int SIGNATURE_PREFIX_LENGTH = 8;



        public override uint DataLength { get; set; }

        public byte[] Hash { get; set; }

        public SignatureAsBlobAndHash(byte[] allData)
            : base(allData.Take(SIGNATURE_PREFIX_LENGTH).ToArray())
        {
            DataLength = (uint)allData.Length;
            Hash = CalculateHash(allData);
        }

        public SignatureAsBlobAndHash(uint fullDataLength, byte[] dataFirstBytes, byte[] fullDataHash)
            : base(dataFirstBytes)
        {
            DataLength = fullDataLength;
            Hash = fullDataHash;
        }


        public override bool IsMatch(byte[] objectData)
        {
            if (!CheckArgumentLength(objectData))
            {
                return false;
            }
            return Hash.SequenceEqual(CalculateHash(objectData));
        }

        private byte[] CalculateHash(byte[] data)
        {
            var hashCalculator = SHA256.Create();
            return hashCalculator.ComputeHash(data);
        }
    }
}
