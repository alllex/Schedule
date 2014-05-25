using System.Windows;
using System.Windows.Controls;
using Editor.ViewModels.Panels.Statistics;
using Editor.ViewModels.Windows;

namespace Editor.Views.Windows
{
    /// <summary>
    /// Interaction logic for StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        public StatisticWindow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var model = DataContext as StatisticWindowViewModel;
            if (model != null)
            {
                GroupStatPanel.DataContext = new GroupStatPanelViewModel { Project = model.Project };
                LecturerStatPanel.DataContext = new LecturerStatPanelViewModel { Project = model.Project };
                SubjectStatPanel.DataContext = new SubjectStatPanelViewModel { Project = model.Project };
//                ClassTimeStatPanel.DataContext = new ClassTimeStatPanelViewModel { Project = model.Project };
                ClassroomStatPanel.DataContext = new ClassroomStatPanelViewModel { Project = model.Project };
            }
        }

//        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
//        {
//            Close();
//        }
//
//        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
//        {
//            var model = DataContext as StatisticWindowViewModel;
//            if (model != null)
//            {
//                GroupStatPanel.DataContext = new GroupStatPanelViewModel { Project = model.Project };
//                LecturerStatPanel.DataContext = new LecturerStatPanelViewModel { Project = model.Project };
//            }
//        }
    }
}
