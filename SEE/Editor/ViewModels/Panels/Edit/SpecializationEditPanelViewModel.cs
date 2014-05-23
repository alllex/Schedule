using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels.Panels.Edit
{
    class SpecializationEditPanelViewModel : HasProjectProperty
    {

        #region Commands

        public ICommand AddSpecializationCommand { get { return new DelegateCommand(OnAddSpecialization); } }
        public ICommand RemoveSpecializationCommand { get { return new DelegateCommand(OnRemoveSpecialization); } }

        private void OnRemoveSpecialization()
        {
            MessageBox.Show("Remove");
            //var year = param as Specialization;
            //if (year == null) return;
            //Project.ClassesSchedule.RemoveSpecialization(year);
        }

        #endregion

        #region Command Handlers

        private void OnAddSpecialization()
        {
            Project.Schedule.AddSpecialization(new Specialization { Name = "Новый" });
        }

        #endregion
    }
}
