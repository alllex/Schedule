﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.Models.SearchConflicts;
using Editor.ViewModels.Cards;
using Editor.ViewModels.Helpers;
using Editor.Views.Cards;

namespace Editor.ViewModels.Controls
{
    public class TableViewModel : HasProjectProperty
    {

        public static int TimeColumnsCount = 2;
        public static int TitleRowsCount = 2;

        #region Properties

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
                    RaisePropertyChanged(() => YearOfStudy);
                }
            }
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
        public ClassCardViewMode[][] ClassesCards;
        private ClassesTable _classesTable;
        private TimeLineMarkup _timeLineMarkup;
        private TitlesMarkup _titlesMarkup;

        private ClassCardViewModel _selectedCard;
        private int _selectedRow, _selectedColumn;

        #region Ctor

        public TableViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        #endregion

        #region Public

        public void ShowConflicts()
        {
            if (Project == null || Project.ConflictCompilation == null || Project.ConflictCompilation.Conflicts == null) return;
            foreach (var conflict in Project.ConflictCompilation.Conflicts)
            {
                foreach (var conflictingClass in conflict.ConflictingClasses)
                {
                    var group = conflictingClass.Group;
                    if (group.YearOfStudy != _classesTable.YearOfStudy) continue;
                    var time = conflictingClass.Time;
                    var row = _classesTable.TimeIndexes[time];
                    var column = _classesTable.GroupIndexes[group];
                    if (row < 0 || column < 0 || row >= ClassesRowsCount || column >= ClassesColumnsCount)
                    {
                        continue;
                    }
                    var vmodel = ClassesCards[row][column].DataContext as ClassCardViewModel;
                    if (vmodel == null) continue;
                    switch (conflict.ConflictType)
                    {
                        case ConflictType.Warning:
                            vmodel.HasWarning = true;
                            break;
                        case ConflictType.Conflict:
                            vmodel.HasConflict = true;
                            break;
                    }
                }
            }
        }

        public void HideConflicts()
        {
            for (var row = 0; row < ClassesRowsCount; row++)
            {
                for (var col = 0; col < ClassesColumnsCount; col++)
                {
                    var vmodel = ClassesCards[row][col].DataContext as ClassCardViewModel;
                    if (vmodel == null) continue;
                    vmodel.HasConflict = false;
                    vmodel.HasWarning = false;
                }
            }
        }

        #endregion

        #region Initialization

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
            ClassesCards = new ClassCardViewMode[ClassesRowsCount][];
            for (var row = 0; row < ClassesRowsCount; row++)
            {
                ClassesCards[row] = new ClassCardViewMode[ClassesColumnsCount];
                for (var col = 0; col < ClassesColumnsCount; col++)
                {
                    ClassesCards[row][col] = CreateClassCard(row, col);
                }
            }
        }

        private ClassCardViewMode CreateClassCard(int row, int column)
        {
            var viewModel = new ClassCardViewModel(_classesTable.Table[row][column]) { Project = Project };
            var classCard = new ClassCardViewMode { DataContext = viewModel };
            Grid.SetRow(classCard, row + TitleRowsCount);
            Grid.SetColumn(classCard, column + TimeColumnsCount);
            AddClassCardHandlers(classCard);
            return classCard;
        }

        public int TableWidth()
        {
            return TimeColumnsCount + ClassesColumnsCount;
        }

        public int TableHeight()
        {
            return TitleRowsCount + ClassesRowsCount;
        }

        #endregion

        #region Commands

        public ICommand EditClassCommand { get { return new DelegateCommand(OnEditClass, CanExecuteEditClass); } }
        public ICommand CopyClassCommand { get { return new DelegateCommand(OnCopyClassCommand); } }
        public ICommand PasteClassCommand { get { return new DelegateCommand(OnPasteClassCommand); } }
        public ICommand SendToCardClipboardCommand { get { return new DelegateCommand(OnSendToCardClipboard); } }
        public ICommand CutClassCommand { get { return new DelegateCommand(OnCutClass, CanExecuteHasClass); } }
        public ICommand DeleteClassCommand { get { return new DelegateCommand(OnDeleteClass, CanExecuteHasClass); } }
        
        #endregion

        #region Command Handlers

        #region Copy

        private void OnCopyClassCommand()
        {
            if (_selectedCard == null || _selectedCard.Class == null) return;
            var @class = new ClassRecord();
            ClassRecord.Copy(_selectedCard.Class, @class);
            ClipboardService.SetData(@class);
        }

        #endregion

        #region Paste

        private void OnPasteClassCommand()
        {
            if (_selectedCard == null) return;
            if (_selectedCard.Class == null)
            {
                _classesTable.Table[_selectedRow][_selectedColumn] = new ClassRecord();
                _selectedCard.Class = _classesTable.Table[_selectedRow][_selectedColumn];
            }
            var cliped = ClipboardService.GetData<ClassRecord>();
            ClassRecord.Copy(cliped, _selectedCard.Class);
            //ClassesCards[_selectedRow][_selectedColumn].DataContext = _selectedCard;
        }

        #endregion

        private void OnCutClass(object param)
        {
            if (_selectedCard == null || _selectedCard.Class == null) return;
            var classCard = param as ClassCardViewMode;
            if (classCard == null) return;
            var row = Grid.GetRow(classCard) - TitleRowsCount;
            var col = Grid.GetColumn(classCard) - TimeColumnsCount;

            var @class = new ClassRecord();
            ClassRecord.Copy(_classesTable.Table[row][col], @class);
            ClipboardService.SetData(@class);

            var vmodel = classCard.DataContext as ClassCardViewModel;
            if (vmodel == null) return;

            vmodel.Class = null;
            _classesTable.Table[row][col] = null;

        }
        
        private void OnDeleteClass(object param)
        {
            if (_selectedCard == null || _selectedCard.Class == null) return;
            var classCard = param as ClassCardViewMode;
            if (classCard == null) return;
            var row = Grid.GetRow(classCard) - TitleRowsCount;
            var col = Grid.GetColumn(classCard) - TimeColumnsCount;

            var vmodel = classCard.DataContext as ClassCardViewModel;
            if (vmodel == null) return;

            vmodel.Class = null;
            _classesTable.Table[row][col] = null;
        }

        private void OnEditClass(object param)
        {
            if (_selectedCard == null) return;
            var classCard = param as ClassCardViewMode;
            if (classCard == null) return;
            OpenCardEditor(classCard);
        }

        private bool CanExecuteEditClass()
        {
            return _selectedCard != null;
        }

        private void OnSendToCardClipboard()
        {
//            if (_selectedCard == null || _selectedCard.Class == null) return;
//            var model = new ClassCardViewModel(_selectedCard.Class);
//            Project.CardClipboard.Add(model);
        }

        private bool CanExecuteHasClass()
        {
            return _selectedCard != null && _selectedCard.Class != null;
        }

        #endregion

        #region Event Handlers

        private void AddClassCardHandlers(ClassCardViewMode card)
        {
            card.MouseLeftButtonUp += ClassCardOnMouseLeftButtonUp;
            card.MouseRightButtonUp += ClassCardOnMouseRightButtonUp;
            card.MouseEnter += CardOnMouseEnter;
            card.MouseLeave += CardOnMouseLeave;
            card.MouseLeftButtonDown += CardOnMouseLeftButtonDown;
            card.MouseRightButtonDown += CardOnMouseRightButtonDown;
        }

        private void RemoveClassCardHandlers(ClassCardViewMode card)
        {
            card.MouseLeftButtonUp -= ClassCardOnMouseLeftButtonUp;
            card.MouseRightButtonUp -= ClassCardOnMouseRightButtonUp;
            card.MouseEnter -= CardOnMouseEnter;
            card.MouseLeave -= CardOnMouseLeave;
            card.MouseLeftButtonDown -= CardOnMouseLeftButtonDown;
            card.MouseRightButtonDown -= CardOnMouseRightButtonDown;
        }
        
        private void CardOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            //DropSelected();
        }

        private void CardOnMouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed) return;
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
        }

        private void CardOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
            if (e.ClickCount == 2)
            {
                OpenCardEditor(classCard);
            }
        }
        
        private void CardOnMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
        }

        private void ClassCardOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }
        
        private void ClassCardOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            DropSelected();
            UpdateSelection(classCard);
            OpenContextMenu(classCard);
        }
        
        private void DropSelected()
        {
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = false;
                _selectedRow = 0;
                _selectedColumn = 0;
            }
        }
        
        private void UpdateSelection(ClassCardViewMode card)
        {
            var row = Grid.GetRow(card) - TitleRowsCount;
            var col = Grid.GetColumn(card) - TimeColumnsCount;
            _selectedRow = row;
            _selectedColumn = col;
            _selectedCard = ClassesCards[row][col].DataContext as ClassCardViewModel;
            if (_selectedCard == null) return;
            _selectedCard.IsSelected = true;
        }

        private void OpenCardEditor(ClassCardViewMode card)
        {
            Point position = card.PointToScreen(new Point(0d, 0d));
            var centerY = position.Y + (card.ActualHeight) / 2;
            var centerX = position.X + (card.ActualWidth) / 2;

            var row = Grid.GetRow(card) - TitleRowsCount;
            var col = Grid.GetColumn(card) - TimeColumnsCount;
            var @class = _classesTable.Table[row][col];
            if (@class == null)
            {
                _classesTable.Table[row][col] = new ClassRecord();
                @class = _classesTable.Table[row][col];
            }
            var model = new ClassCardViewModel(@class) { Project = Project };
            var edit = new ClassCardEditMode(centerX, centerY) { DataContext = model };
            edit.ShowDialog();
            ClassesCards[row][col].DataContext = model;
            _selectedCard = model;
        }
        
        private void OpenContextMenu(ClassCardViewMode classCard)
        {
            var model = classCard.DataContext as ClassCardViewModel;
            if (model == null) return;
            var cm = new ContextMenu();
            cm.Items.Add(new MenuItem { Header = "Edit", Command = EditClassCommand, CommandParameter = classCard});
            cm.Items.Add(new MenuItem { Header = "Copy", Command = CopyClassCommand });
            cm.Items.Add(new MenuItem { Header = "Paste", Command = PasteClassCommand });
            cm.Items.Add(new MenuItem { Header = "Cut", Command = CutClassCommand, CommandParameter = classCard });
            cm.Items.Add(new MenuItem { Header = "Delete", Command = DeleteClassCommand, CommandParameter = classCard });
            //cm.Items.Add(new MenuItem { Header = "Send to Clipboard", Command = SendToCardClipboardCommand });
            cm.IsOpen = true;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var s = sender as TableViewModel;
            if (s == null) return;
            if (e.PropertyName == "Project")
            {
                OnProjectChanged();
            }
            else if (e.PropertyName == "YearOfStudy")
            {
                OnYearOfStudyChanged();
            }
        }

        private void OnYearOfStudyChanged()
        {
            if (YearOfStudy == null) return;
            TableHeader = YearOfStudy.ToString();
            _classesTable = Project.ClassesSchedule.GetClassesTable(YearOfStudy);
            _titlesMarkup = new TitlesMarkup(_classesTable.Groups);
            InitializeTitles();
            InitLectureCards();
        }

        private void OnProjectChanged()
        {
            _timeLineMarkup = new TimeLineMarkup(Project.ClassesSchedule);
            InitDayLine();
            InitTimeIntervalLine();

            if (YearOfStudy != null)
            {
                OnYearOfStudyChanged();
            }
        }

        #endregion

        #region Delegate



        #endregion
    }
}
