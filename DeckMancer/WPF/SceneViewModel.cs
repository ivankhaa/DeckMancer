using DeckMancer.Core;
using System.ComponentModel;
using System.IO;

namespace DeckMancer.WPF
{
    public class SceneViewModel:TreeViewItemViewModel
    {

        private static GameObjectViewModel _currentGameObjectSelected;
        public static GameObjectViewModel CurrentGameObjectSelected
        {
            get { return _currentGameObjectSelected; }
            set
            {
                if (_currentGameObjectSelected != value)
                {
                    _currentGameObjectSelected = value;

                }
            }
        }

        private Scene _scene;
        public byte[] Image { get; }
        public SceneViewModel(Scene scene)
            : base(null, true)
        {
            _scene = scene;
            Image = ImageLoader.LoadImage(Path.GetFullPath(@"Resource\Icon\Scene.png"));
        }

        public string Name
        {
            get { return _scene.Name; }
            set { _scene.Name = value; }
        }
        public Scene Scene
        {
            get { return _scene; }
        }

        public void AddGameObject(GameObject gameObject) 
        {
            Scene.AddGameObject(gameObject);
            var GOVM = new GameObjectViewModel(gameObject, this);
            GOVM.PropertyChanged += OnChanged;
            base.Children.Add(GOVM);
        }
        private void OnChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as GameObjectViewModel;
            if (e.PropertyName == "IsSelected")
            {
                if (TreeViewScenes.CurrentSceneViewSelected != item.Parent)
                    {
                    TreeViewScenes.CurrentSceneViewSelected.IsSelected = false;
                    TreeViewScenes.CurrentSceneViewSelected = item.Parent as SceneViewModel;
                    SceneManager.LoadScene(TreeViewScenes.CurrentSceneViewSelected.Scene);
                }
                    item.Parent.IsSelected = true;
                if (CurrentGameObjectSelected != null)
                {
                    if (SceneManager.CurrentGameObjectSelected != CurrentGameObjectSelected.GameObject)
                    {
                        CurrentGameObjectSelected.IsSelected = false;
                    }
                }
                if (item.IsSelected == false)
                {
                    CurrentGameObjectSelected = null;
                    SceneManager.CurrentGameObjectSelected = null;
                    item.IsEditing = false;
                    item.IsFocused = false;
                }
                if (item.IsSelected == true)
                {
                    CurrentGameObjectSelected = item;
                    SceneManager.CurrentGameObjectSelected = item.GameObject;
                    item.IsFocused = true;
                }
            }
        }
        protected override void LoadChildren()
        {
            foreach (GameObject gameObject in _scene.GameObjects)
            {
                var GOVM = new GameObjectViewModel(gameObject, this);
                GOVM.PropertyChanged += OnChanged;
                base.Children.Add(GOVM);
            }
        }
    }
}