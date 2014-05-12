using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday //, Sunday
    }

    public enum ClassType
    {
        Lecture, Practice
    }

    public abstract class HavingName : NotificationObject, IComparable
    {
        protected bool Equals(HavingName other)
        {
            return string.Equals(_name, other._name);
        }

        public override int GetHashCode()
        {
            return (_name != null ? _name.GetHashCode() : 0);
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

    }

    public class Time : NotificationObject
    {
        protected bool Equals(Time other)
        {
            return _hours == other._hours && _minutes == other._minutes;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_hours*397) ^ _minutes;
            }
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

    public class TimeInterval : NotificationObject
    {
        protected bool Equals(TimeInterval other)
        {
            return _day == other._day && Equals(_beginTime, other._beginTime) && Equals(_endTime, other._endTime);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int) _day;
                hashCode = (hashCode*397) ^ (_beginTime != null ? _beginTime.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_endTime != null ? _endTime.GetHashCode() : 0);
                return hashCode;
            }
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

    public class ClassTime : TimeInterval
    {
        protected bool Equals(ClassTime other)
        {
            return _week == other._week;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (int) _week;
            }
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
    }

    public class Subject : HavingName
    {
        protected bool Equals(Subject other)
        {
            return _classType == other._classType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (int) _classType;
            }
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

    public class Specialization : HavingName
    {
    }

    public class YearOfStudy : HavingName
    {

        public override string ToString()
        {
            return "Курс " + Name;
        }
    }

    public class Department : HavingName
    {

    }

    public class Group : HavingName
    {
        protected bool Equals(Group other)
        {
            return Equals(_specialization, other._specialization) && Equals(_yearOfStudy, other._yearOfStudy);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (_specialization != null ? _specialization.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_yearOfStudy != null ? _yearOfStudy.GetHashCode() : 0);
                return hashCode;
            }
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

    }

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

    public class Classroom : HavingName
    {
        protected bool Equals(Classroom other)
        {
            return string.Equals(_address, other._address);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (_address != null ? _address.GetHashCode() : 0);
            }
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

    public class Class : HavingName
    {
        protected bool Equals(Class other)
        {
            return Equals(_subject, other._subject) && Equals(_group, other._group) && Equals(_lecturer, other._lecturer) && Equals(_classroom, other._classroom) && Equals(_classTime, other._classTime);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (_subject != null ? _subject.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_group != null ? _group.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_lecturer != null ? _lecturer.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_classroom != null ? _classroom.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_classTime != null ? _classTime.GetHashCode() : 0);
                return hashCode;
            }
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
    }

    public class ClassesSchedule : NotificationObject
    {

        #region TimeLine

        private ObservableCollection<ClassTime> _timeLine = new ObservableCollection<ClassTime>();

        public ObservableCollection<ClassTime> TimeLine
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

        private ObservableCollection<Group> _groups = new ObservableCollection<Group>();

        public ObservableCollection<Group> Groups
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

        private ObservableCollection<Lecturer> _lecturers = new ObservableCollection<Lecturer>();

        public ObservableCollection<Lecturer> Lecturers
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

        private ObservableCollection<Classroom> _classrooms = new ObservableCollection<Classroom>();

        public ObservableCollection<Classroom> Classrooms
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

        private ObservableCollection<Subject> _subjects = new ObservableCollection<Subject>();

        public ObservableCollection<Subject> Subjects
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

        private ObservableCollection<Specialization> _specializations = new ObservableCollection<Specialization>();

        public ObservableCollection<Specialization> Specializations
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

        private ObservableCollection<YearOfStudy> _yearsOfStudy = new ObservableCollection<YearOfStudy>();

        public ObservableCollection<YearOfStudy> YearsOfStudy
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

        private ObservableCollection<Class> _classes = new ObservableCollection<Class>();

        public ObservableCollection<Class> Classes
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
        
    }

}
