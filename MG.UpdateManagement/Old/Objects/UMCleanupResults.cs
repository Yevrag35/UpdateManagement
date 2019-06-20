using MG.UpdateManagement.Framework;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public class UMCleanupResults
    {
        public string WsusServer => UMContext.ServerName;
        public int SupersededUpdatesDeclined { get; }
        public int ExpiredUpdatesDeclined { get; }
        public int ObsoleteComputersDeleted { get; }
        public int ObsoleteUpdatesDeleted { get; }
        public int UpdatesCompressed { get; }
        public SizeInXBytes DiskSpaceFreed { get; }


        public UMCleanupResults()
            : base()
        {
        }
        private protected UMCleanupResults(CleanupResults garbageResults)
        {
            SupersededUpdatesDeclined = garbageResults.SupersededUpdatesDeclined;
            ExpiredUpdatesDeclined = garbageResults.ExpiredUpdatesDeclined;
            ObsoleteComputersDeleted = garbageResults.ObsoleteComputersDeleted;
            ObsoleteUpdatesDeleted = garbageResults.ObsoleteUpdatesDeleted;
            UpdatesCompressed = garbageResults.UpdatesCompressed;
            DiskSpaceFreed = garbageResults.DiskSpaceFreed;
        }

        public static implicit operator UMCleanupResults(CleanupResults results) =>
            new UMCleanupResults(results);
    }

    public class SizeInXBytes : IEnumerable<decimal>
    {
        private protected long baseDec;
        private protected decimal baseValue => baseDec * 1.00M;
        public decimal GigabyteValue => Math.Round(baseValue / 1073741824.00M, 2);
        public decimal MegabyteValue => Math.Round(baseValue / 1048576.00M, 2);
        public decimal KilobyteValue => Math.Round(baseValue / 1024.00M, 2);
        public decimal ByteValue => baseDec;

        private protected SizeInXBytes(long sizeInBytes) =>
            baseDec = sizeInBytes;

        public static implicit operator SizeInXBytes(long byteSize) =>
            new SizeInXBytes(byteSize);
        public static implicit operator SizeInXBytes(int byteSize) =>
            new SizeInXBytes(byteSize);
        public static explicit operator decimal(SizeInXBytes xSize) => xSize.baseValue;

        public IEnumerator<decimal> GetEnumerator() =>
            new List<decimal>(1) { ByteValue }.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            new List<decimal>(1) { ByteValue }.GetEnumerator();
    }
}
