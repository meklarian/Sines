using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sines.Audio.Transforms
{
    public class VolumeControl
    {
        public static IEnumerable<double> Scale(IEnumerable<double> source, double multiplier)
        {
            foreach (double sample in source)
            {
                yield return sample * multiplier;
            }
        }

        public static IEnumerable<double> Truncate(IEnumerable<double> source, double min, double max)
        {
            foreach (double sample in source)
            {
                yield return (sample > max ? max : (sample < min ? min : sample));
            }
        }
    }
}
