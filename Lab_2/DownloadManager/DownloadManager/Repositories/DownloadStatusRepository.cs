using DownloadManager.Data;
using DownloadManager.Models;

namespace DownloadManager.Repositories
{
    public class DownloadStatusRepository(AppDbContext dbContext) : RepositoryBase<DownloadStatus>(dbContext), IDownloadStatusRepository
    {
    }
}