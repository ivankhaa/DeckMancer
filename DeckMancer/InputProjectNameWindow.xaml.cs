using System.Windows;

namespace DeckMancer
{
    public partial class InputProjectNameWindow : Window
    {
        public string ProjectName { get; private set; }

        public InputProjectNameWindow()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            ProjectName = ProjectNameTextBox.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
