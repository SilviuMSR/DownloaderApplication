using System;
using System.Collections.Generic;
using System.Text;

namespace DownloaderApplication.DownloadP
{
    interface IWebHandler
    {
        bool CheckExistingUrl(string UrlName);
    }
}
