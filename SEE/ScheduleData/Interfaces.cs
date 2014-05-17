using System;
using System.Collections.Generic;
using System.Globalization;

namespace ScheduleData.Interfaces
{

    //public enum WeekType
    //{
    //    Odd, Even, Both
    //}

    //public enum Weekdays
    //{
    //    Monday, Tuesday, Wednesday, Thursday, Friday, Saturday //, Sunday
    //}

    //public enum ClassType
    //{
    //    Lecture, Practice
    //}

    //public interface ITime
    //{
    //    int Hours { get; set; }
    //    int Minutes { get; set; }
    //}

    //public class Time : ITime, ICloneable
    //{
    //    protected bool Equals(Time other)
    //    {
    //        return Hours == other.Hours && Minutes == other.Minutes;
    //    }

    //    public override int GetHashCode()
    //    {
    //        unchecked
    //        {
    //            return (Hours*397) ^ Minutes;
    //        }
    //    }

    //    public Time(int hours, int minutes)
    //    {
    //        Hours = hours;
    //        Minutes = minutes;
    //    }

    //    public int Hours { get; set; }
    //    public int Minutes { get; set; }

    //    public int CompareTo(object t)
    //    {
    //        var other = (Time) t;
    //        if (other == null) return 1;
    //        if (Hours == other.Hours)
    //        {
    //            return Minutes.CompareTo(other.Minutes);
    //        }
    //        return Hours.CompareTo(other.Hours);
    //    }

    //    public override string ToString()
    //    {
    //        string h = Hours.ToString(CultureInfo.InvariantCulture);
    //        if (Hours < 10) h = "0" + h;
    //        string m = Minutes.ToString(CultureInfo.InvariantCulture);
    //        if (Minutes < 10) m = "0" + m;
    //        return String.Format("{0}:{1}", h, m);
    //    }

    //    public object Clone()
    //    {
    //        return new Time(Hours, Minutes);
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (ReferenceEquals(null, obj)) return false;
    //        if (ReferenceEquals(this, obj)) return true;
    //        if (obj.GetType() != this.GetType()) return false;
    //        return Equals((Time) obj);
    //    }
    //}

    //public interface ITimeInterval 
    //{
    //    Weekdays Day { get; set; }
    //    Time Begin { get; set; }
    //    Time End { get; set; }
    //}

    //public interface IClassTime : ITimeInterval
    //{
    //    WeekType Week { get; set; }
    //}

    //public interface IHavingName
    //{
    //    string Name { get; set; }
    //}

    //public interface ISubject : IHavingName
    //{
    //    ClassType ClassType { get; set; }
    //}

    //public interface IClassroom : IHavingName
    //{
    //    string Address { get; set; }
    //}

    //public interface ILecturer : IHavingName
    //{
    //    string Degree { get; set; }
    //    IDepartment Department { get; set; }
    //}

    //public interface IDepartment : IHavingName
    //{
    //}

    //public interface IYearOfStudy : IHavingName
    //{
    //}

    //public interface ISpecialization : IHavingName
    //{
    //}

    //public interface IGroup : IHavingName
    //{
    //    IYearOfStudy YearOfStudy { get; set; }
    //    ISpecialization Specialization { get; set; }
    //}

    //public interface IClass
    //{
    //    ISubject Subject { get; set; }
    //    IGroup Group { get; set; }
    //    ILecturer Lecturer { get; set; }
    //    IClassroom Classroom { get; set; }
    //    IClassTime Time { get; set; }
    //}

    //public interface IObjectCollection<T>
    //{
    //    T Add(T lecturer); // add and return added value
    //    bool Remove(T subject);
    //    bool Submit(T subject);
    //    IEnumerable<T> GetAll();
    //}

    //public interface IClassroomCollection : IObjectCollection<IClassroom>
    //{
    //}

    //public interface ISubjectCollection : IObjectCollection<ISubject>
    //{
    //}

    //public interface ILecturerCollection : IObjectCollection<ILecturer>
    //{
    //}

    //public interface IYearOfStudyCollection : IObjectCollection<IYearOfStudy>
    //{
    //}

    //public interface ISpecializationCollection : IObjectCollection<ISpecialization>
    //{
    //}

    //public interface IGroupCollection : IObjectCollection<IGroup>
    //{
    //}

    //public interface IClassTimeCollection : IObjectCollection<IClassTime>
    //{
    //}

    //public interface IClassCollection : IObjectCollection<IClass>
    //{
    //    IClass Get(IGroup group, IClassTime classTime);
    //    IClass Get(ILecturer lecturer, IClassTime classTime);
    //    IClass Get(IClassroom classroom, IClassTime classTime);
    //}

    //public interface ISchedule
    //{
    //    IClassTimeCollection TimeLine { get; }
    //    IClassroomCollection Classrooms { get; }
    //    ISubjectCollection Subjects { get; }
    //    ILecturerCollection Lecturers { get; }
    //    IYearOfStudyCollection YearsOfStudy { get; }
    //    ISpecializationCollection Specializations { get; }
    //    IGroupCollection Groups { get; }
    //    IClassCollection Classes { get; }
    //}
}
