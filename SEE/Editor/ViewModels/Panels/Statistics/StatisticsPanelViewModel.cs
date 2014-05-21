using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Editor.Models;
using ScheduleData;
using ScheduleData.DataMining;

namespace Editor.ViewModels.Panels.Statistics
{
    abstract class StatisticsPanelViewModel<TSubject> : HasProjectProperty where TSubject : HavingId
    {

        #region ObjectList

        private ObservableCollection<TSubject> _objectList;

        public ObservableCollection<TSubject> ObjectList
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

        #region SelectedItem

        private TSubject _selectedItem;

        public TSubject SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    RaisePropertyChanged(() => SelectedItem);
                }
            }
        }

        #endregion

        #region StatisticOnSelected

        private Statistic<TSubject> _statisticOnSelected;

        public Statistic<TSubject> StatisticOnSelected
        {
            get { return _statisticOnSelected; }
            set
            {
                if (_statisticOnSelected != value)
                {
                    _statisticOnSelected = value;
                    RaisePropertyChanged(() => StatisticOnSelected);
                }
            }
        }

        #endregion

        private Dictionary<TSubject, int> _subjectDictionary;

        protected StatisticsPanelViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as StatisticsPanelViewModel<TSubject>;
            if (s == null) return;
            if (e.PropertyName == "Project")
            {
                if (Project != null && Project.ClassesSchedule != null)
                {
                    ObjectList = GetCollection();
                    _subjectDictionary = new Dictionary<TSubject, int>();
                    for (int i = 0; i < ObjectList.Count; i++)
                    {
                        _subjectDictionary.Add(ObjectList[i], i);
                    }

                    if (ObjectList.Any())
                    {
                        SelectedItem = ObjectList.First();
                    }
                }
            } 
            else if (e.PropertyName == "SelectedItem")
            {
                var stat = GetStatistic();
                StatisticOnSelected = stat != null ? stat[_subjectDictionary[SelectedItem]] : null;
            }
            else if (e.PropertyName == "StatisticOnSelected")
            {
                //MessageBox.Show(StatisticOnSelected.CountOfClassesPerWeek + " ");
            }
        }

        protected abstract ObservableCollection<TSubject> GetCollection();
        protected abstract Collection<Statistic<TSubject>> GetStatistic();

    }

    class GroupStatPanelViewModel : StatisticsPanelViewModel<Group>
    {

        protected override ObservableCollection<Group> GetCollection()
        {
            return Project.ClassesSchedule != null ? Project.ClassesSchedule.Groups : null;
        }

        protected override Collection<Statistic<Group>> GetStatistic()
        {
            return Project.StatisticCompilation != null ? Project.StatisticCompilation.GroupStatistic : null;
        }
    }

    class LecturerStatPanelViewModel : StatisticsPanelViewModel<Lecturer>
    {

        protected override ObservableCollection<Lecturer> GetCollection()
        {
            return Project.ClassesSchedule != null ? Project.ClassesSchedule.Lecturers : null;
        }

        protected override Collection<Statistic<Lecturer>> GetStatistic()
        {
            return Project.StatisticCompilation != null ? Project.StatisticCompilation.LecturerStatistic : null;
        }
    }
}
