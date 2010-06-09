using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sines.Audio.Interfaces;

namespace Sines.Audio.WaveForms
{
    public class Square : IMonoSampleSource
    {
        public Square(int initSampleRate, double initAmplitude, double initFrequency)
        {
            if (initFrequency <= 0.0f) { throw new ArgumentOutOfRangeException("initFrequency", "This parameter should be a value greater than zero"); }
            frequency = initFrequency;
            sampleRate = initSampleRate;
            amplitude = initAmplitude;
        }

        public double frequency { get; protected set; }
        public int sampleRate { get; protected set; }
        public double amplitude { get; protected set; }

        public IEnumerable<double> GetSamples()
        {
            long period = (long)(((double)sampleRate) / frequency);
            long halffreq = period / 2;
            long sample = 0;
            double amp = amplitude;
            do
            {
                if (sample < halffreq)
                {
                    yield return amp;
                }
                else
                {
                    yield return -amp;
                }

                sample++;
                sample = sample % period;
            }
            while (true);
        }
    }
}
