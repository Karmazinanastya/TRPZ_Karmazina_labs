using FileDownloader.Data.Models;

namespace FileDownloader.UI.HistoryBuilders
{
    public class HistoryBuilder
    {
        private History _history;

        public HistoryBuilder()
        {
            _history = new History();
        }

        public HistoryBuilder SetId(int id)
        {
            _history.Id = id;
            return this;
        }

        public HistoryBuilder SetFileName(string fileName)
        {
            _history.FileName = fileName;
            return this;
        }

        public HistoryBuilder SetFilePath(string filePath)
        {
            _history.FilePath = filePath;
            return this;
        }

        public HistoryBuilder SetSpeedPriority(int speedPriority)
        {
            _history.SpeedPriority = speedPriority;
            return this;
        }

        public HistoryBuilder SetStartTime(DateTime startTime)
        {
            _history.StartTime = startTime;
            return this;
        }

        public HistoryBuilder SetEndTime(DateTime endTime)
        {
            _history.EndTime = endTime;
            return this;
        }

        public HistoryBuilder SetCreatedAt(DateTime createdAt)
        {
            _history.CreatedAt = createdAt;
            return this;
        }

        public History Build()
        {
            return _history;
        }
    }
}
