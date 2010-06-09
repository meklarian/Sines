using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sines.Audio.Transforms
{
    public class LinearMixer
    {
        public static IEnumerable<double> Mix(IEnumerable<double> alpha, IEnumerable<double> beta, double divisor)
        {
            return alpha.Zip(beta, (a, b) => { return MixSample(a, b, divisor); });
        }

        public static double MixSample(double a, double b, double divisor)
        {
            return (a + b) / divisor;
        }
    }
}
