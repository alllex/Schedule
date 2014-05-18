using System.Windows;
using Editor.ViewModels.Controls;
using Editor.ViewModels.Windows;

namespace Editor.Views.Windows
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            InitializeComponent();

            var model = new EditorWindowViewModel();
            DataContext = model;
            CardClipboard.DataContext = new CardClipboardViewModel(model.Project);
            TablesController.DataContext = new TablesControllerViewModel(model.Project);
        }
    }
}
