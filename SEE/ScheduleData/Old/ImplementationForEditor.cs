﻿

using System;
using System.Runtime.Serialization.Formatters;
using ScheduleData.Interfaces;

namespace ScheduleData.Editor
{

    //public class TimeInterval : ITimeInterval
    //{
    //    public TimeInterval(ITimeInterval t)
    //    {
    //        Day = t.Day;
    //        Begin = t.Begin;
    //        End = t.End;
    //    }
    //    public Weekdays Day { get; set; }
    //    public ITime Begin { get; set; }
    //    public ITime End { get; set; }
    //}

    //public class ClassTime : IClassTime
    //{

    //    public ClassTime(WeekType week, Weekdays day, Time begin, Time end)
    //    {
    //        Week = week;
    //        Day = day;
    //        Begin = begin;
    //        End = end;
    //    }
    //    public WeekType Week { get; set; }
    //    public Weekdays Day { get; set; }
    //    public ITime Begin { get; set; }
    //    public ITime End { get; set; }

    //    public override string ToString()
    //    {
    //        return String.Format("{0}({1}-{2}) {3}", Day, Begin, End, Week);
    //    }

    //}

    //public class Subject : ISubject
    //{
    //    public Subject(string name)
    //    {
    //        Name = name;
    //    }
    //    public string Name { get; set; }
    //    public ClassType ClassType { get; set; }
    //    public int CompareTo(object obj)
    //    {
    //        var t = (IHavingName) obj;
    //        return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
    //    }
    //}

    //public class Classroom : IClassroom
    //{
    //    public Classroom(string name)
    //    {
    //        Name = name;
    //        Address = "";
    //    }

    //    public string Name { get; set; }
    //    public string Address { get; set; }
    //    public int CompareTo(object obj)
    //    {
    //        var t = (IHavingName)obj;
    //        return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
    //    }
    //}

    //public class Lecturer : ILecturer
    //{
    //    public Lecturer(string name)
    //    {
    //        Name = name;
    //    }
    //    public string Name { get; set; }
    //    public string Degree { get; set; }
    //    public IDepartment Department { get; set; }
    //    public int CompareTo(object obj)
    //    {
    //        var t = (IHavingName)obj;
    //        return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
    //    }
    //}

    //public class YearOfStudy : IYearOfStudy
    //{
    //    public YearOfStudy(string name)
    //    {
    //        Name = name;
    //    }
    //    public string Name { get; set; }
    //    public int CompareTo(object obj)
    //    {
    //        var t = (IHavingName)obj;
    //        return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
    //    }
    //}

    //public class Specialization : ISpecialization
    //{
    //    public Specialization(string name)
    //    {
    //        Name = name;
    //    }
    //    public string Name { get; set; }
    //    public int CompareTo(object obj)
    //    {
    //        var t = (IHavingName)obj;
    //        return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
    //    }
    //}

    //public class Group : IGroup
    //{
    //    public Group(string name, IYearOfStudy yearOfStudy, ISpecialization specialization)
    //    {
    //        Name = name;
    //        YearOfStudy = yearOfStudy;
    //        Specialization = specialization;
    //    }

    //    public string Name { get; set; }
    //    public IYearOfStudy YearOfStudy { get; set; }
    //    public ISpecialization Specialization { get; set; }
    //    public int CompareTo(object obj)
    //    {
    //        var t = (IHavingName)obj;
    //        return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
    //    }
    //}

    //public class Class : IClass
    //{
    //    public Class(ISubject subject, IGroup group, ILecturer lecturer, IClassroom classroom, IClassTime time)
    //    {
    //        Subject = subject;
    //        Group = group;
    //        Lecturer = lecturer;
    //        Classroom = classroom;
    //        Time = time;
    //    }

    //    public ISubject Subject { get; set; }
    //    public IGroup Group { get; set; }
    //    public ILecturer Lecturer { get; set; }
    //    public IClassroom Classroom { get; set; }
    //    public IClassTime Time { get; set; }
    //}

    //public class Schedule : ISchedule
    //{
    //    readonly IClassTimeCollection _timeLine = new ClassTimeCollection();
    //    readonly IClassroomCollection _classrooms = new ClassroomCollection();
    //    readonly ISubjectCollection _subjects = new SubjectCollection();
    //    readonly ILecturerCollection _lecturers = new LecturerCollection();
    //    readonly IYearOfStudyCollection _yearsOfStudy = new YearOfStudyCollection();
    //    readonly ISpecializationCollection _specializations = new SpecializationCollection();
    //    readonly IGroupCollection _groups = new GroupCollection();
    //    readonly IClassCollection _classes = new ClassCollection();

    //    public IClassTimeCollection TimeLine
    //    {
    //        get { return _timeLine; }
    //    }
    //    public IClassroomCollection Classrooms
    //    {
    //        get { return _classrooms; }
    //    }
    //    public ISubjectCollection Subjects
    //    {
    //        get { return _subjects; }
    //    }
    //    public ILecturerCollection Lecturers
    //    {
    //        get { return _lecturers; }
    //    }
    //    public IYearOfStudyCollection YearsOfStudy
    //    {
    //        get { return _yearsOfStudy; }
    //    }
    //    public ISpecializationCollection Specializations
    //    {
    //        get { return _specializations; }
    //    }
    //    public IGroupCollection Groups
    //    {
    //        get { return _groups; }
    //    }
    //    public IClassCollection Classes
    //    {
    //        get { return _classes; }
    //    }
    //}

}
