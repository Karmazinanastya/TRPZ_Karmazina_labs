using FileDownloader.UI.ObserverPattern;
using FileDownloader.UI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileDownloader.UI.TemplateMethod
{
    public abstract class BaseDownloader
    {
        protected string Url { get; }
        protected string SavePath { get; }
        protected DownloadManager DownloadManager { get; }
        protected CancellationTokenSource TokenSource { get; }

        protected BaseDownloader(string url, string savePath, DownloadManager downloadManager, CancellationTokenSource tokenSource)
        {
            Url = url;
            SavePath = savePath;
            DownloadManager = downloadManager;
            TokenSource = tokenSource;
        }

        public async Task DownloadFileAsync()
        {
            if (!ValidateUrl())
            {
                MessageBox.Show("Invalid URI, please enter a valid one.");
                return;
            }

            try
            {
                OnPrepareDownload();
                await PerformDownloadAsync();
                OnFinalizeDownload();
            }
            catch (TaskCanceledException)
            {
                OnDownloadCanceled();
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
            finally
            {
                Cleanup();
            }
        }

        protected virtual void OnPrepareDownload()
        {
            DownloadManager.Notify(new DownloadState { Progress = 0, StatusMessage = "Preparing download..." });
        }

        protected abstract Task PerformDownloadAsync();

        protected virtual void OnFinalizeDownload()
        {
            DownloadManager.Notify(new DownloadState { Progress = 100, StatusMessage = "Download completed!", IsCompleted = true });
        }

        protected virtual void OnDownloadCanceled()
        {
            DownloadManager.Notify(new DownloadState { StatusMessage = "Download canceled", IsCanceled = true });
        }

        protected virtual void OnError(Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected virtual void Cleanup()
        {
            TokenSource.Dispose();
        }

        protected virtual bool ValidateUrl()
        {
            return Uri.IsWellFormedUriString(Url, UriKind.Absolute);
        }
    }



    public class HttpFileDownloader : BaseDownloader
    {
        public HttpFileDownloader(string url, string savePath, DownloadManager downloadManager, CancellationTokenSource tokenSource)
            : base(url, savePath, downloadManager, tokenSource)
        {
        }

        protected override async Task PerformDownloadAsync()
        {
            using var client = new HttpClientWithProgress(Url, SavePath, TokenSource.Token);

            // Реєстрація скасування
            TokenSource.Token.Register(() => client.CancelPendingRequests());

            client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DownloadManager.Notify(new DownloadState
                    {
                        Progress = progressPercentage ?? 0,
                        StatusMessage = "Downloading..."
                    });
                });
            };

            await client.StartDownloadAsync();
        }

        protected override void OnFinalizeDownload()
        {
            base.OnFinalizeDownload();

            var result = MessageBox.Show($"Download completed!\nDo you want to open the file?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start(SavePath);
            }
        }

        protected override void OnDownloadCanceled()
        {
            base.OnDownloadCanceled();

            var result = MessageBox.Show($"Download canceled!\nDo you want to delete the file?", "Canceled", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes && File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
        }
    }


}
