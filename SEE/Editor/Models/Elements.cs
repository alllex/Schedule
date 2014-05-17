using System;
using System.Collections.Specialized;
using System.Globalization;
using Editor.Helpers;

namespace Editor.Models
{

    public enum WeekType
    {
        Odd, Even, Both
    }

    public enum Weekdays
    {
        Monday, Tuesday, Wednesday, //Thursday, Friday, Saturday //, Sunday
    }

    public enum ClassType
    {
        Lecture, Practice
    }

    [Serializable]
    public class HavingId : NotificationObject
    {
        private static int _count = 0;
        private readonly int _id = _count++;

        public override int GetHashCode()
        {
            return _id;
        }

    }

    [Serializable]
    public abstract class HavingName : HavingId, IComparable
    {
        protected bool Equals(HavingName other)
        {
            return string.Equals(_name, other._name);
        }

        

        public int CompareTo(object obj)
        {
            var hn = obj as HavingName;
            if (hn == null) return -1;
            return String.Compare(_name, hn.Name, StringComparison.Ordinal);
        }

        #region Name

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }

    [Serializable]
    public class Time : HavingId
    {
        protected bool Equals(Time other)
        {
            return _hours == other._hours && _minutes == other._minutes;
        }
        
        #region Hours

        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    RaisePropertyChanged(() => Hours);
                }
            }
        }

        #endregion

        #region Minutes

        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    RaisePropertyChanged(() => Minutes);
                }
            }
        }

        #endregion
        
        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public override string ToString()
        {
            string h = Hours.ToString(CultureInfo.InvariantCulture);
            if (Hours < 10) h = "0" + h;
            string m = Minutes.ToString(CultureInfo.InvariantCulture);
            if (Minutes < 10) m = "0" + m;
            return String.Format("{0}:{1}", h, m);
        }
        
    }

    [Serializable]
    public class TimeInterval : HavingId
    {
        protected bool Equals(TimeInterval other)
        {
            return _day == other._day && Equals(_beginTime, other._beginTime) && Equals(_endTime, other._endTime);
        }
        
        #region Day

        private Weekdays _day;
        public Weekdays Day
        {
            get { return _day; }
            set
            {
                if (_day != value)
                {
                    _day = value;
                    RaisePropertyChanged(() => Day);
                }
            }
        }

        #endregion

        #region BeginTime

        private Time _beginTime;
        public Time BeginTime
        {
            get { return _beginTime; }
            set
            {
                if (_beginTime != value)
                {
                    _beginTime = value;
                    RaisePropertyChanged(() => BeginTime);
                }
            }
        }

        #endregion

        #region EndTime

        private Time _endTime;

        public Time EndTime
        {
            get { return _endTime; }
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    RaisePropertyChanged(() => EndTime);
                }
            }
        }

        #endregion

    }

    [Serializable]
    public class ClassTime : TimeInterval
    {
        protected bool Equals(ClassTime other)
        {
            return _week == other._week;
        }

        #region Week

        private WeekType _week;
        public WeekType Week
        {
            get { return _week; }
            set
            {
                if (_week != value)
                {
                    _week = value;
                    RaisePropertyChanged(() => Week);
                }
            }
        }

        #endregion

        public ClassTime()
        {
            
        }

        public ClassTime(TimeInterval ti)
        {
            Day = ti.Day;
            BeginTime = ti.BeginTime;
            EndTime = ti.EndTime;
            Week = WeekType.Both;
        }
    }

    [Serializable]
    public class Subject : HavingName
    {
        protected bool Equals(Subject other)
        {
            return _classType == other._classType;
        }
        
        #region ClassType

        private ClassType _classType;
        public ClassType ClassType
        {
            get { return _classType; }
            set
            {
                if (_classType != value)
                {
                    _classType = value;
                    RaisePropertyChanged(() => ClassType);
                }
            }
        }

        #endregion

    }

    [Serializable]
    public class Specialization : HavingName
    {
    }

    [Serializable]
    public class YearOfStudy : HavingName
    {

        public override string ToString()
        {
            return "Курс " + Name;
        }
    }

    [Serializable]
    public class Department : HavingName
    {

    }

    [Serializable]
    public class Group : HavingName
    {
        protected bool Equals(Group other)
        {
            return Equals(_specialization, other._specialization) && Equals(_yearOfStudy, other._yearOfStudy);
        }
        
        #region Specialization

        private Specialization _specialization;
        public Specialization Specialization
        {
            get { return _specialization; }
            set
            {
                if (_specialization != value)
                {
                    _specialization = value;
                    RaisePropertyChanged(() => Specialization);
                }
            }
        }

        #endregion

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

        public override string ToString()
        {
            return String.Format("Группа {0}; \nСпециальность: {1}; " +
                                 "\nКурс: {2}", Name, Specialization, YearOfStudy);
        }
    }

    [Serializable]
    public class Lecturer : HavingName
    {
        #region Degree

        private string _degree;

        public string Degree
        {
            get { return _degree; }
            set
            {
                if (_degree != value)
                {
                    _degree = value;
                    RaisePropertyChanged(() => Degree);
                }
            }
        }

        #endregion

        #region Department

        private Department _department;

        public Department Department
        {
            get { return _department; }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    RaisePropertyChanged(() => Department);
                }
            }
        }

        #endregion

    }

    [Serializable]
    public class Classroom : HavingName
    {
        protected bool Equals(Classroom other)
        {
            return string.Equals(_address, other._address);
        }
        
        #region Address

        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    RaisePropertyChanged(() => Address);
                }
            }
        }

        #endregion

    }

    [Serializable]
    public class Class : HavingName
    {
        protected bool Equals(Class other)
        {
            return Equals(_subject, other._subject) && Equals(_group, other._group) && Equals(_lecturer, other._lecturer) && Equals(_classroom, other._classroom) && Equals(_classTime, other._classTime);
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

        #region Group

        private Group _group;

        public Group Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    RaisePropertyChanged(() => Group);
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

        #region ClassTime

        private ClassTime _classTime;

        public ClassTime ClassTime
        {
            get { return _classTime; }
            set
            {
                if (_classTime != value)
                {
                    _classTime = value;
                    RaisePropertyChanged(() => ClassTime);
                }
            }
        }

        #endregion

        public static void CopyWithoutGroup(Class from, Class to)
        {
            if (from == null || to == null) return;
            to.Subject = from.Subject;
            to.Lecturer = from.Lecturer;
            to.Classroom = from.Classroom;
            to.ClassTime = from.ClassTime;
        }
    }

    [Serializable]
    public class ClassesSchedule : HavingId
    {

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

        #region Classes

        private ObservableCollectionEx<Class> _classes = new ObservableCollectionEx<Class>();
        public ObservableCollectionEx<Class> Classes
        {
            get { return _classes; }
            set
            {
                if (_classes != value)
                {
                    _classes = value;
                    RaisePropertyChanged(() => Classes);
                }
            }
        }

        #endregion

        public ClassesSchedule()
        {
            TimeLine.CollectionChanged += OnSomeCollectionChanged;
            Groups.CollectionChanged += OnSomeCollectionChanged;
            Lecturers.CollectionChanged += OnSomeCollectionChanged;
            Classrooms.CollectionChanged += OnSomeCollectionChanged;
            Subjects.CollectionChanged += OnSomeCollectionChanged;
            Specializations.CollectionChanged += OnSomeCollectionChanged;
            YearsOfStudy.CollectionChanged += OnSomeCollectionChanged;
            //Classes.CollectionChanged += OnSomeCollectionChanged;
        }

        private void OnSomeCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
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
