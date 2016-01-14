﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;

namespace ActionVisualizer
{
    public class SineWaveProvider32 : WaveProvider32
    {
        int sample;

        public SineWaveProvider32()
        {
            Frequency = 1000;
            Amplitude = 0.25f; // let's not hurt our ears            
        }
        
        // Create a Sine Tone with the specified parameters.
        public SineWaveProvider32(float f, float a, int samplerate, int channels)
        {
            this.SetWaveFormat(samplerate, channels);
            Frequency = f;
            Amplitude = a; // let's not hurt our ears            
        }

        public float Frequency { get; set; }
        public float Amplitude { get; set; }

        // Accoring to NAudio standard, fill a buffer (in WAV format) to be played. Check NAudio documentation for more details about
        // small details like how the offset is provided.
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n + offset] = (float)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                sample++;
                if (sample >= sampleRate) sample = 0;
            }
            return sampleCount;
        }
    }
}
