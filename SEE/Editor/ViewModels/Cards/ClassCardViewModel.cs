using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Editor.Helpers;
using Editor.Models;
using Editor.UserControls;

namespace Editor.ViewModels
{
    class ClassCardViewModel : BaseViewModel
    {

        #region Properties

        #region Class

        private Class _class;
        public Class Class
        {
            get { return _class; }
            set
            {
                if (_class != value)
                {
                    _class = value;
                    UpdateMirrorClasses();
                    RaisePropertyChanged(() => Class);
                }
            }
        }

        #endregion

        //#region TableItem

        //private TableItem<Class> _tableItem;
        //public TableItem<Class> TableItem
        //{
        //    get { return _tableItem; }
        //    set
        //    {
        //        if (!_tableItem.Equals(value))
        //        {
        //            _tableItem = value;
        //            Class = _tableItem.Item;
        //            RaisePropertyChanged(() => TableItem);
        //        }
        //    }
        //}

        //#endregion

        //#region Subject

        //private string _subject = "";
        //public string Subject
        //{
        //    get { return _subject; }
        //    set
        //    {
        //        if (_subject != value)
        //        {
        //            _subject = value;
        //            foreach (var @class in _classes)
        //            {
        //                @class.Subject.Name = value;
        //            }
        //            RaisePropertyChanged(() => Subject);
        //        }
        //    }
        //}

        //#endregion

        //#region Lecturer

        //private string _lecturer = "";
        //public string Lecturer
        //{
        //    get { return _lecturer; }
        //    set
        //    {
        //        if (_lecturer != value)
        //        {
        //            _lecturer = value;
        //            foreach (var @class in _classes)
        //            {
        //                @class.Lecturer.Name = value;
        //            }
        //            RaisePropertyChanged(() => Lecturer);
        //        }
        //    }
        //}

        //#endregion

        //#region Group

        //private string _group = "";
        //public string Group
        //{
        //    get { return _group; }
        //    set
        //    {
        //        if (_group != value)
        //        {
        //            _group = value;
        //            foreach (var @class in _classes)
        //            {
        //                @class.Group.Name = value;
        //            }
        //            RaisePropertyChanged(() => Group);
        //        }
        //    }
        //}

        //#endregion

        //#region Classroom

        //private string _classroom = "";
        //public string Classroom
        //{
        //    get { return _classroom; }
        //    set
        //    {
        //        if (_classroom != value)
        //        {
        //            _classroom = value;
        //            foreach (var @class in _classes)
        //            {
        //                @class.Classroom.Name = value;
        //            }
        //            RaisePropertyChanged(() => Classroom);
        //        }
        //    }
        //}

        //#endregion

        //#region SelectedClassroom

        //public Classroom SelectedClassroom
        //{
        //    get
        //    {
        //        var cs = ClassesSchedule.Classrooms.Where(classroom => classroom.Name == Classroom);
        //        var classrooms = cs as IList<Classroom> ?? cs.ToList();
        //        return classrooms.Any() ? classrooms.First() : null;
        //    }
        //    set
        //    {
        //        if (value != null && value.Name != Classroom)
        //        {
        //            Classroom = value.Name;
        //        }
        //    }
        //}

        //#endregion

        //#region SelectedLecturer

        //public Lecturer SelectedLecturer
        //{
        //    get
        //    {
        //        var cs = ClassesSchedule.Lecturers.Where(lecturer => lecturer.Name == Lecturer);
        //        var lecturers = cs as IList<Lecturer> ?? cs.ToList();
        //        return lecturers.Any() ? lecturers.First() : null;
        //    }
        //    set
        //    {
        //        if (value != null && value.Name != Lecturer)
        //        {
        //            Lecturer = value.Name;
        //        }
        //    }
        //}

        //#endregion

        //#region SelectedSubject

        //public Subject SelectedSubject
        //{
        //    get
        //    {
        //        var cs = ClassesSchedule.Subjects.Where(s => s.Name == Subject);
        //        var subjects = cs as IList<Subject> ?? cs.ToList();
        //        return subjects.Any() ? subjects.First() : null;
        //    }
        //    set
        //    {
        //        if (value != null && value.Name != Subject)
        //        {
        //            Subject = value.Name;
        //        }
        //    }
        //}

        //#endregion

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

        #endregion

        private ObservableCollectionEx<Class> _classes = new ObservableCollectionEx<Class>();

        #region Ctor

        public ClassCardViewModel(Class @class)
        {
            Class = @class;
            IsEditing = false;
            IsSelected = false;
        }

        public ClassCardViewModel()
        {
            IsEditing = false;
            IsSelected = false;
        }


        #endregion

        private void UpdateMirrorClasses()
        {
            foreach (var @class in _classes)
            {
                Class.CopyWithoutGroup(Class, @class);
            }
        }

        public void SetClasses(IEnumerable<Class> classes)
        {
            _classes.Clear();
            foreach (var @class in classes)
            {
                if (@class != null)
                {
                    _classes.Add(@class);
                }
            }
            if (!_classes.Any()) return;
            Class = _classes.First();
            UpdateMirrorClasses();
        }

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

        protected override void ClassesScheduleOnPropertyChanged()
        {
            
        }
    }
}
