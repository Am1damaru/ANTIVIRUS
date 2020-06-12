using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DomainObjects.ScanObjectAbstraction
{
    public interface IObjectContent
    {
        ulong SizeObject();

        bool Read(ulong Position, byte[] data, out uint readBytesCount);

    }
}
