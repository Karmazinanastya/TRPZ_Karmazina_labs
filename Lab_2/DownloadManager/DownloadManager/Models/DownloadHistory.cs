using DownloadManager.Enums;


namespace DownloadManager.Models
{
    public class DownloadHistory
    {
        public int ID { get; set; }
        public int DownloadID { get; set; }
        public DateTime Date { get; set; }
        public DownloadStatus Status { get; set; }

        public Download Download { get; set; }
    }
}
