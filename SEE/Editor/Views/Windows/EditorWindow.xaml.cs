using System.Windows;
using Editor.ViewModels;

namespace Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {

        private EditorWindowViewModel viewModel;

        public EditorWindow()
        {
            InitializeComponent();
            viewModel = new EditorWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
