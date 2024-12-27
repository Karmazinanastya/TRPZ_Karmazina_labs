using FileDownloader.Data.Models;
using System.Windows;

namespace FileDownloader.UI
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow(List<History> historyList)
        {
            InitializeComponent();
            HistoryDataGrid.ItemsSource = historyList;
        }
    }
}
