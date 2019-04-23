using System;

namespace VoidTime
{
    public class Time
    {
        private static TimeData data;
        private static bool IsCreated;

        public static float DeltaTime => data.DeltaTime;
        public static int FrameCount => data.FrameCount;
        public static float RealtimeSinceStartup => (float) (DateTime.Now - data.StartTime).TotalSeconds;

        public static void Create(TimeData data)
        {
            if (IsCreated)
                return;
            IsCreated = true;
            Time.data = data;
        }
    }
}