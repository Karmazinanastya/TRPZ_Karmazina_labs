
namespace DownloadManager.Models
{
    public class DownloadStatistics
    {
        public int ID { get; set; }
        public int DownloadID { get; set; }
        public int TotalDownloads { get; set; }

        public Download Download { get; set; }
    }
}
