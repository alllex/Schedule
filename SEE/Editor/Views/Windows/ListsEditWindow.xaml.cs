using System.Windows;
using Editor.ViewModels.Panels.Edit;
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
                ClassroomEditPanel.DataContext = new ClassroomEditPanelViewModel { Project = model.Project };
                GroupEditPanel.DataContext = new GroupEditPanelViewModel { Project = model.Project };
                SubjectEditPanel.DataContext = new SubjectEditPanelViewModel() { Project = model.Project };
                LecturerEditPanel.DataContext = new LecturerEditPanelViewModel { Project = model.Project };
                SpecializationEditPanel.DataContext = new SpecializationEditPanelViewModel {Project = model.Project};
                YearOfStudyEditPanel.DataContext = new YearOfStudyEditPanelViewModel { Project = model.Project };
            }
        }
    }
}
