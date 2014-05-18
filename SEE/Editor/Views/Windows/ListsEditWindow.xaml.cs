using System.Windows;
using Editor.ViewModels.Panels;
using Editor.ViewModels.Windows;

namespace Editor.Views.Windows
{
    /// <summary>
    /// Interaction logic for LecturersEditWindow.xaml
    /// </summary>
    public partial class ListsEditWindow : Window
    {
        public ListsEditWindow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var model = DataContext as ListsEditWindowViewModel;
            if (model != null)
            {
                ClassroomEditPanel.DataContext = new ClassroomEditPanelViewModel{ ClassesSchedule = model.ClassesSchedule };
                GroupEditPanel.DataContext = new GroupEditPanelViewModel { ClassesSchedule = model.ClassesSchedule };
                LecturerEditPanel.DataContext = new LecturerEditPanelViewModel { ClassesSchedule = model.ClassesSchedule };
            }
        }
    }
}
