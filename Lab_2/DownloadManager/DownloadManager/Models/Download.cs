using DownloadManager.Enums;

namespace DownloadManager.Models
{
    public class Download
    {
        public int ID { get; set; }
        public int DownloadStatusId { get; set; }
        public required string FilePath { get; set; }
        public int SpeedLimit { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long DownloadedSize { get; set; }
        public long TotalSize { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
