using DownloadManager.Data;
using DownloadManager.Models;

namespace DownloadManager.Repositories
{
    public class DownloadHistoryRepository(AppDbContext dbContext) : RepositoryBase<DownloadHistory>(dbContext), IDownloadHistoryRepository
    {
    }
}