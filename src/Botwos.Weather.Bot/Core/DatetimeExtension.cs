using System;
using System.Linq;

namespace Botwos.Weather.Bot.Core
{
    static public class DatetimeExtension
    {
        static public int[] GetRandomShortCodeIds(this DateTime time)
        {
            var ticks = time.Ticks.ToString().ToArray();
            return new[]
            {
                Convert.ToInt32(ticks[ticks.Length - 1].ToString()),
                Convert.ToInt32(ticks[ticks.Length - 2].ToString()),
                Convert.ToInt32(ticks[ticks.Length - 3].ToString())
            };
        }
    }
}