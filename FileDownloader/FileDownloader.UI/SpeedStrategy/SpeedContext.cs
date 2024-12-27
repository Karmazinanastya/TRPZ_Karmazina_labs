

namespace FileDownloader.UI.SpeedPriorityStrategy
{
    public class HighSpeedStrategy : ISpeedStrategy
    {
        public int GetSpeedLimit() => 5000 * 1024; // 5000 KB/s
    }

    public class GoodSpeedStrategy : ISpeedStrategy
    {
        public int GetSpeedLimit() => 3000 * 1024; // 3000 KB/s
    }

    public class MediumSpeedStrategy : ISpeedStrategy
    {
        public int GetSpeedLimit() => 1000 * 1024; // 1000 KB/s
    }

    public class LowSpeedStrategy : ISpeedStrategy
    {
        public int GetSpeedLimit() => 500 * 1024; // 500 KB/s
    }
}
