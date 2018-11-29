using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DownloaderApplication.DownloadP
{
    class WebHandler : IWebHandler
    {

        public async void Downloader(string UrlName, string WhereToSave, string FileName, int LimitedBytes)
        {
            await ContentDownloader(UrlName, WhereToSave, FileName, LimitedBytes);
        }

        public bool CheckExistingUrl(string UrlName)
        {
            Uri uriResult;

            // Used to check if given url is a valid one
            bool result = Uri.TryCreate(UrlName, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task ContentDownloader(string UrlName, string WhereToSave, string FileName, int LimitedBytes)
        {

            return Task.Factory.StartNew(() =>
                {
                    try
                    {
                        long FileSize = 0;
                        int BufferSize = LimitedBytes;
                        long ExistFileSize = 0;

                        FileStream SaveFileStream;

                        if (CheckExistingUrl(UrlName) == false)
                        {
                            throw new InvalidDataException();
                        }

                        /*Check if the file already exist*/
                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName)))
                        {
                            FileInfo FInfo = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName));
                            /* Recieve how much size was already downloaded */
                            ExistFileSize = FInfo.Length;
                        }
                        /* If we already have some content in that file, we need to created that file only in append mode and to attach the rest of the content there*/
                        if (ExistFileSize > 0)
                        {
                            SaveFileStream = new FileStream(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), WhereToSave), FileName), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        }
                        /* If that file have size 0 means that is not created so we need to open that file in create mode and add that content*/
                        else
                        {
                            SaveFileStream = new FileStream(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), WhereToSave), FileName), FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                        }

                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(UrlName);
                        /* Add the content after the existing length */
                        request.AddRange((int)ExistFileSize);

                        Stream ResponseStream;

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        ResponseStream = response.GetResponseStream();

                        FileSize = response.ContentLength;
                        Console.WriteLine("We need to write " + FileSize + " bytes");

                        int Recieved;
                        byte[] buffer = new byte[BufferSize];
                        int Sum = 0;

                        while ((Recieved = ResponseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            SaveFileStream.Write(buffer, 0, Recieved);
                            SaveFileStream.Flush();
                            Sum += Recieved;
                        }

                        if (Sum == FileSize)
                        {
                            Console.WriteLine("Successfully writed " + Sum + " bytes." + " File " + FileName + " was created");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Unsuccesffully writed file");
                    }
                }
            );
        }

    }
}
