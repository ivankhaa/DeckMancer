using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Object
    {
        public uint ID { get; protected set; }
        public string Name { get; set; }
    }
}
