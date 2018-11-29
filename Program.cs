using DownloaderApplication.DownloadP;
using DownloaderApplication.InputP;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace DownloaderApplication
{
    class Program
    {

        static void Get()
        {

        }
        static void Main(string[] args)
        {

            WebHandler web = new WebHandler();

            Console.WriteLine("Please insert next format -f FileWithUrls -o DirectoryWhereToSaveFiles -l LimitedBytesPerSecond");
            string UserInput = Console.ReadLine();

            Input input = new Input();


            input.WorkWithUserInput(UserInput, web);



            Console.Read();
        }
    }
}
