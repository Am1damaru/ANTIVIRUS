using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

namespace Core.DomainObjects.Bases
{
    public class ThreadSignature
    {
        public char[] ThreadName { get; }

        public SignatureAsBlobAndHash Signature { get; }

        public ulong SignatureFirstByteMinOffset { get; }

        public ulong SignatureFirstByteMaxOffset { get; }

        public ThreadSignature(string Name, string allData, ulong MinOffset, ulong MaxOffset)
        {
            ThreadName = Name.PadRight(20).ToCharArray(0,20);

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
        public ThreadSignature()
        { }

    }

    public class SignatureAsBlob
    {
        public virtual uint DataLength => (uint)Data.Length;

        public byte[] Data { get; }

        public SignatureAsBlob(byte[] data)
        {
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

        private readonly uint _dataLength;

        public override uint DataLength => _dataLength;

        public byte[] Hash { get; }

        public SignatureAsBlobAndHash(byte[] allData)
            : base(allData.Take(SIGNATURE_PREFIX_LENGTH).ToArray())
        {
            _dataLength = (uint)allData.Length;
            Hash = CalculateHash(allData);
        }

        public SignatureAsBlobAndHash(uint fullDataLength, byte[] dataFirstBytes, byte[] fullDataHash)
            : base(dataFirstBytes)
        {
            _dataLength = fullDataLength;
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
