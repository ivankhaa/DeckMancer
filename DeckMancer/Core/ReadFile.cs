using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeckMancer.Core
{
    public static class ReadFile
    {
        public static string ReadAllText(string filePath)
        {
            try
            {
                return File.ReadAllText(Path.GetFullPath(filePath));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
