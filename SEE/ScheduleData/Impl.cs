using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleData
{
    public class TimeInterval: ITimeInterval
    {
        public Weekdays Day { get; set; }
        public Time Begin { get; set; }
        public Time End { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-\n{1}", Begin, End);
        }
    }

    public class LectureTime : ILectureTime
    {
        public WeekType Week { get; set; }
        public Weekdays Day { get; set; }
        public Time Begin { get; set; }
        public Time End { get; set; }
    }

    public class Subject: ISubject
    {
        public string Name { get; set; }
    }

    public class Room : IRoom
    {
        public string Name { get; set; }
    }

    public class Lecturer : ILecturer
    {
        public string Name { get; set; }
    }

    public class Group : IGroup
    {
        public string Name { get; set; }
        public IGroup Overgroup { get; set; }
        public IList<IGroup> Subgroups { get; set; }

        public Group(string name)
        {
            Name = name;
            Subgroups = new List<IGroup>();
        }

        public Group(string name, IEnumerable<IGroup> subgroups)
        {
            Name = name;
            Subgroups = new List<IGroup>(subgroups);
        }
    }

    public class Lecture : ILecture
    {
        public ISubject Subject { get; set; }
        public IGroup Group { get; set; }
        public ILecturer Lecturer { get; set; }
        public IRoom Room { get; set; }
        public ILectureTime Time { get; set; }
    }

    public class GroupCollection : IObjectCollection<IGroup>
    {
        public IGroup Add(IGroup t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IGroup t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(IGroup t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGroup> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public class LecturersCollection : IObjectCollection<ILecturer>
    {
        public ILecturer Add(ILecturer t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ILecturer t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(ILecturer t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILecturer> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public class RoomsCollection : IObjectCollection<IRoom>
    {
        public IRoom Add(IRoom t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IRoom t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(IRoom t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRoom> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public class LecturesTable : ILectures
    {
        public ILecture Add(ILecture t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ILecture t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(ILecture t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILecture> GetAll()
        {
            throw new NotImplementedException();
        }

        public ILecture Get(IGroup @group, ILectureTime lectureTime)
        {
            throw new NotImplementedException();
        }

        public ILecture Get(ILecturer lecturer, ILectureTime lectureTime)
        {
            throw new NotImplementedException();
        }

        public ILecture Get(IRoom room, ILectureTime lectureTime)
        {
            throw new NotImplementedException();
        }
    }

    public class Schedule : ISchedule
    {
        public IObjectCollection<ILectureTime> TimeLine { get; private set; }
        public IObjectCollection<IGroup> Groups { get; private set; }
        public IObjectCollection<ILecturer> Lecturers { get; private set; }
        public IObjectCollection<IRoom> Rooms { get; private set; }
        public ILectures Lectures { get; private set; }
    }
}
