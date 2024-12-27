using FileDownloader.UI.FactoryMethod_SpeedStrategy;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows.Controls;

public class DownloadFacade
{
    private readonly SpeedStrategyFactory _strategyFactory;

    public DownloadFacade(SpeedStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public async Task DownloadFileAsync(
        string url,
        string filePath,
        int priority,
        ProgressBar progressBar,
        Label progressLabel,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
        {
            throw new ArgumentException("Invalid URL");
        }

        var speedStrategy = _strategyFactory.CreateSpeedStrategy(priority);
        int maxBytesPerSecond = speedStrategy.GetSpeedLimit();

        using (var client = new HttpClient())
        using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
        {
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync(cancellationToken))
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var buffer = new byte[8192];
                int bytesRead;
                var sw = new Stopwatch();

                progressBar.Value = 0;
                progressLabel.Content = "0%";

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    sw.Start();
                    await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);

                    sw.Stop();
                    var elapsed = sw.ElapsedMilliseconds;
                    var expectedTime = (bytesRead * 1000.0) / maxBytesPerSecond;
                    if (elapsed < expectedTime)
                    {
                        await Task.Delay((int)(expectedTime - elapsed), cancellationToken);
                    }
                    sw.Reset();

                    var progressPercentage = (int)((double)fileStream.Length / response.Content.Headers.ContentLength.Value * 100);
                    progressBar.Value = progressPercentage;
                    progressLabel.Content = $"{progressPercentage}%";
                }
            }
        }
    }
}
