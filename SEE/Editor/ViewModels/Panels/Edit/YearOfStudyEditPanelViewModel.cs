using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels.Panels.Edit
{
    class YearOfStudyEditPanelViewModel : HasProjectProperty
    {
        #region Commands

        public ICommand AddYearOfStudyCommand { get { return new DelegateCommand(OnAddYearOfStudy); } }
        public ICommand RemoveYearOfStudyCommand { get { return new DelegateCommand(OnRemoveYearOfStudy); } }

        private void OnRemoveYearOfStudy()
        {
            MessageBox.Show("Remove");
            //var year = param as YearOfStudy;
            //if (year == null) return;
            //Project.Schedule.RemoveYearOfStudy(year);
        }

        #endregion

        #region Command Handlers

        private void OnAddYearOfStudy()
        {
            Project.Schedule.AddYearOfStudy(new YearOfStudy{Name = "Новый"});
        }

        #endregion
        

    }
}
