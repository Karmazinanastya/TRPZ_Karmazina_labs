using FileDownloader.UI.CompositePattern;
using System.Windows.Controls;

namespace FileDownloader.UI.Services
{
    public class DownloadItemsCollection
    {
        Dictionary<Button, DownloadListBoxItem> downloadToItems;
        Dictionary<Button, DownloadListBoxItem> cancelToItems;
        Dictionary<Button, DownloadListBoxItem> removeToItems;

        public DownloadItemsCollection()
        {
            downloadToItems = new Dictionary<Button, DownloadListBoxItem>();
            cancelToItems = new Dictionary<Button, DownloadListBoxItem>();
            removeToItems = new Dictionary<Button, DownloadListBoxItem>();
        }
        private readonly DownloadComposite _rootComposite = new();

        public void Add(DownloadListBoxItem item)
        {
            var leaf = new DownloadLeaf(item);
            CheckIfNotContains(item, item.DownloadButton, item.CancelButton, item.RemoveButton);
            downloadToItems.Add(item.DownloadButton, item);
            cancelToItems.Add(item.CancelButton, item);
            removeToItems.Add(item.RemoveButton, item);
            _rootComposite.Add(leaf);
        }

        public void Remove(Button removeButton)
        {
            CheckIfContains(removeToItems, removeButton);
            var item = GetByRemoveButton(removeButton);

            var leaf = new DownloadLeaf(item);
            _rootComposite.Remove(leaf);
            downloadToItems.Remove(item.DownloadButton);
            cancelToItems.Remove(item.CancelButton);
            removeToItems.Remove(removeButton);
        }

        public async Task ExecuteAllAsync()
        {
            await _rootComposite.ExecuteAsync();
        }

        public async Task UndoAllAsync()
        {
            await _rootComposite.UndoAsync();
        }
        public DownloadListBoxItem GetByDownloadButton(Button button)
        {
            CheckIfContains(downloadToItems, button);
            return downloadToItems[button];
        }

        public DownloadListBoxItem GetByCancelButton(Button button)
        {
            CheckIfContains(cancelToItems, button);
            return cancelToItems[button];
        }
        public DownloadListBoxItem GetByRemoveButton(Button button)
        {
            CheckIfContains(removeToItems, button);
            return removeToItems[button];
        }

        private void CheckIfContains(Dictionary<Button, DownloadListBoxItem> dict, Button button)
        {
            if (dict == null)
            {
                throw new NullReferenceException("Dictiony cannot be null");
            }
            if (button == null)
            {
                throw new ArgumentNullException("Given key cannot be null");
            }
            if (!dict.ContainsKey(button))
            {
                throw new KeyNotFoundException("Given key not found");
            }
        }

        private void CheckIfNotContains(DownloadListBoxItem item, Button downloadButton, Button cancelButton, Button removeButton)
        {
            if (downloadToItems == null || cancelToItems == null || removeToItems == null)
            {
                throw new NullReferenceException("Dictionary cannot be null");
            }
            if (item == null || downloadButton == null || cancelButton == null || removeButton == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }
            if (downloadToItems.ContainsKey(downloadButton)
                || cancelToItems.ContainsKey(cancelButton)
                || removeToItems.ContainsKey(removeButton))
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary");
            }

            if (downloadToItems.ContainsValue(item)
                || cancelToItems.ContainsValue(item)
                || removeToItems.ContainsValue(item))
            {
                throw new ArgumentException("An element with the same value already exists in the dictionary");
            }
        }
    }
}
