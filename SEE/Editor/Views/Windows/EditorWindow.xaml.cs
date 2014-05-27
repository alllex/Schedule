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

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");

            var conflictsViewerViewModel = new ConflictsViewerViewModel();
            var tableControllerViewModel = new TableControllerViewModel();
            var viewModel = new EditorWindowViewModel(tableControllerViewModel, conflictsViewerViewModel);
            DataContext = viewModel;
            viewModel.PropertyChanged += (s, e) =>
            {
                if (s is EditorWindowViewModel)
                {
                    if (e.PropertyName == "Project")
                    {
                        tableControllerViewModel.Project = viewModel.Project;
                        conflictsViewerViewModel.Project = viewModel.Project;
                    }
                    else if (e.PropertyName == "HasActiveProject")
                    {
                        OnHasActiveProjectChanged(viewModel.HasActiveProject);
                    }
                }
            };
            TablesController.DataContext = tableControllerViewModel;
            ConflictsViewer.DataContext = conflictsViewerViewModel;
        }

        private void OnHasActiveProjectChanged(bool has)
        {
            if (has)
            {
                TablesController.Visibility = Visibility.Visible;
                OnStartUpPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                TablesController.Visibility = Visibility.Collapsed;
                OnStartUpPanel.Visibility = Visibility.Visible;
            }
        }
    }


}
