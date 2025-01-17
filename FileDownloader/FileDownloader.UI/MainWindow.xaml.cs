using FileDownloader.Data;
using FileDownloader.UI.CommandPattern;
using FileDownloader.UI.Services;
using System.Windows;

using Button = System.Windows.Controls.Button;


namespace FileDownloader.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DownloadItemsCollection items;
        private readonly CommandInvoker invoker;

        public MainWindow()
        {
            InitializeComponent();
            items = new DownloadItemsCollection();
            invoker = new CommandInvoker();
        }

        private async void AddNewDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadListBoxItem item = new DownloadListBoxItem((int)(DownloadsListBox.Width * .95));
            item.CancelButton.Click += CancelButton_Click;
            item.DownloadButton.Click += Download_ClickAsync;
            item.RemoveButton.Click += RemoveButton_Click;

            DownloadsListBox.Items.Add(item.DownloadBorder);
            items.Add(item);
        }

        private async void ViewHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var history = new HistoryCollection();
            var viewHistoryCommand = new ViewHistoryCommand(history.GetAll());
            await invoker.ExecuteCommandAsync(viewHistoryCommand);
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var removeButton = (Button)sender;

            var item = items.GetByRemoveButton(removeButton);
            var removeCommand = new RemoveCommand(items, item);
            await invoker.ExecuteCommandAsync(removeCommand);

            DownloadsListBox.Items.Remove(item.DownloadBorder);
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var cancelButton = (Button)sender;
            var item = items.GetByCancelButton(cancelButton);

            var cancelCommand = new DownloadCommand(item);
            await invoker.ExecuteCommandAsync(cancelCommand);
        }

        private async void Download_ClickAsync(object sender, RoutedEventArgs e)
        {
            var downloadButton = (Button)sender;
            var item = items.GetByDownloadButton(downloadButton);

            var downloadCommand = new DownloadCommand(item);
            await invoker.ExecuteCommandAsync(downloadCommand);
        }

        private async void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            await invoker.UndoLastCommandAsync();
        }

        private async void ExecuteAllDownloads_Click(object sender, RoutedEventArgs e)
        {
            await items.ExecuteAllAsync();
        }

        private async void UndoAllDownloads_Click(object sender, RoutedEventArgs e)
        {
            await items.UndoAllAsync();
        }
    }
}