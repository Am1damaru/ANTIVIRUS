

namespace Core.DomainObjects.ScanObjectAbstraction
{
    public interface IScanRegionReadCallback
    {
        void OnScanRegionBegin(ulong regionOffset, ulong regionSize);

        void OnScanRegionData(ulong regionOffset, ulong RegionSize, byte[] regionData, uint RegionDataActualBytes);

        void OnScanRegionEnd(ulong regionOffset, ulong regionSize);

    }

    public class ScanRegion
    {
        public ulong Offset { get; }  //Смещение относительно начала контента
        public ulong Size { get; } //Размер сегмента

        public IObjectContent Content { get; }

        public void Read(byte[] block, IScanRegionReadCallback callback) //Метод блочного чтения заданного региона
        {
            ulong position = Offset;
            callback?.OnScanRegionBegin(Offset, Size);
            while (Content.Read(position, block, out uint bytesRead))
            {
                position += bytesRead;
                callback?.OnScanRegionEnd(Offset, Size);
            }

        }

    }
}
