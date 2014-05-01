using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.UserControls;
using ScheduleData;

namespace Editor.ViewModels
{
    class LectureCardViewModel : BaseViewModel
    {

        #region Properties

        #region Subject

        public string Subject
        {
            get { return _class.Subject.Name; }
            set
            {
                if (_class.Subject.Name != value)
                {
                    _class.Subject.Name = value;
                    RaisePropertyChanged(() => Subject);
                }
            }
        }

        #endregion

        #region Lecturer

        public string Lecturer
        {
            get { return  _class.Lecturer.Name; }
            set
            {
                if ( _class.Lecturer.Name != value)
                {
                     _class.Lecturer.Name = value;
                    RaisePropertyChanged(() => Lecturer);
                }
            }
        }

        #endregion

        #region Group

        public string Group
        {
            get { return  _class.Group.Name; }
            set
            {
                if ( _class.Group.Name != value)
                {
                    _class.Group.Name = value;
                    RaisePropertyChanged(() => Group);
                }
            }
        }

        #endregion

        #region Room

        public string Room
        {
            get { return _class.Classroom.Name; }
            set
            {
                if (_class.Classroom.Name != value)
                {
                    _class.Classroom.Name = value;
                    RaisePropertyChanged(() => Room);
                }
            }
        }

        #endregion

        #region IsEditing

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    RaisePropertyChanged(() => IsEditing);
                }
            }
        }

        #endregion

        #region IsSelected

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    if (!value)
                    {
                        IsEditing = false;
                    }
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand SetEditModeCommand { get { return new DelegateCommand(OnSetEditMode, CanExecuteSetEditMode); } }
        public ICommand SetViewModeCommand { get { return new DelegateCommand(OnSetViewMode, CanExecuteSetViewMode); } }
        public ICommand ClickCommand { get { return new DelegateCommand(OnClick); } }

        #endregion

        #region Ctor

        private IClass _class;

        public LectureCardViewModel(IClass @class)
        {
            _class = @class;
            IsEditing = false;
            IsSelected = false;
        }

        #endregion

        #region Command Handlers

        private void OnSetEditMode()
        {
            IsEditing = true;
        }

        private bool CanExecuteSetEditMode()
        {
            return !IsEditing;
        }

        private void OnSetViewMode()
        {
            IsEditing = false;
        }

        private bool CanExecuteSetViewMode()
        {
            return IsEditing;
        }

        private void OnClick(object parameter)
        {
            var ui = (UIElement) parameter;
            if (ui == null)
            {
                MessageBox.Show("Cannot cast to UIElement");
                return;
            }
            ui.RaiseEvent(new RoutedEventArgs(LectureCard.ClickEvent));
        }


        #endregion
    }
}
