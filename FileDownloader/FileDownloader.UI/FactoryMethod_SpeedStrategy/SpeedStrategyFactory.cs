using FileDownloader.UI.Flyweight;
using FileDownloader.UI.SpeedPriorityStrategy;


namespace FileDownloader.UI.FactoryMethod_SpeedStrategy
{
    public abstract class SpeedStrategyFactory
    {
        protected readonly SpeedStrategyFlyweightFactory FlyweightFactory = new();

        public abstract ISpeedStrategy CreateSpeedStrategy(int priority);
    }

    public class DefaultSpeedStrategyFactory : SpeedStrategyFactory
    {
        public override ISpeedStrategy CreateSpeedStrategy(int priority)
        {
            return FlyweightFactory.GetSpeedStrategy(priority);
        }
    }
}
