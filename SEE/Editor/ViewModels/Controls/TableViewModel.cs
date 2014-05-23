using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.ViewModels.Cards;
using Editor.ViewModels.Helpers;
using Editor.Views.Cards;
using Editor.Views.Controls;
using ScheduleData;
using ScheduleData.SearchConflicts;

namespace Editor.ViewModels.Controls
{
    public class TableViewModel : HasProjectProperty
    {

        public static int TimeColumnsCount = 2;
        public static int TitleRowsCount = 2;
        private const int DayMarginOffset = 5;
        private static readonly int ClassesPerDayMax = ClassTime.ClassIntervals.Length;

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

        public LeftTopControl LeftTopControl;

        public ClassCardViewMode[][] ClassesCards;
        private ClassesTable _classesTable;
        private TimeLineMarkup _timeLineMarkup;
        private TitlesMarkup _titlesMarkup;

        private ClassCardViewModel _selectedCard;
        private int _selectedRow, _selectedColumn;

        private readonly Action _updateViews;

        #region Ctor

        public TableViewModel(Action updateViews)
        {
            _updateViews = updateViews;
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
                    var time = conflictingClass.ClassTime;
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

        private void InitializeLeftTop()
        {
            var viewModel = new LeftTopContolViewModel(_updateViews) {Project = Project};
            LeftTopControl = new LeftTopControl { DataContext = viewModel };
            Grid.SetRow(LeftTopControl, 0);
            Grid.SetColumn(LeftTopControl, 0);
            Grid.SetRowSpan(LeftTopControl, 2);
            Grid.SetColumnSpan(LeftTopControl, 2);
        }

        private void InitializeTitles()
        {
            Titles = new ObservableCollection<UIElement>();
            foreach (var title in _titlesMarkup.Titles)
            {
                var tvm = new TitleCardViewModel(title.Item, _updateViews) { Project = Project };
                var tc = new TitleCard { DataContext = tvm };
                Grid.SetRow(tc, title.Row);
                Grid.SetColumn(tc, TitleRowsCount + title.Column);
                Grid.SetRowSpan(tc, title.RowSpan);
                Grid.SetColumnSpan(tc, title.ColumnSpan);
                Titles.Add(tc);
            }
            foreach (var title in _titlesMarkup.Subtitles)
            {
                var tvm = new SubtitleCardViewModel(title.Item, _updateViews) { Project = Project };
                var tc = new SubtitleCard { DataContext = tvm };
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
                dc.Margin = new Thickness(0, 0, 0, DayMarginOffset);
                DayLine.Add(dc);
            }
        }

        private void InitTimeIntervalLine()
        {
            TimeIntervals = new ObservableCollection<UIElement>();
            var i = 0;
            foreach (var classInterval in _timeLineMarkup.ClassesIntervals)
            {
                var tvm = new TimeCardViewModel(classInterval.Item);
                var tc = new TimeCard { DataContext = tvm };
                Grid.SetRow(tc, TitleRowsCount + classInterval.Row);
                Grid.SetColumn(tc, classInterval.Column);
                Grid.SetRowSpan(tc, classInterval.RowSpan);
                Grid.SetColumnSpan(tc, classInterval.ColumnSpan);
                if ((++i) % ClassesPerDayMax == 0)
                {
                    tc.Margin = new Thickness(0, 0, 0, DayMarginOffset);
                }
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
            if (Project.ClassesSchedule.TimeLine[row].Number + 1 == ClassesPerDayMax)
            {
                classCard.Margin = new Thickness(0, 0, 0, DayMarginOffset);
            }
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

        public ICommand EditClassCommand { get { return new DelegateCommand(OnEditClass, HasSelectedCard); } }
        public ICommand CopyClassCommand { get { return new DelegateCommand(OnCopyClassCommand, HasSelectedCard); } }
        public ICommand PasteClassCommand { get { return new DelegateCommand(OnPasteClassCommand, HasSelectedCard); } }
        public ICommand SendToCardClipboardCommand { get { return new DelegateCommand(OnSendToCardClipboard, HasSelectedCard); } }
        public ICommand CutClassCommand { get { return new DelegateCommand(OnCutClass, CanExecuteHasClass); } }
        public ICommand DeleteClassCommand { get { return new DelegateCommand(OnDeleteClass, CanExecuteHasClass); } }

        public ICommand SelectionMoveUpCommand { get { return new DelegateCommand(OnSelectionMoveUp, CanExecuteSelectionMoveUp); } }
        public ICommand SelectionMoveDownCommand { get { return new DelegateCommand(OnSelectionMoveDown, CanExecuteSelectionMoveDown); } }
        public ICommand SelectionMoveRightCommand { get { return new DelegateCommand(OnSelectionMoveRight, CanExecuteSelectionMoveRight); } }
        public ICommand SelectionMoveLeftCommand { get { return new DelegateCommand(OnSelectionMoveLeft, CanExecuteSelectionMoveLeft); } }

        #endregion

        #region Command Handlers

        #region Move selection

        private bool CanExecuteSelectionMoveLeft()
        {
            return HasSelectedCard() && _selectedColumn > 0;
        }

        private void OnSelectionMoveLeft()
        {
            UpdateSelection(_selectedRow, _selectedColumn - 1);
        }

        private bool CanExecuteSelectionMoveRight()
        {
            return HasSelectedCard() && _selectedColumn < ClassesColumnsCount - 1;
        }

        private void OnSelectionMoveRight()
        {
            UpdateSelection(_selectedRow, _selectedColumn + 1);
        }

        private bool CanExecuteSelectionMoveDown()
        {
            return HasSelectedCard() && _selectedRow < ClassesRowsCount - 1;
        }

        private void OnSelectionMoveDown()
        {
            UpdateSelection(_selectedRow + 1, _selectedColumn);
        }

        private bool CanExecuteSelectionMoveUp()
        {
            return HasSelectedCard() && _selectedRow > 0;
        }

        private void OnSelectionMoveUp()
        {
            UpdateSelection(_selectedRow - 1, _selectedColumn);
        }

        #endregion

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
        }

        #endregion

        #region Cut


        private void OnCutClass()
        {
            if (_selectedCard == null || _selectedCard.Class == null) return;
            var classCard = ClassesCards[_selectedRow][_selectedColumn];
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

        #endregion

        #region Delete

        private void OnDeleteClass()
        {
            if (_selectedCard == null || _selectedCard.Class == null) return;
            var classCard = ClassesCards[_selectedRow][_selectedColumn];
            if (classCard == null) return;
            var row = Grid.GetRow(classCard) - TitleRowsCount;
            var col = Grid.GetColumn(classCard) - TimeColumnsCount;

            var vmodel = classCard.DataContext as ClassCardViewModel;
            if (vmodel == null) return;

            vmodel.Class = null;
            _classesTable.Table[row][col] = null;
        }

        #endregion
        
        #region Edit

        private void OnEditClass()
        {
            if (_selectedCard == null) return;
            var classCard = ClassesCards[_selectedRow][_selectedColumn];
            if (classCard == null) return;
            OpenCardEditor(classCard);
        }

        #endregion

        private bool HasSelectedCard()
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
            UpdateSelection(classCard);
        }

        private void CardOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            UpdateSelection(classCard);
            if (e.ClickCount == 2)
            {
                OnEditClass();
            }
        }
        
        private void CardOnMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            UpdateSelection(classCard);
        }

