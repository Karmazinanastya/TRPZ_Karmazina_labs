using DownloadManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DownloadManager.Data
{
    public class AppDbContext : DbContext
    {
        string connectionString = "Host=localhost;Port=5432;Database=YourDatabaseName;Username=YourUsername;Password=YourPassword";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<DownloadStatus> downloadStatuses { get; set; }
        public DbSet<DownloadHistory> downloadHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DownloadStatus>().HasData(
                new DownloadStatus { Id = 1, Name = "Pending" },
                new DownloadStatus { Id = 2, Name = "InProgress" },
                new DownloadStatus { Id = 3, Name = "Completed" },
                new DownloadStatus { Id = 4, Name = "Failed" }
            );

            modelBuilder.Entity<DownloadHistory>().HasData(
                new DownloadHistory
                {
                    ID = 1,
                    DownloadStatusId = 1, // "Pending"
                    FilePath = "/downloads/file1.txt",
                    SpeedLimit = 500,
                    StartTime = DateTime.UtcNow,
                    EndTime = null,
                    DownloadedSize = 0,
                    TotalSize = 1000,
                    CreatedAt = DateTime.UtcNow
                },
                new DownloadHistory
                {
                    ID = 2,
                    DownloadStatusId = 3, // "Completed"
                    FilePath = "/downloads/file2.txt",
                    SpeedLimit = 1000,
                    StartTime = DateTime.UtcNow.AddHours(-2),
                    EndTime = DateTime.UtcNow.AddHours(-1),
                    DownloadedSize = 2000,
                    TotalSize = 2000,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            );
        }
    }
}
