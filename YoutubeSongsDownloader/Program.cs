using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;

namespace YoutubeSongsDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the PATH of file with list of Youtube videos' urls:\n");
            var youtubeUrlsFilePath = Console.ReadLine();
            if (!File.Exists(youtubeUrlsFilePath))
            {
                Console.WriteLine("File does not exist");
                return;
            }
            var videoUrls = File.ReadAllLines(youtubeUrlsFilePath);

            var downloadDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Downloaded", Path.GetFileNameWithoutExtension(youtubeUrlsFilePath));
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }

            var logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Downloaded", "log.txt");
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath);
            }

            //using (var logFile = new StreamWriter(logFilePath, true))
            using (var logFile = new ConsoleFileLogger(logFilePath, true))
            {
                var youtube = YouTube.Default;
                var downloadedMp3s = 0;

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                Parallel.ForEach(videoUrls, (videoUrl) =>
                //foreach (var videoUrl in videoUrls)
                {
                    try
                    {
                        DownloadMp3(videoUrl, downloadDirectory, youtube);
                        ++downloadedMp3s;
                    }
                    catch (Exception ex)
                    {
                        logFile.WriteLine("Error processing: " + videoUrl);
                        logFile.WriteLine(ex);
                    }
                }
                );

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;
                string elapsedTime = string.Format(
                    "{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours,
                    ts.Minutes,
                    ts.Seconds,
                    ts.Milliseconds / 10);
                logFile.WriteLine("RunTime: " + elapsedTime);
                logFile.WriteLine("Downloaded: " + downloadedMp3s);
                logFile.WriteLine("Error with: " + (videoUrls.Length - downloadedMp3s));
            }
        }

        private static void DownloadMp3(string videoUrl, string downloadDirectory, YouTube youtube)
        {
            var video = youtube.GetVideo(videoUrl);
            var videoDestination = Path.Combine(downloadDirectory, video.FullName);
            var audioDestination = Path.Combine(downloadDirectory, Path.GetFileNameWithoutExtension(video.FullName) + ".mp3");

            File.WriteAllBytes(videoDestination, video.GetBytes());

            var videoFile = new MediaFile { Filename = videoDestination };
            var audioFile = new MediaFile { Filename = audioDestination };

            using (var engine = new Engine())
            {
                engine.GetMetadata(videoFile);

                engine.Convert(videoFile, audioFile);

                File.Delete(videoDestination);
            }
        }
    }
}
