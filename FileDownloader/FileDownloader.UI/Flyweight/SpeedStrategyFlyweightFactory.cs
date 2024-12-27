using FileDownloader.UI.SpeedPriorityStrategy;

namespace FileDownloader.UI.Flyweight
{
    public class SpeedStrategyFlyweightFactory
    {
        private readonly Dictionary<int, ISpeedStrategy> _strategies = new();

        public ISpeedStrategy GetSpeedStrategy(int priority)
        {
            if (!_strategies.ContainsKey(priority))
            {
                _strategies[priority] = priority switch
                {
                    1 => new HighSpeedStrategy(),
                    2 => new GoodSpeedStrategy(),
                    3 => new MediumSpeedStrategy(),
                    4 => new LowSpeedStrategy(),
                    _ => throw new ArgumentException("Invalid priority"),
                };
            }
            return _strategies[priority];
        }
    }
}
