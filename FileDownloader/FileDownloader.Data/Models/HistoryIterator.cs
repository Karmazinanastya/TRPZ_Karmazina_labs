using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.Data.Models
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    public class HistoryIterator : IIterator<History>
    {
        private readonly List<History> _historyList;
        private int _position = 0;

        public HistoryIterator(List<History> historyList)
        {
            _historyList = historyList;
        }

        public bool HasNext()
        {
            return _position < _historyList.Count;
        }

        public History Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more elements");
            }
            return _historyList[_position++];
        }
    }
}
