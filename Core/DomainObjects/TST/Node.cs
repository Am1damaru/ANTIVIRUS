using System;
using System.Collections.Generic;
using System.Text;
using Core.DomainObjects.Bases;

namespace Core.DomainObjects.TST
{
    public class Node
    {
        public byte Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Equal { get; set; }

        public bool End { get; set; }
        public List<ThreadSignature> ListSignatures { get; set; }

        public Node(ThreadSignature signature, byte data)
        {
            Data = data;
        }

        public Node(ThreadSignature signature, bool End)
        {
            if (ListSignatures == null)
            {
                ListSignatures = new List<ThreadSignature>();
                ListSignatures.Add(signature);
            }
            else
            {
                ListSignatures.Add(signature);
            }

        }

        public void Add(ThreadSignature signature)
        {

            Node current = this;
            int comparer;
            for (int i = 0; i < signature.Signature.Data.Length; i++)
            {
                comparer = signature.Signature.Data[i].CompareTo(current.Data);
                if (comparer == 0)
                {
                    if (i == signature.Signature.Data.Length - 1)
                    {
                        current.End = true;
                        current.Equal = new Node(signature, current.End);
                        return;
                    }

                    if (current.Equal == null)
                        current.Equal = new Node(signature, signature.Signature.Data[i + 1]);
                    current = current.Equal;
                }
                else
                {
                    if (comparer < 0)
                    {
                        if (current.Left == null)
                            current.Left = new Node(signature, signature.Signature.Data[i]);
                        current = current.Left;
                        i--;
                    }
                    else
                    {
                        if (current.Right == null)
                            current.Right = new Node(signature, signature.Signature.Data[i]);
                        current = current.Right;
                        i--;
                    }

                    if (i == signature.Signature.Data.Length - 1)
                    {
                        current.End = true;
                        return;
                    }
                }
            }
        }

        public List<ThreadSignature> CheckSign(byte[] data)
        {
            Node current = this;
            int comparer;
            for (int i = 0; i < data.Length;)
            {
                comparer = data[i].CompareTo(current.Data);
                if (comparer == 0)
                {
                    if (i == data.Length - 1)
                    {
                        return current.Equal.ListSignatures;
                    }
                    else
                    {
                        current = current.Equal;
                        i++;
                    }
                }
                else
                {
                    if (comparer < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }

                }
            }
            return null;
        }

    }
}
