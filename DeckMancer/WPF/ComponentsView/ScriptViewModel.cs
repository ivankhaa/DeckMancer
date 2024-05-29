using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.WPF
{
    public class ScriptViewModel : ComponentViewModel
    {
        Script _script;
        public string Name { get { if (_script.Name != null) return _script.Name + ".cs"; else return ""; } set { _script.Name = value; OnPropertyChanged(nameof(Name)); } }
        public ScriptViewModel(Script script)
        {
            _script = script;
        }
    }
}
