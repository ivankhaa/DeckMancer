using DeckMancer.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DeckMancer.WPF
{
    public class ListBoxDebugViewModel
    {
        public ObservableCollection<DebugContent> Items { get; set; }
       
        public ListBoxDebugViewModel()
        {

            Debug.OutputTextChanged += Debug_OutputTextChanged;
            Items = new ObservableCollection<DebugContent>();
        }

        private void Debug_OutputTextChanged(List<DebugContent> newText)
        {
            Items.Clear();
            foreach (var item in newText)
            {
                Items.Add(item);
            }
        }
    }
}
