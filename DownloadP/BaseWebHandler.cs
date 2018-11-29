using System;
using System.Collections.Generic;
using System.Text;

namespace DownloaderApplication.DownloadP
{
    class BaseWebHandler : IWebHandler
    {
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
    }
}
