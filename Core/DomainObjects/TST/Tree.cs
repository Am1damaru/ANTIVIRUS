using System;
using System.Collections.Generic;
using System.Text;
using Core.DomainObjects.Bases;

namespace Core.DomainObjects.TST
{
    public class Tree
    {
        public Node Root { get; set; }
        public static int Count { get; set; }

        public void Add(ThreadSignature signature)
        {

            if (Root == null)
            {
                Root = new Node(signature, signature.Signature.Data[0]);
                Root.Add(signature);
            }
            else
            {
                Root.Add(signature);
            }
            Count++;
        }

        public List<ThreadSignature> CheckSignature(byte[] data)
        {
            return Root.CheckSign(data);
        }    


        

    }
}
