using System;

namespace NumbersGenerator.Timers
{
    public interface ILevelTimer
    {
        event EventHandler OnValueHit;

        void Initialize(int initalLevelTicks, int levelPeriod, int maxLevel);

        void Start();

        void Dispose();

        void Stop();

        void Reset();
    }
}
