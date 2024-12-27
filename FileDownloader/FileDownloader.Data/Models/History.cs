namespace FileDownloader.Data.Models;

public class History
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int SpeedPriority { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public long DownloadedSize { get; set; }
    public long TotalSize { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
