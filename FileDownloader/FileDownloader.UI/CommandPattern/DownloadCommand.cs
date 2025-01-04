using FileDownloader.Data.Models;
using FileDownloader.UI.Services;


namespace FileDownloader.UI.CommandPattern
{
    public interface ICommand
    {
        Task ExecuteAsync();
        Task UndoAsync();
    }
    public class DownloadCommand : ICommand
    {
        private readonly DownloadListBoxItem _item;

        public DownloadCommand(DownloadListBoxItem item)
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

    public class RemoveCommand : ICommand
    {
        private readonly DownloadItemsCollection _itemsCollection;
        private readonly DownloadListBoxItem _item;

        public RemoveCommand(DownloadItemsCollection itemsCollection, DownloadListBoxItem item)
        {
            _itemsCollection = itemsCollection;
            _item = item;
        }

        public Task ExecuteAsync()
        {
            _itemsCollection.Remove(_item.RemoveButton);
            return Task.CompletedTask;
        }

        public Task UndoAsync()
        {
            _itemsCollection.Add(_item);
            return Task.CompletedTask;
        }
    }

    public class ViewHistoryCommand : ICommand
    {
        private readonly List<History> _historyList;

        public ViewHistoryCommand(List<History> historyList)
        {
            _historyList = historyList;
        }

        public Task ExecuteAsync()
        {
            var historyWindow = new HistoryWindow(_historyList);
            historyWindow.Show();
            return Task.CompletedTask;
        }

        public Task UndoAsync()
        {
            // Немає необхідності в Undo
            return Task.CompletedTask;
        }
    }

    public class CommandInvoker
    {
        private readonly Stack<ICommand> _commandHistory = new();

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await command.ExecuteAsync();
            _commandHistory.Push(command);
        }

        public async Task UndoLastCommandAsync()
        {
            if (_commandHistory.Count > 0)
            {
                var command = _commandHistory.Pop();
                await command.UndoAsync();
            }
        }
    }




}
