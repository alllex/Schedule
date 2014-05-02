using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Editor.Helpers;
using Editor.Models;
using Editor.Repository;
using Editor.UserControls;
using ScheduleData;
using ScheduleData.Editor;
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{

    class TableViewModel : BaseViewModel
    {

        public static int TimeColumnsCount = 2;
        public static int TitleRowsCount = 2;

        #region Properties

        #region Lectures

        private ObservableCollection<UIElement> _classesCards;
        public ObservableCollection<UIElement> ClassCards
        {
            get { return _classesCards; }
            set
            {
                if (_classesCards != value)
                {
                    _classesCards = value;
                    RaisePropertyChanged(() => ClassCards);
                }
            }
        }

        #endregion 

        #region TimeLine

        private ObservableCollection<UIElement> _timeIntervals;
        public ObservableCollection<UIElement> TimeIntervals
        {
            get { return _timeIntervals; }
            set
            {
                if (_timeIntervals != value)
                {
                    _timeIntervals = value;
                    RaisePropertyChanged(() => TimeIntervals);
                }
            }
        }

        #endregion

        #region DayLine

        private ObservableCollection<UIElement> _dayLine;
        public ObservableCollection<UIElement> DayLine
        {
            get { return _dayLine; }
            set
            {
                if (_dayLine != value)
                {
                    _dayLine = value;
                    RaisePropertyChanged(() => DayLine);
                }
            }
        }

        #endregion

        #region Titles

        private ObservableCollection<UIElement> _titles;
        public ObservableCollection<UIElement> Titles
        {
            get { return _titles; }
            set
            {
                if (_titles != value)
                {
                    _titles = value;
                    RaisePropertyChanged(() => Titles);
                }
            }
        }

        #endregion

        #endregion

        private ClassesTable _classTable;
        private TimeLineMarkup _timeLineMarkup;
        private TitlesMarkup _titlesMarkup;

        private List<ClassCard> _selectedCards = new List<ClassCard>();

        public TableViewModel(ISchedule schedule, IYearOfStudy year)
        {
            _classTable = new ClassesTable(schedule, year);
            _timeLineMarkup = new TimeLineMarkup(_classTable.TimeIntervals);
            _titlesMarkup = new TitlesMarkup(_classTable.Groups);
            InitDayLine();
            InitTimeIntervalLine();
            InitializeTitles();
            InitLectureCards();
        }

        //private void InitializeSubTitles()
        //{
        //    Subtitles = new ObservableCollection<UIElement>();
        //    int col = TimeColumnsCount;
        //    foreach (var subgroup in ScheduleRepository.Subgroups)
        //    {
        //        Subtitles.Add(havingNameToSubtitleCard(subgroup, col++));
        //    }
        //}

        private void InitializeTitles()
        {
            Titles = new ObservableCollection<UIElement>();
            foreach (var title in _titlesMarkup.Titles)
            {
                var tvm = new TitleCardViewModel(title.Item);
                var tc = new TitleCard { DataContext = tvm };
                Grid.SetRow(tc, title.Row);
                Grid.SetColumn(tc, title.Column);
                Grid.SetRowSpan(tc, title.RowSpan);
                Grid.SetColumnSpan(tc, title.ColumnSpan);
                Titles.Add(tc);
            }
        }

        private void InitDayLine()
        {
            DayLine = new ObservableCollection<UIElement>();
            foreach (var day in _timeLineMarkup.Days)
            {
                var dvm = new DayCardViewModel(day.Item);
                var dc = new DayCard { DataContext = dvm };
                Grid.SetRow(dc, TitleRowsCount + day.Row);
                Grid.SetColumn(dc, day.Column);
                Grid.SetRowSpan(dc, day.RowSpan);
                Grid.SetColumnSpan(dc, day.ColumnSpan);
                DayLine.Add(dc);
            }
        }

        private void InitTimeIntervalLine()
        {
            TimeIntervals = new ObservableCollection<UIElement>();
            foreach (var classInterval in _timeLineMarkup.ClassIntervals)
            {
                var tvm = new TimeCardViewModel(classInterval.Item);
                var tc = new TimeCard { DataContext = tvm };
                Grid.SetRow(tc, TitleRowsCount + classInterval.Row);
                Grid.SetColumn(tc, classInterval.Column);
                Grid.SetRowSpan(tc, classInterval.RowSpan);
                Grid.SetColumnSpan(tc, classInterval.ColumnSpan);
                TimeIntervals.Add(tc);
            }
        }

        private void InitLectureCards()
        {
            ClassCards = new ObservableCollection<UIElement>();  

            for (int row = 0; row < _classTable.RowsCount(); row++)
            {
                for (int col = 0; col < _classTable.ColumnsCount(); col++)
                {
                    var l = _classTable.Table[row][col];
                    var lvm = new ClassCardViewModel(new List<IClass> {l.Item});
                    var lc = new ClassCard { DataContext = lvm };
                    Grid.SetRow(lc, row + TitleRowsCount);
                    Grid.SetColumn(lc, col + TimeColumnsCount);
                    Grid.SetRowSpan(lc, l.RowSpan);
                    Grid.SetColumnSpan(lc, l.ColumnSpan);
                    lc.Click += LectureCardOnClick;
                    ClassCards.Add(lc);
                }
            }    
        }

        public int TableWidth()
        {
            return TimeColumnsCount + _classTable.ColumnsCount();
        }

        public int TableHeight()
        {
            return TitleRowsCount + _classTable.RowsCount();
        }

        #region Commands

        //public ICommand SetEditModeCommand { get { return new DelegateCommand(OnSetEditMode, CanExecuteSetEditMode); } }
        //public ICommand SetViewModeCommand { get { return new DelegateCommand(OnSetViewMode, CanExecuteSetViewMode); } }

        #endregion

        #region Command Handlers


        #endregion

        #region Event Handlers

        private void LectureCardOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var lc = (ClassCard)sender;
            if (_selectedCards.Any())
            {
                foreach (var selectedCard in _selectedCards)
                {
                    var vm = (ClassCardViewModel)selectedCard.DataContext;
                    vm.IsSelected = false;
                }
                _selectedCards.Clear();
            }
            _selectedCards.Add(lc);
            var lcvm = (ClassCardViewModel)lc.DataContext;
            lcvm.IsSelected = true;
        }

        #endregion
    }
}
