using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace FileDownloader.UI.ObserverPattern
{
    public interface IObserver
    {
        void Update(DownloadState state);
    }

    public class DownloadState
    {
        public double Progress { get; set; }
        public string StatusMessage { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCanceled { get; set; }
    }

    public class DownloadManager
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Detach(IObserver observer) => _observers.Remove(observer);

        public void Notify(DownloadState state)
        {
            foreach (var observer in _observers)
            {
                observer.Update(state);
            }
        }
    }

    public class ProgressBarObserver : IObserver
    {
        private ProgressBar _progressBar;

        public ProgressBarObserver(ProgressBar progressBar)
        {
            _progressBar = progressBar;
        }

        public void Update(DownloadState state)
        {
            _progressBar.Value = state.Progress;
            _progressBar.Visibility = state.IsCompleted || state.IsCanceled ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class LabelObserver : IObserver
    {
        private Label _label;

        public LabelObserver(Label label)
        {
            _label = label;
        }

        public void Update(DownloadState state)
        {
            _label.Content = state.StatusMessage;
        }
    }
}
