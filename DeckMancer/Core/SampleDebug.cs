using System.Collections.Generic;

namespace DeckMancer.Core
{
    public enum DebugIcon
    {
        Error,
        Warning,
        Message
    }
    public class DebugContent
    {
        public System.DateTime Date { get; set; } = System.DateTime.Now;
        public byte[] ImageBuffer { get; set; }
        public string Message { get; set; }
        public int Duplicate { get; set; } = 1;

    }
    public class SampleDebug
    {
        private enum OutputType
        {
            None,
            Write,
            WriteLine
        }

        private static string PathErrorIcon = System.IO.Path.GetFullPath(@"Resource\Icon\Debug_Error.png");
        private static string PathWarningIcon = System.IO.Path.GetFullPath(@"Resource\Icon\Debug_Warning.png");
        private static string PathMessageIcon = System.IO.Path.GetFullPath(@"Resource\Icon\Debug_Message.png");
        private static byte[] ErrorIconBuffer = ImageLoader.LoadImage(PathErrorIcon);
        private static byte[] WarningIconBuffer = ImageLoader.LoadImage(PathWarningIcon);
        private static byte[] MessageIconBuffer = ImageLoader.LoadImage(PathMessageIcon);

        private OutputType lastOutputType = OutputType.None;
        private List<DebugContent> OutputText = new List<DebugContent>();

        public delegate void OutputTextChangedHandler(List<DebugContent> newText);
        public event OutputTextChangedHandler OutputTextChanged;


        public void Clear()
        {
            OutputText.Clear();
            lastOutputType = OutputType.None;
            OnOutputTextChanged();
        }
        public void Remove(DebugContent item) 
        {
            OutputText.Remove(item);
        }
        private DebugContent FindDebugContentByMessage(List<DebugContent> debugContents, string message)
        {
            return debugContents.Find(item => item.Message == message);
        }
        public void WriteLine(string message, DebugIcon type = DebugIcon.Message)
        {
            DebugContent foundDebugContent = null;
            byte[] iconBuffer = (type == DebugIcon.Message) ? MessageIconBuffer : (type == DebugIcon.Warning) ? WarningIconBuffer : ErrorIconBuffer;


            if (lastOutputType == OutputType.Write)
            {
                if(OutputText.Count > 0)
                    foundDebugContent = FindDebugContentByMessage(OutputText, OutputText[OutputText.Count - 1].Message + message);
                if (foundDebugContent != null)
                {
                    foundDebugContent.Duplicate++;
                    foundDebugContent.Date = System.DateTime.Now;
                    OutputText.RemoveAt(OutputText.Count-1);
                }
                else
                    OutputText[OutputText.Count - 1].Message += message;

            }
            else
            {
                 foundDebugContent = FindDebugContentByMessage(OutputText, message);
                if (foundDebugContent != null)
                {
                    foundDebugContent.Duplicate++;
                    foundDebugContent.Date = System.DateTime.Now;
                }
                else
                    OutputText.Add(new DebugContent { ImageBuffer = iconBuffer, Message = message });
            }
            lastOutputType = OutputType.WriteLine;
            
            OnOutputTextChanged();
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public void Write(string message)
        {
            if (lastOutputType == OutputType.Write)
            {
                OutputText[OutputText.Count - 1].Message += message;
            }
            else
            {
            
                OutputText.Add(new DebugContent { ImageBuffer = MessageIconBuffer, Message = message });
            }

            lastOutputType = OutputType.Write;

            OnOutputTextChanged();
        }

        public void Write(string format, params object[] args)
        {
            Write(string.Format(format, args));
        }

        private void OnOutputTextChanged()
        {
            OutputTextChanged?.Invoke(OutputText);
        }
    }
}
