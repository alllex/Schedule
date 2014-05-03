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
using ScheduleData.Interfaces;

namespace Editor.ViewModels
{
    class ClassCardViewModel : BaseViewModel
    {

        #region Properties

        #region Subject

        private string _subject = "";
        public string Subject
        {
            get { return _subject; }
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    foreach (var @class in _classes)
                    {
                        @class.Subject.Name = value;
                    }
                    RaisePropertyChanged(() => Subject);
                }
            }
        }

        #endregion

        #region Lecturer

        private string _lecturer = "";
        public string Lecturer
        {
            get { return _lecturer; }
            set
            {
                if (_lecturer != value)
                {
                    _lecturer = value;
                    foreach (var @class in _classes)
                    {
                        @class.Lecturer.Name = value;
                    }
                    RaisePropertyChanged(() => Lecturer);
                }
            }
        }

        #endregion

        #region Group

        private string _group = "";
        public string Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    foreach (var @class in _classes)
                    {
                        @class.Group.Name = value;
                    }
                    RaisePropertyChanged(() => Group);
                }
            }
        }

        #endregion

        #region Classroom

        private string _classroom = "";
        public string Classroom
        {
            get { return _classroom; }
            set
            {
                if (_classroom != value)
                {
                    _classroom = value;
                    foreach (var @class in _classes)
                    {
                        @class.Classroom.Name = value;
                    }
                    RaisePropertyChanged(() => Classroom);
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

        #region IsEmpty

        public bool IsEmpty
        {
            get { return _classes.Any(); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand SetEditModeCommand { get { return new DelegateCommand(OnSetEditMode, CanExecuteSetEditMode); } }
        public ICommand SetViewModeCommand { get { return new DelegateCommand(OnSetViewMode, CanExecuteSetViewMode); } }
        public ICommand ClickCommand { get { return new DelegateCommand(OnClick); } }

        #endregion

        #region Ctor

        private List<IClass> _classes;

        public ClassCardViewModel(IClass @class)
        {
            if (@class != null)
            {
                _classes = new List<IClass> { @class };
                Subject = @class.Subject.Name;
                Lecturer = @class.Lecturer.Name;
                Group = @class.Group.Name;
                Classroom = @class.Classroom.Name;
            }
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
            ui.RaiseEvent(new RoutedEventArgs(ClassCard.ClickEvent));
        }


        #endregion
    }
}
