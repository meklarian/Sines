using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sines.Audio.Transforms
{
    public class SampleConversion
    {
        public static IEnumerable<short> To16Bit(IEnumerable<double> samples)
        {
            double max = short.MaxValue;
            double min = short.MinValue;
            foreach (double sample in samples)
            {
                if (sample > max)
                {
                    yield return short.MaxValue;
                }
                else
                {
                    if (sample < min)
                    {
                        yield return short.MinValue;
                    }
                    else
                    {
                        yield return (short)sample;
                    }
                }
            }
        }
    }
}
