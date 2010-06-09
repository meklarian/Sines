using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sines.Audio;

namespace TestDeclaration1
{
    class Program
    {
        static void MakeSine(string filename)
        {
            FluentStream F = new FluentStream(44100).Sine(5000d, 440).Sine(5000d, 880).Sine(5000d, 1760);
            F.SaveFile(filename, 16, 44100 * 4);
        }

        static void MakeSquare(string filename)
        {
            FluentStream F = new FluentStream(44100).Square(5000d, 440).Square(5000d, 880).Square(5000d, 1760);
            F.SaveFile(filename, 16, 44100 * 4);
        }

        static void MakeMixed(string filename)
        {
            FluentStream F = new FluentStream(44100).Square(3000d, 440).Square(3000d, 880).Sine(3000d, 220).Sine(3000, 110);
            F.SaveFile(filename, 16, 44100 * 4);
        }

        static void MakeQuietSine(string filename)
        {
            FluentStream F = new FluentStream(44100).Sine(5000d, 440).Sine(5000d, 880).Sine(5000d, 1760).ScaleVolume(.05);
            F.SaveFile(filename, 16, 44100 * 4);
        }

        static void MakeQuietSquare(string filename)
        {
            FluentStream F = new FluentStream(44100).Square(5000d, 440).Square(5000d, 880).Square(5000d, 1760).ScaleVolume(.05);
            F.SaveFile(filename, 16, 44100 * 4);
        }

        static void MakeQuietMixed(string filename)
        {
            FluentStream F = new FluentStream(44100).Square(3000d, 440).Square(3000d, 880).Sine(3000d, 220).Sine(3000, 110).ScaleVolume(.05);
            F.SaveFile(filename, 16, 44100 * 4);
        }

        static void Main(string[] args)
        {
            MakeSine("sine_440s.wav");
            MakeSquare("square_440s.wav");
            MakeMixed("mixed_440s.wav");
            MakeQuietSine("sine_440s_quiet.wav");
            MakeQuietSquare("square_440s_quiet.wav");
            MakeQuietMixed("mixed_440s_quiet.wav");
        }
    }
}
