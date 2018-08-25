using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public class UMDownload
    {
        private readonly Uri _uri;
        private readonly string _tit;
        private readonly Guid _id;
        private readonly string _dlPath;

        public Uri UpdateUrl => _uri;
        public string FileName => _tit;
        public Guid UpdateId => _id;
        public string DownloadPath => _dlPath;

        public UMDownload(UMUpdate update, string downloadPath)
        {
            _id = update.Id.UpdateId;
            _dlPath = downloadPath;
            var tempList = update.GetInstallableItems();
            for (int i = 0; i < tempList.Count; i++)
            {
                var item = tempList[i];
                for (int i1 = 0; i1 < item.Files.Count; i1++)
                {
                    var file = item.Files[i1];
                    if (file.Name.EndsWith(".cab", StringComparison.OrdinalIgnoreCase) &&
                        file.Type == FileType.SelfContained)
                    {
                        _uri = file.OriginUri;
                        _tit = file.Name;
                    }
                }
            }
        }
    }
}
