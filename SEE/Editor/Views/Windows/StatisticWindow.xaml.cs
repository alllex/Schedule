using System.Windows;
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
            }
        }
    }
}
