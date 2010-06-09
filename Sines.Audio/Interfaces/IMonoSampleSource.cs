using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sines.Audio.Interfaces
{
    public interface IMonoSampleSource
    {
        IEnumerable<double> GetSamples();
    }
}
