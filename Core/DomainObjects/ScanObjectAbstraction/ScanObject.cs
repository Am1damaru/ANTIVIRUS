using System.Collections.Generic;

namespace Core.DomainObjects.ScanObjectAbstraction
{
    public interface IScanObjectReadCallback : IScanRegionReadCallback
    {
        void OnObjectBegin(string Name);

        void OnObjectEnd(string Name);

        void OnChildObjectBegin(string parentName, string Name);

        void OnChildObjectEnd(string parentName, string Name);
    }

    public class ScanObject : IObjectContent //объект для сканирования
    {
        public string Name { get; }

        public List<ScanRegion> ScanRegions { get; }

        public List<ScanObject> ChildObjects { get; }

        public ulong SizeObject()
        {
          
        }

        public bool Read(ulong Position, byte[] data, out uint readBytesCount)
        {

        }





        public void Read(int blockSize, IScanObjectReadCallback readCallback)
        {
            byte[] block = new byte[blockSize];
            Read(block, readCallback, parentObject: null);
        }

        private void Read(byte[] block, IScanObjectReadCallback readCallback, ScanObject parentObject)
        {
            if (parentObject != null)
            {
                readCallback?.OnChildObjectBegin(parentObject.Name, Name);
            }
            else
            {
                readCallback?.OnObjectBegin(Name);
            }
            foreach (var region in ScanRegions)
            {
                region.Read(block, readCallback);
            }
            foreach (var childObject in ChildObjects)
            {
                childObject.Read(block, readCallback, this);
            }
            if (parentObject != null)
            {
                readCallback?.OnChildObjectEnd(parentObject.Name, Name);
            }
            else
            {
                readCallback?.OnObjectEnd(Name);
            }
        }
    }
}
