using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DeckMancer.WPF
{
    public class ComponentsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ComponentViewModel> _components;
        public ObservableCollection<ComponentViewModel> Components
        {
            get { return _components; }
            private set
            {
                if (_components != value)
                {
                    _components = value;
                    OnPropertyChanged(nameof(Components));
                }
            }
        }

        public ComponentsViewModel()
        {
            Components = new ObservableCollection<ComponentViewModel>();
            SceneManager.ChangeGameObjectSelected += HandleGameObjectSelected;
        }

        private void HandleGameObjectSelected(object sender, EventArgs e)
        {
            Update();
        }
        public void Update() 
        {
            if (SceneManager.CurrentGameObjectSelected != null)
            {
                var originalComponents = SceneManager.CurrentGameObjectSelected.GetComponents<Core.Component>();
                var componentViewModels = new List<ComponentViewModel>();

                foreach (var originalComponent in originalComponents)
                {
                    if (originalComponent is Transform)
                    {
                        var viewModel = new TransformViewModel((Core.Transform)originalComponent);
                        componentViewModels.Add(viewModel);
                    }
                    else if (originalComponent is Mesh)
                    {
                        var viewModel = new MeshViewModel((Mesh)originalComponent);
                        componentViewModels.Add(viewModel);
                    }
                    else if (originalComponent is Camera)
                    {
                        var viewModel = new CameraViewModel((Camera)originalComponent);
                        componentViewModels.Add(viewModel);
                    }
                    else if (originalComponent is Script)
                    {
                        var viewModel = new ScriptViewModel((Script)originalComponent);
                        componentViewModels.Add(viewModel);
                    }
                }
                Components = new ObservableCollection<ComponentViewModel>(componentViewModels);
            }
            else
            {
                Components.Clear();
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
