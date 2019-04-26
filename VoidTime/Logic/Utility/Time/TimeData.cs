using System;
using System.Diagnostics;

namespace VoidTime
{
    public class TimeData
    {
        private readonly Stopwatch timer = new Stopwatch();
        public float DeltaTime { get; set; }
        public int FrameCount { get; set; }
        public DateTime StartTime { get; set; }

        public TimeData()
        {
            StartTime = DateTime.Now;
        }

        public void Update()
        {
            timer.Stop();
            FrameCount++;
            DeltaTime = timer.ElapsedMilliseconds;
            timer.Restart();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Reset();
        }
    }
}