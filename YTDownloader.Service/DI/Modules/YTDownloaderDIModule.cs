using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTDownloader.Service.Downloader;

namespace YTDownloader.Service.DI.Modules
{
    public class YTDownloaderDIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDownloader>().To<CustomYTDownloader>();
        }
    }
}