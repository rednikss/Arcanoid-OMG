using System;

namespace App.Scripts.Architecture.Scene.Timer
{
    public class TimerStateInfo
    {
        public double LastTotalSeconds;
        public uint TimerSecondsTime;

        public TimerStateInfo()
        {
            LastTotalSeconds = (DateTime.MinValue - DateTime.Now).TotalSeconds;
            TimerSecondsTime = 0;
        }
    }
}