using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Editor.ViewModels;

namespace Editor.Views
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

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
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
