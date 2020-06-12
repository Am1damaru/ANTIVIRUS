using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DomainObjects.ScanObjectAbstraction
{
    public interface IObjectContent
    {

        string Path { get; set; }
        ulong SizeObject();

        byte[] Read(ulong Position);

    }
}
