using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DeckMancer.WPF
{
    public class ComponentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TransformTemplate { get; set; }
        public DataTemplate MeshTemplate { get; set; }
        public DataTemplate CameraTemplate { get; set; }
        public DataTemplate ScriptTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
          if (item is TransformViewModel)
            {
                return TransformTemplate;
            }
            else if (item is MeshViewModel)
            {
                return MeshTemplate;
            }
            else if (item is CameraViewModel)
            {
                return CameraTemplate;
            }
            else if (item is ScriptViewModel)
            {
                return ScriptTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
