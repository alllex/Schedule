using System.Collections.ObjectModel;
using System.ComponentModel;
using Editor.Models;
using Editor.ViewModels.Controls;

namespace Editor.ViewModels.Panels.Statistics
{
    class StatisticsPanelViewModel<T> : HasProjectProperty
    {

        #region ObjectList

        private ObservableCollection<T> _objectList;

        public ObservableCollection<T> ObjectList
        {
            get { return _objectList; }
            set
            {
                if (_objectList != value)
                {
                    _objectList = value;
                    RaisePropertyChanged(() => ObjectList);
                }
            }
        }

        #endregion

    }

    class GroupStatPanelViewModel : StatisticsPanelViewModel<Group>
    {
        public GroupStatPanelViewModel()
        {

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as GroupStatPanelViewModel;
            if (s == null) return;
            if (e.PropertyName == "Project")
            {
                if (Project != null && Project.ClassesSchedule != null)
                {
                    ObjectList = Project.ClassesSchedule.Groups;
                }
            }
        }
    }
}
