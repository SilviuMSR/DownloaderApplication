using DownloaderApplication.DownloadP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace DownloaderApplication.ReaderP
{
    class Reader
    {
        public void GestionateUrls(string UrlNames, string DirectoryName, int NumberOfBytes, WebHandler web)
        {
            string line;
            using (StreamReader reader = new StreamReader(UrlNames))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] LineWords = line.Split(Convert.ToChar(" "));
                    string FileName = LineWords[1];
                    Console.WriteLine(FileName);

                    try
                    {   
                       web.Downloader(LineWords[0], DirectoryName, FileName, NumberOfBytes);
                    }
                    catch (InvalidDataException e)
                    {
                        Console.WriteLine("Invalid url path");
                    }
                }
            }
        }
    }
}
