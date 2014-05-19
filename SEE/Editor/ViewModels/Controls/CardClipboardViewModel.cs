using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.ViewModels.Cards;
using Editor.Views.Cards;

namespace Editor.ViewModels.Controls
{
    class CardClipboardViewModel : BaseViewModel    
    {

        #region Project

        private ScheduleProject _project;

        public ScheduleProject Project
        {
            get { return _project; }
            set
            {
                if (_project != value)
                {
                    _project = value;
                    RaisePropertyChanged(() => Project);
                }
            }
        }

        #endregion

        //private ClassCardViewModel _selectedCard;

        public CardClipboardViewModel(ScheduleProject project)
        {
            Project = project;
            //Project.CardClipboard.CollectionChanged += CardClipboardOnCollectionChanged;
        }

//        private void CardClipboardOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//        {
//            if (e.NewItems == null) return;
//            switch (e.Action)
//            {
//                case NotifyCollectionChangedAction.Remove:
//                    foreach (var item in e.NewItems)
//                    {
//                        //Removed items
//                        if (item == null) continue;
//                        RemoveClassCardHandlers(item as ClassCardViewModel);
//                    }
//                    break;
//                case NotifyCollectionChangedAction.Add:
//                    foreach (var item in e.NewItems)
//                    {
//                        //Added items
//                        if (item == null) continue;
//                        AddClassCardHandlers(item as ClassCardViewModel);
//                    }
//                    break;
//            }
//        }
//
//        #region Commands
//
//        public ICommand CopyCardCommand { get { return new DelegateCommand(OnCopyClassCommand); } }
//        public ICommand CutCardCommand { get { return new DelegateCommand(OnCutCard); } }
//
//        #endregion
//
//        #region Command Handlers
//
//        #region Copy
//
//        private void OnCopyClassCommand()
//        {
//            if (_selectedCard == null || _selectedCard.Class == null) return;
//            var @class = new ClassRecord();
//            ClassRecord.Copy(_selectedCard.Class, @class);
//            ClipboardService.SetData(@class);
//        }
//
//        #endregion
//        
//        private void OnCutCard()
//        {
//            if (_selectedCard == null || _selectedCard.Class == null) return;
//            var @class = new ClassRecord();
//            ClassRecord.Copy(_selectedCard.Class, @class);
//            ClipboardService.SetData(@class);
//            Project.CardClipboard.Remove(_selectedCard);
//        }
//
//        #endregion
//
//        #region Event Handlers
//
//        private void AddClassCardHandlers(ClassCardViewMode card)
//        {
//            card.MouseLeftButtonUp += ClassCardOnMouseLeftButtonUp;
//            card.MouseRightButtonUp += ClassCardOnMouseRightButtonUp;
//            card.MouseEnter += CardOnMouseEnter;
//            card.MouseLeftButtonDown += CardOnMouseLeftButtonDown;
//            card.MouseRightButtonDown += CardOnMouseRightButtonDown;
//        }
//
//        private void RemoveClassCardHandlers(ClassCardViewMode card)
//        {
//            card.MouseLeftButtonUp -= ClassCardOnMouseLeftButtonUp;
//            card.MouseRightButtonUp -= ClassCardOnMouseRightButtonUp;
//            card.MouseEnter -= CardOnMouseEnter;
//            card.MouseLeftButtonDown -= CardOnMouseLeftButtonDown;
//            card.MouseRightButtonDown -= CardOnMouseRightButtonDown;
//        }
//
//        private void CardOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//        {
//            var classCard = sender as ClassCardViewMode;
//            if (classCard == null) return;
//            DropSelected();
//            UpdateSelection(classCard);
//            if (e.ClickCount == 2)
//            {
//                OpenCardEditor(classCard);
//            }
//        }
//        
//        private void CardOnMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
//        {
//            var classCard = sender as ClassCardViewMode;
//            if (classCard == null) return;
//            DropSelected();
//            UpdateSelection(classCard);
//        }
//
//        private void CardOnMouseEnter(object sender, MouseEventArgs e)
//        {
//            if (e.LeftButton != MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed) return;
//            var classCard = sender as ClassCardViewMode;
//            if (classCard == null) return;
//            DropSelected();
//            UpdateSelection(classCard);
//        }
//
//        private void ClassCardOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
//        {
//        }
//        
//        private void ClassCardOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
//        {
//            var classCard = sender as ClassCardViewMode;
//            if (classCard == null) return;
//            OpenContextMenu(classCard);
//        }
//        
//        private void DropSelected()
//        {
//            if (_selectedCard != null)
//            {
//                _selectedCard.IsSelected = false;
//            }
//        }
//        
//        private void UpdateSelection(ClassCardViewMode card)
//        {
//            _selectedCard = card.DataContext as ClassCardViewModel;
//            if (_selectedCard == null) return;
//            _selectedCard.IsSelected = true;
//        }
//
//        private void OpenCardEditor(ClassCardViewMode card)
//        {
////            Point position = card.PointToScreen(new Point(0d, 0d));
////            var centerY = position.Y + (card.ActualHeight) / 2;
////            var centerX = position.X + (card.ActualWidth) / 2;
////
////            var row = Grid.GetRow(card) - TitleRowsCount;
////            var col = Grid.GetColumn(card) - TimeColumnsCount;
////            var @class = _classesTable.Table[row][col];
////            if (@class == null)
////            {
////                _classesTable.Table[row][col] = new ClassRecord();
////
////            }
////            var model = new ClassCardViewModel(@class){ClassesSchedule = ClassesSchedule};
////            var edit = new ClassCardEditMode(centerX, centerY) { DataContext = model };
////
////            edit.ShowDialog();
//        }
//        
//        private void OpenContextMenu(ClassCardViewMode classCard)
//        {
//            var model = classCard.DataContext as ClassCardViewModel;
//            if (model == null) return;
//            var cm = new ContextMenu();
//            cm.Items.Add(new MenuItem { Header = "Copy", Command = CopyCardCommand });
//            cm.Items.Add(new MenuItem { Header = "Cut", Command = CutCardCommand });
//            cm.IsOpen = true;
//        }
//
//        #endregion
    }
}
