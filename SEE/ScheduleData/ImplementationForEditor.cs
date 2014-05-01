using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementationData;

namespace Data
{
    public class LectureTime : ILectureTime
    {
        public LectureTime(WeekType week, Weekdays day, Time begin, Time end)
        {
            Week = week;
            Day = day;
            Begin = begin;
            End = end;
        }
        public WeekType Week { get; set; }
        public Weekdays Day { get; set; }
        public Time Begin { get; set; }
        public Time End { get; set; }
    }
    public class Subject : ISubject
    {
        public Subject(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class Room : IRoom
    {
        public Room(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class Lecturer : ILecturer
    {
        public Lecturer(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class Course : ICourse
    {
        public Course(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class Direction : IDirection
    {
        public Direction(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class Group : IGroup
    {
        public Group(string name, ICourse course, IDirection direction)
        {
            Name = name;
            Course = course;
            Direction = direction;
        }
        public string Name { get; set; }
        public ICourse Course { get; set; }
        public IDirection Direction { get; set; }
    }
    public class Lecture : ILecture
    {
        public Lecture(ISubject subject, IGroup group, ILecturer lecturer, IRoom room, ILectureTime time)
        {
            Subject = subject;
            Group = group;
            Lecturer = lecturer;
            Room = room;
            Time = time;
        }
        public ISubject Subject { get; set; }
        public IGroup Group { get; set; }
        public ILecturer Lecturer { get; set; }
        public IRoom Room { get; set; }
        public ILectureTime Time { get; set; }
    }
    public class Schedule : ISchedule
    {
        ILectureTimeCollection timeLine = new LectureTimeCollection();
        IRoomCollection rooms = new RoomCollection();
        ISubjectCollection subjects = new SubjectCollection();
        ILecturerCollection lecturers = new LecturerCollection();
        ICourseCollection courses = new CourseCollection();
        IDirectionCollection directions = new DirectionCollection();
        IGroupCollection groups = new GroupCollection();
        ILectureCollection lectures = new LectureCollection();

        public ILectureTimeCollection TimeLine
        {
            get { return timeLine; }
        }
        public IRoomCollection Rooms
        {
            get { return rooms; }
        }
        public ISubjectCollection Subjects
        {
            get { return subjects; }
        }
        public ILecturerCollection Lecturers
        {
            get { return lecturers; }
        }
        public ICourseCollection Courses
        {
            get { return courses; }
        }
        public IDirectionCollection Directions
        {
            get { return directions; }
        }
        public IGroupCollection Groups
        {
            get { return groups; }
        }
        public ILectureCollection Lectures
        {
            get { return lectures; }
        }
    }
}
