using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Sines.Audio.Outputs
{
    public class WavePCM
    {
        public WavePCM(ushort initChannels, uint initSampleRate, ushort initBitsPerSample, uint initSampleCount)
        {
            RiffChunkID = new char[4] { 'R', 'I', 'F', 'F' };
            RiffChunkSize = 36;
            Format = new char[4] { 'W', 'A', 'V', 'E' };
            FormatChunkID = new char[4] { 'f', 'm', 't', ' ' };
            FormatChunkSize = 16;
            AudioFormat = 1;
            NumChannels = initChannels;
            SampleRate = initSampleRate;
            BitsPerSample = initBitsPerSample;
            ByteRate = SampleRate * (uint)NumChannels * BitsPerSample / 8;
            BlockAlign = (ushort)(NumChannels * BitsPerSample / 8);
            DataChunkID = new char[4] { 'd', 'a', 't', 'a' };
            DataChunkSize = 0;

            // Reserve Space in Header
            DataChunkSize = initSampleCount * NumChannels * BitsPerSample / 8;
            RiffChunkSize = DataChunkSize + 36;

            // Reference Items
            _SampleCount = initSampleCount;
        }

        // Serialized Header Items
        char[] RiffChunkID;
        uint RiffChunkSize;
        char[] Format;
        char[] FormatChunkID;
        uint FormatChunkSize;
        ushort AudioFormat;
        ushort NumChannels;
        uint SampleRate;
        uint ByteRate;
        ushort BlockAlign;
        ushort BitsPerSample;
        char[] DataChunkID;
        uint DataChunkSize;
        // Non-Serialized Header Items
        uint _SampleCount;

        public void StoreSamples(string filename, IEnumerable<short> samples)
        {
            using (FileStream fs = File.Create(filename))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    SerializeSamples(writer, samples);
                    writer.Close();
                }
                fs.Close();
            }
        }

        protected void SerializeHeader(BinaryWriter writer)
        {
            writer.Write(RiffChunkID);
            writer.Write(RiffChunkSize);
            writer.Write(Format);
            writer.Write(FormatChunkID);
            writer.Write(FormatChunkSize);
            writer.Write(AudioFormat);
            writer.Write(NumChannels);
            writer.Write(SampleRate);
            writer.Write(ByteRate);
            writer.Write(BlockAlign);
            writer.Write(BitsPerSample);
            writer.Write(DataChunkID);
            writer.Write(DataChunkSize);
        }

        public void SerializeSamples(BinaryWriter writer, IEnumerable<short> samples)
        {
            SerializeHeader(writer);

            Debug.Assert(BitsPerSample == 16);
            Debug.Assert(NumChannels == 1);

            SampleWriter.SerializeSamples(samples, writer, (int)_SampleCount);
        }

        public void SerializeSamples(BinaryWriter writer, IEnumerable<Tuple<short, short>> samples)
        {
            SerializeHeader(writer);

            Debug.Assert(BitsPerSample == 16);
            Debug.Assert(NumChannels == 2);

            SampleWriter.SerializeSamples(samples, writer, (int)_SampleCount);
        }
    }
}
