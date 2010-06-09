using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sines.Audio.Interfaces;

namespace Sines.Audio.WaveForms
{
    public class Sine : IMonoSampleSource
    {
        public Sine(int initSampleRate, double initAmplitude, double initFrequency)
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
            double period = ((double)this.sampleRate) / this.frequency;
            long sample = 0;
            long samplePeriod = (long)period;
            double amp = this.amplitude;
            do
            {
                double radians = (double)sample * Math.PI * 2.0d / period;
                yield return (amp * Math.Sin(radians));

                sample++;
                sample = sample % samplePeriod;
            }
            while (true);
        }
    }
}
