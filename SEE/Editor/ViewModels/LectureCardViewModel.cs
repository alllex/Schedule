using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using ScheduleData;

namespace Editor.ViewModels
{
    class LectureCardViewModel : BaseViewModel
    {
        #region Properties

        #region Subject

        public string Subject
        {
            get { return _lecture.Subject.Name; }
            set
            {
                if (_lecture.Subject.Name != value)
                {
                    _lecture.Subject.Name = value;
                    RaisePropertyChanged(() => Subject);
                }
            }
        }

        #endregion

        #region Lecturer

        public string Lecturer
        {
            get { return  _lecture.Lecturer.Name; }
            set
            {
                if ( _lecture.Lecturer.Name != value)
                {
                     _lecture.Lecturer.Name = value;
                    RaisePropertyChanged(() => Lecturer);
                }
            }
        }

        #endregion

        #region Group

        public string Group
        {
            get { return  _lecture.Group.Name; }
            set
            {
                if ( _lecture.Group.Name != value)
                {
                    _lecture.Group.Name = value;
                    RaisePropertyChanged(() => Group);
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

        #endregion

        #region Commands

        public ICommand SetEditModeCommand { get { return new DelegateCommand(OnSetEditMode, CanExecuteSetEditMode); } }
        public ICommand SetViewModeCommand { get { return new DelegateCommand(OnSetViewMode, CanExecuteSetViewMode); } }

        #endregion

        #region Ctor

        private ILecture _lecture;
        public LectureCardViewModel(ILecture lecture)
        {
            _lecture = lecture;
            IsEditing = false;
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

        #endregion

    }
}
