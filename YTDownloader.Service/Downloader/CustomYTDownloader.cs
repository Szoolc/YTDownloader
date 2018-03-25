using Konsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YTDownloader.Service.Downloader
{
    public class CustomYTDownloader : IDownloader
    {
        private static readonly Dictionary<Extensions, string> supportedExtensions = new Dictionary<Extensions, string>()
        {
            { Extensions.MP3, ".mp3" },
            { Extensions.MP4, ".mp4" },
            { Extensions.AVI, ".avi" },
            { Extensions.MPEG, ".mpeg" }
        };

        private string _TITLE = string.Empty;
        private ProgressBar progressBar;

        public CustomYTDownloader()
        {
            progressBar = new ProgressBar(100);
        }

        private VideoDownloader videoDownloader;

        public void DownloadVideo()
        {
            if (videoDownloader.Video == null)
            {
                throw new Exception("There is no setup for current downloader! Please setup configuration");
            }

            try
            {
                videoDownloader.Execute();
            }
            catch (Exception e)
            {
                progressBar.Refresh(0, $"FAILED! {e.Message}");
            }
        }

        public void Setup(Resolution resolution, Extensions extension, string uri, string savePath, string title)
        {
            if (supportedExtensions.ContainsKey(extension))
            {
                var infos = DownloadUrlResolver.GetDownloadUrls(uri);
                VideoInfo videoInfo = infos.FirstOrDefault(x => ConfigurationPredicate(x, resolution, supportedExtensions[extension]));

                if (videoInfo == null)
                    videoInfo = infos.FirstOrDefault(x => ConfigurationPredicate(x, Resolution.Default, supportedExtensions[extension]));
                if (videoInfo == null)
                    throw new Exception("There is no video with provided parameters!");

                videoDownloader = new VideoDownloader(videoInfo, savePath + "\\" + title + videoInfo.VideoExtension, Int32.MaxValue);
                _TITLE = title + " | " + videoInfo.Title;
                videoDownloader.DownloadProgressChanged += OnProgressChanged;
                videoDownloader.DownloadStarted += OnDownloadStarted;
                videoDownloader.DownloadFinished += OnDownloadFinished;
            }
            else
            {
                throw new Exception("Unsupported extension type for current Downloader!");
            }
        }

        private bool ConfigurationPredicate(VideoInfo videoInfo, Resolution resolution, string extension)
        {
            return videoInfo.Resolution == (int)resolution && videoInfo.VideoExtension == extension;
        }

        private void OnDownloadStarted(object sender, EventArgs args)
        {
            progressBar.Next("STARTED");
        }

        private void OnDownloadFinished(object sender, EventArgs args)
        {
            progressBar.Next("FINISHED : " + videoDownloader.Video.Title);
        }

        private void OnProgressChanged(object sender, ProgressEventArgs progressEventArgs)
        {
            progressBar.Refresh((int)progressEventArgs.ProgressPercentage, $" {_TITLE} | Progress : ");
        }
    }
}