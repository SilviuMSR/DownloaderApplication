using DownloaderApplication.DownloadP;
using DownloaderApplication.ReaderP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DownloaderApplication.InputP
{
    class Input
    {

        public void WorkWithUserInput(string UserInput, WebHandler web)
        {
            string[] InputWords = UserInput.Split(Convert.ToChar(" "));

            /* File with urls and filenames to be created */
            string UrlNames = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), InputWords[1]);
            /* Directory where we will save all created files */
            string DirectoryName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), InputWords[3]);

            Reader reader = new Reader();

            reader.GestionateUrls(UrlNames, DirectoryName, Convert.ToInt32(InputWords[5]), web);
           
        }
    }
}
