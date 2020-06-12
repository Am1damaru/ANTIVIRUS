using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DomainObjects.scan
{
    class ScanReport
    {
        public string initiator { get; set; }

        public DateTime timeStart { get; set; }

        public DateTime timeStop { get; set; }

        public ulong scannedFiles { get; set; }

        public ulong scannedObjects { get; set; }

        public ulong threatsDetected { get; set; }

        public List<string> listThread { get; set; }

    }
}
