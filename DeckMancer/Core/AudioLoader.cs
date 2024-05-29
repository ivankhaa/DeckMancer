using System;
using NAudio.Wave;

namespace DeckMancer.Core
{
    public static class AudioLoader
    {
        public static byte[] LoadWavFile(string filePath)
        {
            try
            {
                using (var reader = new WaveFileReader(filePath))
                {
                    byte[] buffer = new byte[reader.Length];

                    int bytesRead = reader.Read(buffer, 0, buffer.Length);
                    
                    return buffer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке файла: {ex.Message}");
                return null;
            }
        }
    }
}
