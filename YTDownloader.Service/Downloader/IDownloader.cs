using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader.Service.Downloader
{
    public interface IDownloader
    {
        void Setup(Resolution resolution, Extensions extension, string uri, string savePath, string title);

        void DownloadVideo();
    }

    public enum Resolution
    {
        Default = 360,
        HD = 720,
        FullHD = 1080
    }

    public enum Extensions
    {
        MP3 = 0,
        MP4 = 1,
        AVI = 2,
        MPEG = 3
    }
}