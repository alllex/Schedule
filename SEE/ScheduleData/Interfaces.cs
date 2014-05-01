using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleData
{

    public interface IHavingId
    {
        int Id { get; set; }
    }

    public interface ITimeInterval
    {
        Weekdays Day { get; set; }
        Time Begin { get; set; }
        Time End { get; set; }
    }

    public interface ILectureTime : ITimeInterval
    {
        WeekType Week { get; set; }
    }

    public interface IHavingName
    {
        string Name { get; set; }
    }

    public interface ISubject : IHavingName
    {

    }

    public interface IRoom : IHavingName
    {
        string Address { get; set; }
    }

    public interface ILecturer : IHavingName
    {
        string Degree { get; set; }
        string Department { get; set; }
    }

    public interface IGroup : IHavingName
    {
        IGroup Overgroup { get; set; } 
        IList<IGroup> Subgroups { get; set; }
    }

    public interface ILecture
    {
        ISubject Subject { get; set; }
        IGroup Group { get; set; }
        ILecturer Lecturer { get; set; }
        IRoom Room { get; set; }
        ILectureTime Time { get; set; }
    }

    public interface IObjectCollection<T> 
    {
        T Add(T t);         // add and return added value
        bool Remove(T t);      
        bool Submit(T t);
        IEnumerable<T> GetAll();
    }

    public interface ILectures : IObjectCollection<ILecture>
    {
        ILecture Get(IGroup group, ILectureTime lectureTime);
        ILecture Get(ILecturer lecturer, ILectureTime lectureTime);
        ILecture Get(IRoom room, ILectureTime lectureTime);
    }

    public interface ISchedule
    {
        IObjectCollection<ILectureTime> TimeLine { get; }
        IObjectCollection<IGroup> Groups { get; }
        IObjectCollection<ILecturer> Lecturers { get; }
        IObjectCollection<IRoom> Rooms { get; } 
        ILectures Lectures { get; }
    }
}
