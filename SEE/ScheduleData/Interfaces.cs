using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ScheduleData;

namespace Editor.Models
{

    public interface ITime
    {
        int Hours { get; set; }
        int Minutes { get; set; }
    }

    public interface ITimeInterval
    {
        Weekdays Day { get; set; }
        ITime BeginTime { get; set; }
        ITime EndTime { get; set; }
    }

    public interface IClassTime : ITimeInterval
    {
        WeekType Week { get; set; }
    }

    public interface IHavingName : IComparable
    {
        string Name { get; set; }
    }

    public interface ISubject : IHavingName
    {
        ClassType ClassType { get; set; }
    }

    public interface IClassroom : IHavingName
    {
        string Address { get; set; }
    }

    public interface ILecturer : IHavingName
    {
        string Degree { get; set; }
        IDepartment Department { get; set; }
    }

    public interface IDepartment : IHavingName
    {
    }

    public interface IYearOfStudy : IHavingName
    {
    }

    public interface ISpecialization : IHavingName
    {
    }

    public interface IGroup : IHavingName
    {
        IYearOfStudy YearOfStudy { get; set; }
        ISpecialization Specialization { get; set; }
    }

    public interface IClass
    {
        ISubject Subject { get; set; }
        IGroup Group { get; set; }
        ILecturer Lecturer { get; set; }
        IClassroom Classroom { get; set; }
        IClassTime ClassTime { get; set; }
    }

    public interface IClassCollection : ICollection<IClass>
    {
        IClass Get(IGroup group, IClassTime classTime);
        IClass Get(ILecturer lecturer, IClassTime classTime);
        IClass Get(IClassroom classroom, IClassTime classTime);
    }

    public interface ISchedule
    {
        Collection<IClassTime> TimeLine { get; }
        Collection<IClassroom> Classrooms { get; }
        Collection<ISubject> Subjects { get; }
        Collection<ILecturer> Lecturers { get; }
        Collection<IYearOfStudy> YearsOfStudy { get; }
        Collection<ISpecialization> Specializations { get; }
        Collection<IGroup> Groups { get; }
        Collection<IClass> Classes { get; }
    }
}
