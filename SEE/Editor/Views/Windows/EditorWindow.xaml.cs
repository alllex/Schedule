using System;
using System.ComponentModel;
using System.Diagnostics;
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

            var tableControllerViewModel = new TableControllerViewModel();
            var viewModel = new EditorWindowViewModel(tableControllerViewModel);
            DataContext = viewModel;
            viewModel.PropertyChanged += (s, e) =>
            {
                var x = s as EditorWindowViewModel;
                if (x == null) return;
                if (e.PropertyName == "Project")
                {
                    tableControllerViewModel.Project = viewModel.Project;
                }
            };
            TablesController.DataContext = tableControllerViewModel;
        }
    }
}
