namespace DeckMancer.Core
{
    public static class Debug
    {
        private static readonly SampleDebug debugInstance = new SampleDebug();

        public static void Clear()
        {
            debugInstance.Clear();
        }
        public static void Remove(DebugContent item)
        {
            debugInstance.Remove(item);
        }

        public static void WriteLine(string message, DebugIcon type = DebugIcon.Message)
        {
            debugInstance.WriteLine(message, type);
        }

        public static void WriteLine(string format, params object[] args)
        {
            debugInstance.WriteLine(format, args);
        }

        public static void Write(string message)
        {
            debugInstance.Write(message);
        }

        public static void Write(string format, params object[] args)
        {
            debugInstance.Write(format, args);
        }

        public static event SampleDebug.OutputTextChangedHandler OutputTextChanged
        {
            add { debugInstance.OutputTextChanged += value; }
            remove { debugInstance.OutputTextChanged -= value; }
        }
    }
}

