using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Script : Component
    {
        public Behaviour BehaviourScript { get; set; }
        public Script()
        {
            
        }
    }
}