        private void ClassCardOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }
        
        private void ClassCardOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
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
            UpdateSelection(row, col);
        }

        private void UpdateSelection(int row, int col)
        {
            DropSelected();
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
            var @class = _classesTable.Table[row][col] ?? new ClassRecord();
            var model = new ClassCardViewModel(@class) { Project = Project };
            var edit = new ClassCardEditMode(centerX, centerY) { DataContext = model };
            edit.ShowDialog();
            if (@class.Classroom != null || @class.Lecturer != null || @class.Subject != null)
            {
                _classesTable.Table[row][col] = @class;
                ClassesCards[row][col].DataContext = model;
            }
            else
            {
                ClassesCards[row][col].DataContext = new ClassCardViewModel(null) { Project = Project }; ;
            }
            UpdateSelection(row, col);
        }
        
        private void OpenContextMenu(ClassCardViewMode classCard)
        {
            var model = classCard.DataContext as ClassCardViewModel;
            if (model == null) return;
            var cm = new ContextMenu();
            cm.Items.Add(new MenuItem { Header = "Редактировать", Command = EditClassCommand});
            cm.Items.Add(new MenuItem { Header = "Скопировать", Command = CopyClassCommand, InputGestureText = "Ctrl+C" });
            cm.Items.Add(new MenuItem { Header = "Вставить", Command = PasteClassCommand, InputGestureText = "Ctrl+V" });
            cm.Items.Add(new MenuItem { Header = "Вырезать", Command = CutClassCommand, InputGestureText = "Ctrl+X" });
            cm.Items.Add(new MenuItem { Header = "Удалить", Command = DeleteClassCommand, InputGestureText = "Del" });
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
            InitializeLeftTop();
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


    }
}
