using System;
using System.Timers;

namespace NumbersGenerator.Timers
{
    public abstract class LevelTimer : IDisposable, ILevelTimer
    {
        public event EventHandler OnValueHit;

        protected int MaxLevel { get; private set; }
        protected int LevelPeriod { get; private set; }
        protected int InitialLevelTicks { get; private set; }
        protected int Level { get; private set; }

        private Timer _levelTimer;
        private Timer _valueTimer;

        public void Initialize(int initalLevelTicks, int levelPeriod, int maxLevel)
        {
            LevelPeriod = levelPeriod;
            InitialLevelTicks = initalLevelTicks;
            MaxLevel = maxLevel;

            Initialize();
        }

        public void Start()
        {
            _levelTimer.Start();
            _valueTimer.Start();
        }

        public void Dispose()
        {
            _valueTimer.Dispose();
            _levelTimer.Dispose();
        }

        public void Stop()
        {
            _valueTimer.Stop();
            _levelTimer.Stop();
        }

        public void Reset()
        {
            Level = 1;
        }

        protected abstract int GetCurrentLevelTicks();

        private void ValueTimerCallback(object sender, ElapsedEventArgs e)
        {
            OnValueHit.Invoke(null, null);
        }

        private void LevelTimerCallback(object sender, ElapsedEventArgs e)
        {
            Level++;
            if (Level > MaxLevel)
            {
                Stop();
                return;
            }

            _valueTimer.Interval = LevelPeriod / GetCurrentLevelTicks();
        }

        private void Initialize()
        {
            Reset();

            _levelTimer = new Timer(LevelPeriod);
            _levelTimer.Elapsed += LevelTimerCallback;

            _valueTimer = new Timer(LevelPeriod / GetCurrentLevelTicks());
            _valueTimer.Elapsed += ValueTimerCallback;
        }
    }
}