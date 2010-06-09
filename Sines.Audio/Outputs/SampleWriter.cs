using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sines.Audio.Outputs
{
    public class SampleWriter
    {
        public static void SerializeSamples(IEnumerable<short> samples, BinaryWriter writer)
        {
            foreach (short sample in samples)
            {
                writer.Write(sample);
            }
        }

        public static void SerializeSamples(IEnumerable<Tuple<short, short>> samples, BinaryWriter writer)
        {
            foreach (Tuple<short, short> sample in samples)
            {
                writer.Write(sample.Item1);
                writer.Write(sample.Item2);
            }
        }

        public static void SerializeSamples(IEnumerable<short> samples, BinaryWriter writer, int sampleCount)
        {
            int i = 0;
            foreach (short sample in samples)
            {
                if (i >= sampleCount) { break; }
                writer.Write(sample);
                i++;
            }
        }

        public static void SerializeSamples(IEnumerable<Tuple<short, short>> samples, BinaryWriter writer, int sampleCount)
        {
            int i = 0;
            foreach (Tuple<short, short> sample in samples)
            {
                if (i >= sampleCount) { break; }
                writer.Write(sample.Item1);
                writer.Write(sample.Item2);
                i++;
            }
        }
    }
}
