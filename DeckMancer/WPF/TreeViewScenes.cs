using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DeckMancer.WPF
{
    public class TreeViewScenes
    {
        public static SceneViewModel CurrentSceneViewSelected { get; set; }

        private ObservableCollection<SceneViewModel> _scenes;
        public ObservableCollection<SceneViewModel> Scenes
        {
            get { return _scenes; }
        }
        public TreeViewScenes(List<Scene> scenes)
        {
            _scenes = new ObservableCollection<SceneViewModel>(
                (from scene in scenes
                 select new SceneViewModel(scene))
                .ToList());

            foreach (var _s in _scenes)
            {
                _s.PropertyChanged += OnChanged;
                _s.BindChildren();
            }
            CurrentSceneViewSelected = _scenes[0];
            _scenes[0].IsSelected = true;
            SceneManager.ChangeGameObjectSelected += HandleGameObjectSelected;
        }
        public void AddScene(Scene scene) 
        {
            var svm = new SceneViewModel(scene);
            svm.PropertyChanged += OnChanged;
            svm.BindChildren();
            _scenes.Add(svm);
        }
        private void HandleGameObjectSelected(object sender, EventArgs e)
        {
           foreach(GameObjectViewModel GOV in CurrentSceneViewSelected.Children) 
           {
                if (GOV.GameObject == SceneManager.CurrentGameObjectSelected)
                {
                    GOV.IsSelected = true;
                }
           }
        }
        private void OnChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as SceneViewModel;
            if (e.PropertyName == "IsSelected")
            {

                if (CurrentSceneViewSelected != item)
                {
                    CurrentSceneViewSelected.IsSelected = false;
                    CurrentSceneViewSelected = item;
                    SceneManager.LoadScene(CurrentSceneViewSelected.Scene);
                }


                if (item.IsSelected == false)
                {
                    item.IsEditing = false;
                    item.IsFocused = false;
                }
                if (item.IsSelected == true)
                {
                    item.IsFocused = true;
                }
            }
        }
    }

}
