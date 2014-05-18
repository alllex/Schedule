using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Editor.Helpers;

namespace Editor.Models
{

    [Serializable]
    public class ClassRecord : HavingName
    {
        protected bool Equals(ClassRecord other)
        {
            return Equals(_subject, other._subject) && Equals(_lecturer, other._lecturer) && Equals(_classroom, other._classroom);
        }

        #region Subject

        private Subject _subject;

        public Subject Subject
        {
            get { return _subject; }
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    RaisePropertyChanged(() => Subject);
                }
            }
        }

        #endregion

        #region Lecturer

        private Lecturer _lecturer;

        public Lecturer Lecturer
        {
            get { return _lecturer; }
            set
            {
                if (_lecturer != value)
                {
                    _lecturer = value;
                    RaisePropertyChanged(() => Lecturer);
                }
            }
        }

        #endregion

        #region Classroom

        private Classroom _classroom;

        public Classroom Classroom
        {
            get { return _classroom; }
            set
            {
                if (_classroom != value)
                {
                    _classroom = value;
                    RaisePropertyChanged(() => Classroom);
                }
            }
        }

        #endregion

        public static void Copy(ClassRecord from, ClassRecord to)
        {
            if (from == null || to == null) return;
            to.Subject = from.Subject;
            to.Lecturer = from.Lecturer;
            to.Classroom = from.Classroom;
        }
    }

    [Serializable]
    public class ClassesSchedule : HavingId
    {

        public static string[] ClassIntervals = {"09:00-10:00", "10:00-11:00", "11:00-12:00"};

        #region TimeLine

        private ObservableCollectionEx<ClassTime> _timeLine = new ObservableCollectionEx<ClassTime>();

        public ObservableCollectionEx<ClassTime> TimeLine
        {
            get { return _timeLine; }
            set
            {
                if (_timeLine != value)
                {
                    _timeLine = value;
                    RaisePropertyChanged(() => TimeLine);
                }
            }
        }

        #endregion

        #region Groups

        private ObservableCollectionEx<Group> _groups = new ObservableCollectionEx<Group>();

        public ObservableCollectionEx<Group> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups != value)
                {
                    _groups = value;
                    RaisePropertyChanged(() => Groups);
                }
            }
        }

        #endregion

        #region Lecturers

        private ObservableCollectionEx<Lecturer> _lecturers = new ObservableCollectionEx<Lecturer>();

        public ObservableCollectionEx<Lecturer> Lecturers
        {
            get { return _lecturers; }
            set
            {
                if (_lecturers != value)
                {
                    _lecturers = value;
                    RaisePropertyChanged(() => Lecturers);
                }
            }
        }

        #endregion

        #region Classrooms

        private ObservableCollectionEx<Classroom> _classrooms = new ObservableCollectionEx<Classroom>();
        public ObservableCollectionEx<Classroom> Classrooms
        {
            get { return _classrooms; }
            set
            {
                if (_classrooms != value)
                {
                    _classrooms = value;
                    RaisePropertyChanged(() => Classrooms);
                }
            }
        }

        #endregion

        #region Subjects

        private ObservableCollectionEx<Subject> _subjects = new ObservableCollectionEx<Subject>();

        public ObservableCollectionEx<Subject> Subjects
        {
            get { return _subjects; }
            set
            {
                if (_subjects != value)
                {
                    _subjects = value;
                    RaisePropertyChanged(() => Subjects);
                }
            }
        }

        #endregion

        #region Specializations

        private ObservableCollectionEx<Specialization> _specializations = new ObservableCollectionEx<Specialization>();

        public ObservableCollectionEx<Specialization> Specializations
        {
            get { return _specializations; }
            set
            {
                if (_specializations != value)
                {
                    _specializations = value;
                    RaisePropertyChanged(() => Specializations);
                }
            }
        }

        #endregion

        #region YearsOfStudy

        private ObservableCollectionEx<YearOfStudy> _yearsOfStudy = new ObservableCollectionEx<YearOfStudy>();

        public ObservableCollectionEx<YearOfStudy> YearsOfStudy
        {
            get { return _yearsOfStudy; }
            set
            {
                if (_yearsOfStudy != value)
                {
                    _yearsOfStudy = value;
                    RaisePropertyChanged(() => YearsOfStudy);
                }
            }
        }

        #endregion

        #region Tables

        private ObservableCollection<ClassesTable> _tables = new ObservableCollection<ClassesTable>();

        public ObservableCollection<ClassesTable> Tables
        {
            get { return _tables; }
            set
            {
                if (_tables != value)
                {
                    _tables = value;
                    RaisePropertyChanged(() => Tables);
                }
            }
        }

        #endregion
        
        public ClassesSchedule()
        {
//            TimeLine.CollectionChanged += OnSomeCollectionChanged;
//            Groups.CollectionChanged += OnSomeCollectionChanged;
//            Lecturers.CollectionChanged += OnSomeCollectionChanged;
//            Classrooms.CollectionChanged += OnSomeCollectionChanged;
//            Subjects.CollectionChanged += OnSomeCollectionChanged;
//            Specializations.CollectionChanged += OnSomeCollectionChanged;
//            YearsOfStudy.CollectionChanged += OnSomeCollectionChanged;
        }

        public void UpdateTables()
        {
            Tables.Clear();
            foreach (var yearOfStudy in YearsOfStudy)
            {
                Tables.Add(new ClassesTable(this, yearOfStudy));
            }
        }

        private void OnSomeCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ItemChangedProperty != null)
            {
                ItemChangedProperty();
            }
        }

        public delegate void ItemChanged();
        public ItemChanged ItemChangedProperty { get; set; }
    }
}