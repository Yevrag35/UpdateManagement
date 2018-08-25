using Dynamic;
using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Exceptions;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading;

namespace MG.UpdateManagement.Cmdlets
{
    [Cmdlet("Download", "UMUpdate", ConfirmImpact = ConfirmImpact.None)]
    [CmdletBinding(PositionalBinding = false)]
    public class DownloadUMUpdate : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, DontShow = true)]
        public UMUpdate Update { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        private static ProgressBar pb;
        private static HttpWebResponse res;
        private static Stream resStream;
        private static Stream fileStream;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            var dlItem = new UMDownload(Update, Path);
            Download(dlItem);
        }

        private static void BreakHandler(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            fileStream.Close();
            fileStream.Dispose();
            resStream.Close();
            resStream.Dispose();
            res.Close();
            res.Dispose();
            pb.Dispose();
        }

        private void Download(UMDownload dlItem)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakHandler);
            var dl = dlItem;
            string finalPath = dl.DownloadPath + "\\" + dl.FileName;
            int bytesProcessed = 0;
            pb = new ProgressBar();
            try
            {
                var req = WebRequest.CreateHttp(dl.UpdateUrl);
                res = req.GetResponse() as HttpWebResponse;
            }
            catch (Exception e)
            {
                throw new Exception("Download failed!", e);
            }
            using (resStream = res.GetResponseStream())
            {
                long Length = res.ContentLength;
                using (fileStream = File.OpenWrite(finalPath))
                {
                    byte[] buffer = new byte[10240];
                    int bytesRead = resStream.Read(buffer, 0, 10240);
                    try
                    {
                        while (bytesRead > 0)
                        {
                            bytesProcessed += bytesRead;
                            pb.Report((double)bytesProcessed / Length);
                            fileStream.Write(buffer, 0, bytesRead);
                            bytesRead = resStream.Read(buffer, 0, 10240);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Download cancelled!");
                        File.Delete(finalPath);
                        return;
                    }
                }
                resStream.Close();
                resStream.Dispose();
            }
            pb.Dispose();
            Console.WriteLine("Download finished for " + dl.FileName);
        }
    }
}
