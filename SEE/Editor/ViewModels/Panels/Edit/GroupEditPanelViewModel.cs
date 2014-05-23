using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels.Panels.Edit
{
    class GroupEditPanelViewModel : HasProjectProperty
    {

        #region Commands

        public ICommand AddGroupCommand { get { return new DelegateCommand(OnAddGroup); } }
        public ICommand RemoveGroupCommand { get { return new DelegateCommand(OnRemoveGroup); } }

        private void OnRemoveGroup()
        {
            MessageBox.Show("Remove");
            //var year = param as Group;
            //if (year == null) return;
            //Project.ClassesSchedule.RemoveGroup(year);
        }

        #endregion

        #region Command Handlers

        private void OnAddGroup()
        {
            Project.Schedule.AddGroup(new Group { Name = "Новый" });
        }

        #endregion

    }
}
