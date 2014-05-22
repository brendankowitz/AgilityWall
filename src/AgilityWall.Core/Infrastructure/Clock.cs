using System;

namespace AgilityWall.Core.Infrastructure
{
    public class Clock
    {
        public static Func<DateTimeOffset> Now = () => DateTimeOffset.Now;
    }
}