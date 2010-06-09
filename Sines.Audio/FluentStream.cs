using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sines.Audio.Interfaces;
using Sines.Audio.WaveForms;
using Sines.Audio.Transforms;
using Sines.Audio.Outputs;

namespace Sines.Audio
{
    public class FluentStream
    {
        public FluentStream(int initSampleRate)
        {
            monoStream = null;
            sampleRate = initSampleRate;
        }

        int sampleRate;
        protected IEnumerable<double> monoStream;

        public FluentStream Sine(double amplitude, double frequency)
        {
            if (object.ReferenceEquals(monoStream, null))
            {
                monoStream = new Sine(sampleRate, amplitude, frequency).GetSamples();
            }
            else
            {
                monoStream = LinearMixer.Mix(monoStream, new Sine(sampleRate, amplitude, frequency).GetSamples(), 1);
            }
            return this;
        }

        public FluentStream Square(double amplitude, double frequency)
        {
            monoStream = new Square(sampleRate, amplitude, frequency).GetSamples();
            return this;
        }

        public FluentStream ScaleVolume(double multiplier)
        {
            monoStream = VolumeControl.Scale(monoStream, multiplier);
            return this;
        }

        public FluentStream TruncateVolume(double min, double max)
        {
            monoStream = VolumeControl.Truncate(monoStream, min, max);
            return this;
        }

        public FluentStream Mix(FluentStream alpha, FluentStream beta, double divisor)
        {
            monoStream = LinearMixer.Mix(alpha.monoStream, beta.monoStream, divisor);
            return this;
        }

        public void SaveFile(string filename, int bitsPerSample, int sampleCount)
        {
            WavePCM pcm = null;
            
            switch (bitsPerSample)
            {
                case 16:
                    pcm = new WavePCM(1, (uint)sampleRate, (ushort)bitsPerSample, (uint)sampleCount);
                    pcm.StoreSamples(filename, SampleConversion.To16Bit(monoStream));
                    break;
            }
        }
    }
}
