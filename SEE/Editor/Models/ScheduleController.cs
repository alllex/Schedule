using System.Windows.Input;
using Editor.Helpers;
using Editor.ViewModels;
using ScheduleData;

namespace Editor.Models
{
    public class ScheduleController : HasProjectProperty
    {
        #region Delegates

        public delegate void AddClassRecord(ClassRecord classRecord);
        public AddClassRecord AddClassRecordDelegate { get; set; }
        public delegate void RemoveClassRecord(ClassRecord classRecord);
        public RemoveClassRecord RemoveClassRecordDelegate { get; set; }

        public delegate void AddYearOfStudy();
        public AddYearOfStudy AddYearOfStudyDelegate { get; set; }
        public delegate void RemoveYearOfStudy(YearOfStudy yearOfStudy);
        public RemoveYearOfStudy RemoveYearOfStudyDelegate { get; set; }

        public delegate void AddSpecialization(YearOfStudy yearOfStudy);
        public AddSpecialization AddSpecializationDelegate { get; set; }
        public delegate void RemoveSpecialization(Specialization specialization);
        public RemoveSpecialization RemoveSpecializationDelegate { get; set; }

        public delegate void AddGroup(Specialization specialization);
        public AddGroup AddGroupDelegate { get; set; }
        public delegate void RemoveGroup(Group group);
        public RemoveGroup RemoveGroupDelegate { get; set; }

        public delegate void AddLecturer(Lecturer lecturer);
        public AddLecturer AddLecturerDelegate { get; set; }
        public delegate void RemoveLecturer(Lecturer lecturer);
        public RemoveLecturer RemoveLecturerDelegate { get; set; }

        public delegate void AddSubject(Subject subject);
        public AddSubject AddSubjectDelegate { get; set; }
        public delegate void RemoveSubject(Subject subject);
        public RemoveSubject RemoveSubjectDelegate { get; set; }


        #endregion

        #region Commands

        public ICommand AddClassRecordCommand { get { return new DelegateCommand(OnAddClassRecord); } }
        public ICommand AddYearOfStudyCommand { get { return new DelegateCommand(OnAddYearOfStudy); } }
        public ICommand AddSpecializationCommand { get { return new DelegateCommand(OnAddSpecialization); } }
        public ICommand AddGroupCommand { get { return new DelegateCommand(OnAddGroup); } }
        public ICommand AddLecturerCommand { get { return new DelegateCommand(OnAddLecturer); } }
        public ICommand AddSubjectCommand { get { return new DelegateCommand(OnAddSubject); } }

        public ICommand RemoveClassCommand { get { return new DelegateCommand(OnRemoveClassRecord); } }
        public ICommand RemoveYearOfStudyCommand { get { return new DelegateCommand(OnRemoveYearOfStudy, CanExecuteRemoveYearOfStudy); } }
        public ICommand RemoveSpecializationCommand { get { return new DelegateCommand(OnRemoveSpecialization, CanExecuteRemoveSpecialization); } }
        public ICommand RemoveGroupCommand { get { return new DelegateCommand(OnRemoveGroup); } }
        public ICommand RemoveLecturerCommand { get { return new DelegateCommand(OnRemoveLecturer); } }
        public ICommand RemoveSubjectCommand { get { return new DelegateCommand(OnRemoveSubject); } }

        #endregion

        #region Command handlers

        public void OnAddClassRecord(object param)
        {
            if (AddClassRecordDelegate != null)
            {
                AddClassRecordDelegate(param as ClassRecord);
            }
        }

        public void OnAddYearOfStudy()
        {
            if (AddYearOfStudyDelegate != null)
            {
                AddYearOfStudyDelegate();
            }
        }

        public void OnAddSpecialization(object yearOfStudy)
        {
            if (AddSpecializationDelegate != null)
            {
                AddSpecializationDelegate(yearOfStudy as YearOfStudy);
            }
        }

        public void OnAddGroup(object param)
        {
            if (AddGroupDelegate != null)
            {
                AddGroupDelegate(param as Specialization);
            }
        }

        public void OnAddLecturer(object param)
        {
            if (AddLecturerDelegate != null)
            {
                AddLecturerDelegate(param as Lecturer);
            }
        }

        public void OnAddSubject(object param)
        {
            if (AddSubjectDelegate != null)
            {
                AddSubjectDelegate(param as Subject);
            }
        }

        public void OnRemoveClassRecord(object param)
        {
            if (RemoveClassRecordDelegate != null)
            {
                RemoveClassRecordDelegate(param as ClassRecord);
            }
        }

        public void OnRemoveYearOfStudy(object param)
        {
            if (RemoveYearOfStudyDelegate != null)
            {
                RemoveYearOfStudyDelegate(param as YearOfStudy);
            }
        }

        public void OnRemoveSpecialization(object param)
        {
            if (RemoveSpecializationDelegate != null)
            {
                RemoveSpecializationDelegate(param as Specialization);
            }
        }

        public void OnRemoveGroup(object param)
        {
            if (RemoveGroupDelegate != null)
            {
                RemoveGroupDelegate(param as Group);
            }
        }

        public void OnRemoveLecturer(object param)
        {
            if (RemoveLecturerDelegate != null)
            {
                RemoveLecturerDelegate(param as Lecturer);
            }
        }

        public void OnRemoveSubject(object param)
        {
            if (RemoveSubjectDelegate != null)
            {
                RemoveSubjectDelegate(param as Subject);
            }
        }

        #endregion

        #region Command can execute

        private bool CanExecuteRemoveYearOfStudy()
        {
            return Project.Schedule.YearsOfStudy.Count > 1;
        }

        private bool CanExecuteRemoveSpecialization()
        {
            return Project.Schedule.YearsOfStudy.Count == 1 &&
                   Project.Schedule.Specializations.Count > 1 ||
                   Project.Schedule.YearsOfStudy.Count > 1;
        }

        #endregion

    }
}
