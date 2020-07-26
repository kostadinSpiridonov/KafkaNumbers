using System;

namespace NumbersGenerator.Timers
{
    public class DegreeLevelTimer : LevelTimer
    {
        protected override int GetCurrentLevelTicks()
        {
            return (int)Math.Pow(InitialLevelTicks, Level);
        }
    }
}
