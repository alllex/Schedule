﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.UserControls;
using Editor.ViewModels.Cards;

namespace Editor.ViewModels
{
    public class TableViewModel : BaseViewModel
    {

        public static int TimeColumnsCount = 2;
        public static int TitleRowsCount = 2;

        #region Properties

        protected override void ClassesScheduleOnPropertyChanged()
        {
            _timeLineMarkup = new TimeLineMarkup(ClassesSchedule);
            InitDayLine();
            InitTimeIntervalLine();

            if (YearOfStudy != null)
            {
                YearOfStudyOnPropertyChanged();
            }
        }

        #region YearOfStudy

        private YearOfStudy _yearOfStudy;

        public YearOfStudy YearOfStudy
        {
            get { return _yearOfStudy; }
            set
            {
                if (_yearOfStudy != value)
                {
                    _yearOfStudy = value;
                    YearOfStudyOnPropertyChanged();
                    RaisePropertyChanged(() => YearOfStudy);
                }
            }
        }

        private void YearOfStudyOnPropertyChanged()
        {
            TableHeader = YearOfStudy.ToString();
            _classesTable = new ClassesTable(ClassesSchedule, YearOfStudy);
            _titlesMarkup = new TitlesMarkup(_classesTable.Groups);
            InitializeTitles();
            InitLectureCards();
        }

        #endregion

        #region TableHeader

        private string _tableHeader;

        public string TableHeader
        {
            get { return _tableHeader; }
            set
            {
                if (_tableHeader != value)
                {
                    _tableHeader = value;
                    RaisePropertyChanged(() => TableHeader);
                }
            }
        }

        #endregion
        
        #region TimeLine

        private ObservableCollection<UIElement> _timeIntervals = new ObservableCollection<UIElement>();
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

        private ObservableCollection<UIElement> _dayLine = new ObservableCollection<UIElement>();
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

        private ObservableCollection<UIElement> _titles = new ObservableCollection<UIElement>();
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

        public int ClassesRowsCount;
        public int ClassesColumnsCount;
        public ClassCard[][] ClassesCards;
        private ClassesTable _classesTable;
        private TimeLineMarkup _timeLineMarkup;
        private TitlesMarkup _titlesMarkup;
        private ClassCardViewModel _selectedCard;

        #region Ctor

        public TableViewModel() { }
        
        #endregion

        private void InitializeTitles()
        {
            Titles = new ObservableCollection<UIElement>();
            foreach (var title in _titlesMarkup.Titles)
            {
                var tvm = new TitleCardViewModel(title.Item);
                var tc = new TitleCard { DataContext = tvm };
                Grid.SetRow(tc, title.Row);
                Grid.SetColumn(tc, TitleRowsCount + title.Column);
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
            foreach (var classInterval in _timeLineMarkup.ClassesIntervals)
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
            ClassesRowsCount = _classesTable.RowsCount();
            ClassesColumnsCount = _classesTable.ColumnsCount();
            ClassesCards = new ClassCard[ClassesRowsCount][];
            for (var row = 0; row < ClassesRowsCount; row++)
            {
                ClassesCards[row] = new ClassCard[ClassesColumnsCount];
                for (var col = 0; col < ClassesColumnsCount; col++)
                {
                    ClassesCards[row][col] = CreateClassCard(row, col);
                }
            }    
        }

        private ClassCard CreateClassCard(int row, int column)
        {
            var spanned = _classesTable.Table[row][column];
            var classCard = new ClassCard { DataContext = spanned.Item };
            Grid.SetRow(classCard, row + TitleRowsCount);
            Grid.SetColumn(classCard, column + TimeColumnsCount);
            Grid.SetRowSpan(classCard, spanned.RowSpan);
            Grid.SetColumnSpan(classCard, spanned.ColumnSpan);
            AddClassCardHandlers(classCard);
            return classCard;
        }

        public int TableWidth()
        {
            return TimeColumnsCount + (_classesTable != null ? _classesTable.ColumnsCount() : 0);
        }

        public int TableHeight()
        {
            return TitleRowsCount + (_classesTable != null ? _classesTable.RowsCount() : 0);
        }

        #region Commands

        
        #endregion

        #region Command Handlers


        #endregion

        #region Event Handlers

        private void AddClassCardHandlers(ClassCard card)
        {
            card.MouseLeftButtonUp += ClassCardOnMouseLeftButtonUp;
            card.MouseRightButtonUp += ClassCardOnMouseRightButtonUp;
            card.MouseEnter += CardOnMouseEnter;
            card.MouseLeftButtonDown += CardOnMouseLeftButtonDown;
            card.MouseRightButtonDown += CardOnMouseRightButtonDown;
        }

        private void RemoveClassCardHandlers(ClassCard classCard)
        {
            classCard.MouseLeftButtonUp -= ClassCardOnMouseLeftButtonUp;
            classCard.MouseRightButtonUp -= ClassCardOnMouseRightButtonUp;
        }

        private void CardOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
        }

        private void CardOnMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
        }

        private void CardOnMouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed) return;
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
        }
        
        private void ClassCardOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            OpenContextMenu(classCard);
        }

        private void ClassCardOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        
        private void DropSelected()
        {
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = false;    
            }
        }
        
        private void UpdateSelection(ClassCard card)
        {
            var row = Grid.GetRow(card) - TitleRowsCount;
            var col = Grid.GetColumn(card) - TimeColumnsCount;
            _selectedCard = _classesTable.Table[row][col].Item;
            _selectedCard.IsSelected = true;
        }
        
        private void OpenContextMenu(ClassCard classCard)
        {
            var model = classCard.DataContext as ClassCardViewModel;
            if (model == null) return;
            var cm = new ContextMenu();
            cm.Items.Add(new MenuItem { Header = "Action"});
            cm.IsOpen = true;
        }


        #endregion
    }

    class Selection
    {
        
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public int EndRow { get; set; }
        public int EndColumn { get; set; }
        public int Top { get { return Math.Min(StartRow, EndRow); } }
        public int Bottom { get { return Math.Max(StartRow, EndRow); } }
        public int Left { get { return Math.Min(StartColumn, EndColumn); } }
        public int Right { get { return Math.Max(StartColumn, EndColumn); } }

        public Selection(int row, int column)
        {
            StartRow = row;
            StartColumn = column;
            EndRow = row;
            EndColumn = column;
        }

        public void UpdateEnd(int row, int col)
        {
            EndRow = row;
            EndColumn = col;
        }
    }
}
