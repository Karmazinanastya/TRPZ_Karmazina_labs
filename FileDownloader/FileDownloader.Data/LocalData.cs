using FileDownloader.Data.Models;

namespace FileDownloader.Data
{
    public static class LocalData
    {
        public static List<History> historyList = new List<History>
        {
            new History
            {
                Id = 1,
                FileName = "file_1.txt",
                FilePath = "/downloads/file_1.txt",
                SpeedPriority = 10,
                StartTime = DateTime.UtcNow,
                EndTime = null,
                DownloadedSize = 3677,
                TotalSize = 7125,
                CreatedAt = DateTime.UtcNow
            },
            new History
            {
                Id = 2,
                FileName = "file_2.txt",
                FilePath = "/downloads/file_2.txt",
                SpeedPriority = 5,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow,
                DownloadedSize = 892,
                TotalSize = 5679,
                CreatedAt = DateTime.UtcNow
            },
            new History
            {
                Id = 3,
                FileName = "file_3.txt",
                FilePath = "/downloads/file_3.txt",
                SpeedPriority = 2,
                StartTime = DateTime.UtcNow,
                EndTime = null,
                DownloadedSize = 3428,
                TotalSize = 7579,
                CreatedAt = DateTime.UtcNow
            },
            new History
            {
                Id = 4,
                FileName = "file_4.txt",
                FilePath = "/downloads/file_4.txt",
                SpeedPriority = 5,
                StartTime = DateTime.UtcNow,
                EndTime = null,
                DownloadedSize = 942,
                TotalSize = 9887,
                CreatedAt = DateTime.UtcNow
            },
            new History
            {
                Id = 5,
                FileName = "file_5.txt",
                FilePath = "/downloads/file_5.txt",
                SpeedPriority = 1,
                StartTime = DateTime.UtcNow,
                EndTime = null,
                DownloadedSize = 3635,
                TotalSize = 7419,
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
