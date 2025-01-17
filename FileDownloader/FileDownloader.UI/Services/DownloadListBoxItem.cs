using FileDownloader.Data;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using FileDownloader.UI.ObserverPattern;
using FileDownloader.UI.TemplateMethod;

namespace FileDownloader.UI.Services
{
    public class DownloadListBoxItem
    {
        public Button DownloadButton { get; set; }
        public Button CancelButton { get; set; }
        public Button RemoveButton { get; set; }
        public TextBox DownloadTextBox { get; set; }
        public ProgressBar DownloadProgressBar { get; set; }
        public CancellationTokenSource TokenSource { get; set; }
        public Border DownloadBorder { get; set; }
        public StackPanel DownloadPanel { get; set; }
        public Label ProgressLabel { get; set; }
        public ComboBox PriorityComboBox { get; set; }

        public DownloadListBoxItem(int width)
        {
            DownloadBorder = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2)
            };

            DownloadPanel = new StackPanel
            {
                Width = width * .95,
                Height = 100,
            };

            DownloadTextBox = new TextBox
            {
                Width = width * 0.9,
                Height = 30,
                Margin = new Thickness(5, 10, 0, 0),
            };

            DownloadProgressBar = new ProgressBar
            {
                Width = DownloadTextBox.Width * .9,
                Height = 30,
                Margin = new Thickness(10, 10, 10, 0),
                Visibility = Visibility.Collapsed
            };

            ProgressLabel = new Label
            {
                Content = "0%",
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10, 0, 10, 0),
                Visibility = Visibility.Collapsed
            };

            RemoveButton = new Button
            {
                Margin = new Thickness(10, 10, 10, 10),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = "Remove",
                Background = Brushes.Red,
                Foreground = Brushes.White,
            };

            CancelButton = new Button
            {
                Margin = new Thickness(10, 0, 10, 10),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "Cancel",
                Background = Brushes.Gray,
                Foreground = Brushes.White,
                Visibility = Visibility.Collapsed
            };

            PriorityComboBox = new ComboBox
            {
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10, 0, 10, 0),
            };

            PriorityComboBox.Items.Add(new ComboBoxItem { Content = "High", Tag = 1 });
            PriorityComboBox.Items.Add(new ComboBoxItem { Content = "Good", Tag = 2 });
            PriorityComboBox.Items.Add(new ComboBoxItem { Content = "Medium", Tag = 3 });
            PriorityComboBox.Items.Add(new ComboBoxItem { Content = "Low", Tag = 4 });
            PriorityComboBox.SelectedIndex = 1; // Default to Medium

            DownloadButton = new Button
            {
                Margin = new Thickness(10, 10, 10, 10),
                Width = 100,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = "Download",
                Background = Brushes.DodgerBlue,
                Foreground = Brushes.White,
            };

            var stackPanel = new StackPanel();
            stackPanel.Children.Add(DownloadProgressBar);
            stackPanel.Children.Add(ProgressLabel);

            var grid = new Grid();
            grid.Children.Add(DownloadButton);
            grid.Children.Add(PriorityComboBox);
            grid.Children.Add(RemoveButton);

            DownloadBorder.Child = DownloadPanel;

            DownloadPanel.Children.Add(DownloadTextBox);
            DownloadPanel.Children.Add(grid);
            DownloadPanel.Children.Add(stackPanel);
            DownloadPanel.Children.Add(CancelButton);
        }

        private void ShowDownloadStuff()
        {
            CancelButton.Visibility = Visibility.Collapsed;
            DownloadProgressBar.Visibility = Visibility.Collapsed;
            ProgressLabel.Visibility = Visibility.Collapsed;
            DownloadButton.Visibility = Visibility.Visible;
            DownloadTextBox.Visibility = Visibility.Visible;
            RemoveButton.Visibility = Visibility.Visible;
            PriorityComboBox.Visibility = Visibility.Visible;
        }

        private void HideDownloadStuff()
        {
            CancelButton.Visibility = Visibility.Visible;
            DownloadProgressBar.Visibility = Visibility.Visible;
            ProgressLabel.Visibility = Visibility.Visible;
            DownloadButton.Visibility = Visibility.Collapsed;
            DownloadTextBox.Visibility = Visibility.Collapsed;
            RemoveButton.Visibility = Visibility.Collapsed;
            PriorityComboBox.Visibility = Visibility.Collapsed;
        }

        public async Task DownloadAsync()
        {
            var dlg = new SaveFileDialog
            {
                Filter = "All files (*.*)|*.*"
            };

            var dialogResult = dlg.ShowDialog();

            if (dialogResult == true)
            {
                var filePath = dlg.FileName;

                if (!Uri.IsWellFormedUriString(DownloadTextBox.Text, UriKind.Absolute))
                {
                    MessageBox.Show("Invalid URI, please enter a valid one.");
                    return;
                }

                TokenSource = new CancellationTokenSource();

                var downloadManager = new DownloadManager();
                downloadManager.Attach(new ProgressBarObserver(DownloadProgressBar));
                downloadManager.Attach(new LabelObserver(ProgressLabel));

                var downloader = new HttpFileDownloader(DownloadTextBox.Text, filePath, downloadManager, TokenSource);

                HideDownloadStuff();

                await downloader.DownloadFileAsync();

                ShowDownloadStuff();
            }
        }


    }
}
