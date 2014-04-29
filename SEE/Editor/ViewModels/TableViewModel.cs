using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using Editor.Helpers;
using Editor.Models;
using Editor.Repository;
using Editor.UserControls;
using ScheduleData;

namespace Editor.ViewModels
{

    class TableViewModel : BaseViewModel
    {

        public static int TimeColumnsCount = 2;
        public static int TitleRowsCount = 2;
        public static int TimeColumn = 1;
        public static int DayColumn = 0;
        public static int TitleRow = 0;
        public static int SubtitleRow = 1;

        #region Properties

        #region Lectures

        private ObservableCollection<UIElement> _lecturesCards;
        public ObservableCollection<UIElement> LectureCards
        {
            get { return _lecturesCards; }
            set
            {
                if (_lecturesCards != value)
                {
                    _lecturesCards = value;
                    RaisePropertyChanged(() => LectureCards);
                }
            }
        }

        #endregion 

        #region TimeLine

        private ObservableCollection<UIElement> _timeLine;
        public ObservableCollection<UIElement> TimeLine
        {
            get { return _timeLine; }
            set
            {
                if (_timeLine != value)
                {
                    _timeLine = value;
                    RaisePropertyChanged(() => TimeLine);
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

        #region Subtitles

        private ObservableCollection<UIElement> _subtitles;
        public ObservableCollection<UIElement> Subtitles
        {
            get { return _subtitles; }
            set
            {
                if (_subtitles != value)
                {
                    _subtitles = value;
                    RaisePropertyChanged(() => Subtitles);
                }
            }
        }

        #endregion

        #endregion

        public TableViewModel()
        {
            InitializeLectureCards();
            InitializeTimeLine();
            InitializeDayLine();
            InitializeTitles();
            InitializeSubTitles();
        }

        private void InitializeSubTitles()
        {
            Subtitles = new ObservableCollection<UIElement>();
            int col = TimeColumnsCount;
            foreach (var subgroup in ScheduleRepository.Subgroups)
            {
                Subtitles.Add(havingNameToSubtitleCard(subgroup, col++));
            }
        }

        private void InitializeTitles()
        {
            Titles = new ObservableCollection<UIElement>();
            int col = TimeColumnsCount;
            foreach (var group in ScheduleRepository.Groups)
            {
                Titles.Add(havingNameToTitleCard(group, col));
                int shift = group.Subgroups.Count();
                col += shift == 0 ? 1 : shift;
            }
        }

        private void InitializeDayLine()
        {
            DayLine = new ObservableCollection<UIElement>();
            for (int i = 0; i < ScheduleRepository.WeekdaysCount; i++)
            {
                DayLine.Add(WeekdaysToDayCard(i));
            }
        }

        private void InitializeTimeLine()
        {
            TimeLine = new ObservableCollection<UIElement>();
            for (int i = 0; i < ScheduleRepository.TimeLineLength; i++)
            {
                TimeLine.Add(TimeToTimeCard(i));
            }
        }

        private void InitializeLectureCards()
        {
            LectureCards = new ObservableCollection<UIElement>();  
            for (int row = 0; row < ScheduleRepository.RowCount(); row++)
            {
                for (int col = 0; col < ScheduleRepository.ColCount(); col++)
                {
                    LectureCards.Add(LectureToLectureCard(row, col));
                }
            }
        }

        private LectureCard LectureToLectureCard(int row, int col)
        {
            var lvm = new LectureCardViewModel(ScheduleRepository.Table[row][col]);
            var lc = new LectureCard {DataContext = lvm};
            Grid.SetRow(lc, row + TitleRowsCount);
            Grid.SetColumn(lc, col + TimeColumnsCount);
            return lc;
        }

        private TimeCard TimeToTimeCard(int timeLineIndex)
        {
            var tvm = new TimeCardViewModel(ScheduleRepository.TimeLine[timeLineIndex]);
            var tc = new TimeCard {DataContext = tvm};
            Grid.SetRow(tc, timeLineIndex + TitleRowsCount);
            Grid.SetColumn(tc, TimeColumn);
            return tc;
        }

        private DayCard WeekdaysToDayCard(int dayIndex)
        {
            var dvm = new DayCardViewModel((Weekdays) dayIndex);
            var dc = new DayCard {DataContext = dvm};
            Grid.SetRow(dc, TitleRowsCount + dayIndex * ScheduleRepository.LecturesPerDay);
            Grid.SetColumn(dc, DayColumn);
            Grid.SetRowSpan(dc, ScheduleRepository.LecturesPerDay);
            return dc;
        }

        private TitleCard havingNameToTitleCard(IGroup group, int col)
        {
            var tvm = new TitleCardViewModel(group);
            var tc = new TitleCard{DataContext = tvm};
            Grid.SetRow(tc, TitleRow);
            Grid.SetColumn(tc, col);
            int span = group.Subgroups.Count();
            if (span > 0)
                Grid.SetColumnSpan(tc, span);
            return tc;
        }

        private SubtitleCard havingNameToSubtitleCard(IGroup group, int col)
        {
            var stvm = new SubtitleCardViewModel(group);
            var stc = new SubtitleCard { DataContext = stvm };
            Grid.SetRow(stc, SubtitleRow);
            Grid.SetColumn(stc, col);
            return stc;
        }

        public int TableWidth()
        {
            return ScheduleRepository.SubtitleCount + TimeColumnsCount;
        }

        public int TableHeight()
        {
            return ScheduleRepository.TimeLineLength + TitleRowsCount;
        }

    }
}
