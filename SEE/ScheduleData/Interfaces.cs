using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum WeekType
    {
        Odd, Even, Both
    }

    public enum Weekdays
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday //, Sunday
    }

    public class Time : ICloneable
    {
        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }
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
        public object Clone()
        {
            return new Time(Hours, Minutes);
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

    public interface ICourse : IHavingName
    {
    }

    public interface IDirection : IHavingName
    {
    }

    public interface IGroup : IHavingName
    {
        ICourse Course { get; set; }
        IDirection Direction { get; set; }
    }

    public interface ILecture
    {
        ISubject Subject { get; set; }
        IGroup Group { get; set; }
        ILecturer Lecturer { get; set; }
        IRoom Room { get; set; }
        ILectureTime Time { get; set; }
    }

    public interface IObjectCollection<Object>
    {
        Object Add(Object subject); // add and return added value
        bool Remove(Object subject);
        bool Submit(Object subject);
        IEnumerable<Object> GetAll();
    }

    public interface IRoomCollection : IObjectCollection<IRoom>
    {
    }

    public interface ISubjectCollection : IObjectCollection<ISubject>
    {
    }

    public interface ILecturerCollection : IObjectCollection<ILecturer>
    {
    }

    public interface ICourseCollection : IObjectCollection<ICourse>
    {
    }

    public interface IDirectionCollection : IObjectCollection<IDirection>
    {
    }

    public interface IGroupCollection : IObjectCollection<IGroup>
    {
    }

    public interface ILectureTimeCollection : IObjectCollection<ILectureTime>
    {
    }

    public interface ILectureCollection : IObjectCollection<ILecture>
    {
        ILecture Get(IGroup group, ILectureTime lectureTime);
        ILecture Get(ILecturer lecturer, ILectureTime lectureTime);
        ILecture Get(IRoom room, ILectureTime lectureTime);
    }

    public interface ISchedule
    {
        ILectureTimeCollection TimeLine { get; }
        IRoomCollection Rooms { get; }
        ISubjectCollection Subjects { get; }
        ILecturerCollection Lecturers { get; }
        ICourseCollection Courses { get; }
        IDirectionCollection Directions { get; }
        IGroupCollection Groups { get; }
        ILectureCollection Lectures { get; }
    }
}
