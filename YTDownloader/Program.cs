using Konsole;
using System;
using System.Threading;
using System.Threading.Tasks;
using YTDownloader.Service.DI;
using YTDownloader.Service.Downloader;

namespace YTDownloader
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string downloadUri = args[0];
            string savePath = args[1];
            string title = args[2];

            Console.WriteLine("START");
            var t1 = Task.Run(() =>
            {
                Runner runner = new Runner();
                runner.Run(downloadUri, savePath, title);
            });
            var t2 = Task.Run(() =>
            {
                Runner runner = new Runner();
                runner.Run(downloadUri, savePath, title + "1");
            });
            Task.WaitAll(new[] { t1, t2 });
            Console.WriteLine("STOP");
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }
    }

    public class Runner
    {
        public void Run(string uri, string savePath, string title)
        {
            IDIContainer container = new DIContainer();
            container.Setup();

            IDownloader downloader = container.GetInstance<IDownloader>();
            downloader.Setup(Resolution.HD, Extensions.MP4, uri, savePath, title);

            downloader.DownloadVideo();
        }
    }
}