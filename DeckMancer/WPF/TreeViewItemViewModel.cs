using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DeckMancer.WPF
{
    public class TreeViewItemViewModel : INotifyPropertyChanged
    {

        static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

        private readonly ObservableCollection<TreeViewItemViewModel> _children;
        private readonly TreeViewItemViewModel _parent;

        private bool _isExpanded;
        private bool _isSelected;
        private bool _isEditing;
        private bool _isFocused;

        protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
        {
            _parent = parent;

            _children = new ObservableCollection<TreeViewItemViewModel>();

            if (lazyLoadChildren)
                _children.Add(DummyChild);
        }

        private TreeViewItemViewModel()
        {

        }

        public ObservableCollection<TreeViewItemViewModel> Children
        {
            get { return _children; }
        }

        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }
        public bool IsFocused
        {
            get { return _isFocused; }
            set
            {
                if (value != _isFocused)
                {
                    _isFocused = value;
                    OnPropertyChanged("IsFocusable");
                }
            }
        }
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                if (value != _isEditing)
                {
                    _isEditing = value;
                    OnPropertyChanged("IsEditing");
                }
            }
        }
        public void BindChildren() 
        {            
            if (this.HasDummyChild)
            {
                this.Children.Remove(DummyChild);
                this.LoadChildren();
            }
        }

        protected virtual void LoadChildren()
        {

        }


        public TreeViewItemViewModel Parent
        {
            get { return _parent; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
