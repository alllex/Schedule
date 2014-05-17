using System;
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

        #region ClassesCards

        private ObservableCollection<UIElement> _classesCards = new ObservableCollection<UIElement>();
        public ObservableCollection<UIElement> ClassesCards
        {
            get { return _classesCards; }
            set
            {
                if (_classesCards != value)
                {
                    _classesCards = value;
                    RaisePropertyChanged(() => ClassesCards);
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

        private ClassesTable _classesTable;
        private TimeLineMarkup _timeLineMarkup;
        private TitlesMarkup _titlesMarkup;
        private Selection _selection;
        private Collection<ClassCardViewModel> _selectedCards = new Collection<ClassCardViewModel>();

        public TableViewModel()
        {
            ClassesCards.CollectionChanged += ClassesCardsOnCollectionChanged;
        }

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
            ClassesCards.Clear();  
            for (int row = 0; row < _classesTable.RowsCount(); row++)
            {
                for (int col = 0; col < _classesTable.ColumnsCount(); col++)
                {
                    ClassesCards.Add(CreateClassCard(row, col));
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

        public ICommand JoinClassesCommand { get { return new DelegateCommand(OnJoinClassesCommand, CanExecuteJoinClasses); } }
        
        #endregion

        #region Command Handlers

        private bool CanExecuteJoinClasses()
        {
            return _selectedCards.Count() > 1;
        }

        private void OnJoinClassesCommand()
        {
            var mainClass = _classesTable.Table[_selection.Top][_selection.Left];
            var row = _selection.Top;
            var col = _selection.Left;
            mainClass.RowSpan = _selection.Bottom - _selection.Top + 1;
            mainClass.ColumnSpan = _selection.Right - _selection.Left + 1;
            ClassesCards.RemoveAt(row * _classesTable.ColumnsCount() + col);
            ClassesCards.Add(CreateClassCard(row, col));
            if (ClassesJoinedDelegate != null)
            {
                ClassesJoinedDelegate(this);
            }
        }

        public delegate void ClassesJoined(TableViewModel t);
        public ClassesJoined ClassesJoinedDelegate { get; set; }

        #endregion

        #region Event Handlers
        
        private void ClassesCardsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    if (e.NewItems == null) return;
                    foreach (ClassCard classCard in e.NewItems)
                    {
                        //Removed items
                        classCard.MouseLeftButtonDown -= ClassCardOnMouseLeftButtonDown;
                        classCard.MouseLeftButtonUp -= ClassCardOnMouseLeftButtonUp;
                        classCard.MouseEnter -= ClassCardOnMouseEnter;
                        classCard.MouseLeave -= ClassCardOnMouseLeave;
                        classCard.MouseRightButtonUp -= ClassCardOnMouseRightButtonUp;
                    }
                    break;
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems == null) return;
                    foreach (ClassCard classCard in e.NewItems)
                    {
                        //Added items
                        classCard.MouseLeftButtonDown += ClassCardOnMouseLeftButtonDown;
                        classCard.MouseLeftButtonUp += ClassCardOnMouseLeftButtonUp;
                        classCard.MouseEnter += ClassCardOnMouseEnter;
                        classCard.MouseLeave += ClassCardOnMouseLeave;
                        classCard.MouseRightButtonUp += ClassCardOnMouseRightButtonUp;
                    }
                    break;
            }
        }

        private void ClassCardOnMouseRightButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            OpenContextMenu(classCard);
        }

        private void ClassCardOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            CreateSelection(classCard);
        }

        private void ClassCardOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            //var classCard = sender as ClassCard;
            //if (classCard == null) return;
            //FixSelection(classCard);
        }

        private void ClassCardOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.LeftButton != MouseButtonState.Pressed) return;
            var classCard = sender as ClassCard;
            if (classCard == null) return;
            UpdateSelection(classCard);
        }

        private void ClassCardOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            //if (!selection.IsSelecting) return;
        }

        private void DropSelected()
        {
            foreach (var selectedCard in _selectedCards)
            {
                if (selectedCard == null) continue;
                selectedCard.IsSelected = false;
                selectedCard.IsEditing = false;
            }
            _selectedCards.Clear();
        }

        private void CreateSelection(ClassCard cc)
        {
            var model = cc.DataContext as ClassCardViewModel;
            if (model == null) return;
            var row = Grid.GetRow(cc) - TitleRowsCount;
            var col = Grid.GetColumn(cc) - TimeColumnsCount;
            _selection = new Selection(row, col);
            DropSelected();
            UpdateSelection(cc);
        }

        private void UpdateSelection(ClassCard card)
        {
            var row = Grid.GetRow(card) - TitleRowsCount;
            var col = Grid.GetColumn(card) - TimeColumnsCount;
            _selection.UpdateEnd(row, col);
            DropSelected();
            for (int r = _selection.Top; r <= _selection.Bottom; r++)
            {
                for (int c = _selection.Left; c <= _selection.Right; c++)
                {
                    var spanned = _classesTable.Table[r][c];
                    var @class = spanned.Item;
                    @class.IsSelected = true;
                    _selectedCards.Add(@class);
                }
            }
        }
        
        private void OpenContextMenu(ClassCard classCard)
        {
            var model = classCard.DataContext as ClassCardViewModel;
            if (model == null) return;
            if (!_selectedCards.Contains(model))
            {
                CreateSelection(classCard);
            }
            var cm = new ContextMenu();
            cm.Items.Add(new MenuItem { Header = "Объединить", Command = JoinClassesCommand});
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
