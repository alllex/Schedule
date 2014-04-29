using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleData
{
    public enum WeekType
    {
        Odd,
        Even,
        Both
    }

    public enum Weekdays
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday //, Sunday
    }

    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public override string ToString()
        {
            string h = Hours.ToString(CultureInfo.InvariantCulture);
            if (Hours < 10) h = "0" + h;
            string m = Minutes.ToString(CultureInfo.InvariantCulture);
            if (Minutes < 10) m = "0" + m;
            return String.Format("{0}:{1}", h, m);
        }
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
    }

    public interface ILecturer : IHavingName
    {
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
