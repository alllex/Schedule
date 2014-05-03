using System.Windows;
using Editor.ViewModels;

namespace Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            InitializeComponent();
            var viewModel = new EditorWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
