using FileDownloader.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDownloader.UI.CompositePattern
{
    public interface IDownloadComponent
    {
        Task ExecuteAsync();
        Task UndoAsync();
    }

    public class DownloadLeaf : IDownloadComponent
    {
        private readonly DownloadListBoxItem _item;

        public DownloadLeaf(DownloadListBoxItem item)
        {
            _item = item;
        }

        public async Task ExecuteAsync()
        {
            await _item.DownloadAsync();
        }

        public Task UndoAsync()
        {
            _item.TokenSource?.Cancel();
            return Task.CompletedTask;
        }
    }

    public class DownloadComposite : IDownloadComponent
    {
        private readonly List<IDownloadComponent> _children = new();

        public void Add(IDownloadComponent component)
        {
            _children.Add(component);
        }

        public void Remove(IDownloadComponent component)
        {
            _children.Remove(component);
        }

        public async Task ExecuteAsync()
        {
            foreach (var child in _children)
            {
                await child.ExecuteAsync();
            }
        }

        public async Task UndoAsync()
        {
            foreach (var child in _children)
            {
                await child.UndoAsync();
            }
        }
    }
}
