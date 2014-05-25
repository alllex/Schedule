using System.Windows;
using System.Windows.Controls;
using Editor.ViewModels.Controls;

namespace Editor.Views.Controls
{
    /// <summary>
    /// Interaction logic for ConflictsViewer.xaml
    /// </summary>
    public partial class ConflictsViewer : UserControl
    {
        public ConflictsViewer()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ConflictsViewerViewModel;
            if (vm == null) return;
            vm.OpenConflictSolverCommand.Execute(ListBox.SelectedItem);
        }
    }
}
