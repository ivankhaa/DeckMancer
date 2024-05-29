using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DeckMancer.WPF
{
    public class ComponentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
