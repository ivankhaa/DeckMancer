using OpenTK.Audio.OpenAL;
using DeckMancer.Core;
using OpenTK.Mathematics;
using System;

namespace DeckMancer.Lab
{
    class Sound
    {
        int source;
        int buffer;

        public static short[] ConvertBytesToShorts(byte[] bytes)
        {
            short[] shorts = new short[bytes.Length / 2]; 

            for (int i = 0; i < shorts.Length; i++)
            {
                shorts[i] = System.BitConverter.ToInt16(bytes, i * 2);
            }
            return shorts;
        }

        public Sound(string audioFilePath)
        {
            var device = ALC.OpenDevice(null);
            int a = 0;
            var context = ALC.CreateContext(device, ref a);
            ALC.MakeContextCurrent(context);
            
            var aBuffer = AudioLoader.LoadWavFile(audioFilePath);
            short[] audioShorts = ConvertBytesToShorts(aBuffer);
            
            buffer = AL.GenBuffer();
            AL.BufferData(buffer, ALFormat.Stereo16, audioShorts, 44100);
            
            source = AL.GenSource();
            AL.Source(source, ALSource3f.Position, 0.0f, 0.0f, 0.0f);
            AL.Source(source, ALSourceb.Looping, true);
            AL.Source(source, ALSourcei.Buffer, buffer);
        }
        public void Play()
        {
            AL.SourcePlay(source);
        }

        public void Stop()
        {
            AL.SourceStop(source);
        }
    }
}
