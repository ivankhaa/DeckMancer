using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DeckMancer.Serialization
{
    public static class BinarySerialization
    {
        private static string error = null;
        public static string GetLastError() 
        {
            return error;
        }
        public static void Serialize<T>(T obj, string fileName) where T : class
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
        }

        public static T Deserialize<T>(string fileName) where T : class
        {
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    var data = formatter.Deserialize(stream) as T;
                    error = null;
                    return data;
                }
            }
            catch (Exception ex)
            {

                error = $"Error deserializing object from file {fileName}: {ex.Message}";
                return default(T);
            }
        }
    }
}
