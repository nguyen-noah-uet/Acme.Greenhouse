using System;
using Volo.Abp;

namespace Acme.Greenhouse.Extensions
{
    public static class DoubleExtensions
    {
        public static double Smooth(this double x)
        {
            return Math.Round(x, 2);
            var now = DateTime.Now;
            var hour = now.Hour;
            var minute = now.Minute;
            var random = new Random(minute);

            if (hour is >= 8 and <= 17)
            {
                int a = (minute % 10) + 1;
                if (minute is >= 0 and < 10)
                {
                    x = 100 - 0.5 * a * random.NextDouble();
                }
                else if (minute is >= 10 and < 20)
                {
                    x = 96 - 0.4 * a * random.NextDouble();
                }
                else if (minute is >= 20 and < 30)
                {
                    x = 93 - 0.2 * a * random.NextDouble();
                }
                else if (minute is >= 30 and < 40)
                {
                    x = 90 - 0.5 * a * random.NextDouble();
                }
                else if (minute is >= 40 and < 50)
                {
                    x = 87 - 0.5 * a * random.NextDouble();
                }
                else if (minute is >= 50 and < 60)
                {
                    x = 84 - 0.2 * a * random.NextDouble();
                }
            }
            else if (hour is >= 18 and <= 23)
            {
                x = 82 - 0.5 * (hour - 18 + 1) * random.NextDouble();
            }
            else if (hour is >= 0 and <= 4)
            {
                x = 79 - 0.8 * (hour - 0 + 1) * random.NextDouble();
            }
            else if (hour is >= 5 and <= 7)
            {
                x = 86 - 0.5 * (hour - 5 + 1) * random.NextDouble();
            }

            return Math.Round(x, 2);
        }
    }
}
