using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeckMancer.WPF
{
    public class GameObjectViewModel : TreeViewItemViewModel
    {
        public byte[] Image { get; }
        readonly GameObject _gameObject;
        public GameObject GameObject { get { return _gameObject; } }
        public GameObjectViewModel(GameObject gameObject, SceneViewModel sceneView)
            : base(sceneView, false)
        {
            _gameObject = gameObject;
            Image = ImageLoader.LoadImage(Path.GetFullPath(@"Resource\Icon\GameObject.png"));

        }

        public string Name
        {
            get { return _gameObject.Name; }
            set { _gameObject.Name = value; OnPropertyChanged(nameof(Name)); }
        }
    }

}
