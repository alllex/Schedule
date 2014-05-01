using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace ImplementationData
{

    using Ident = UInt32;

    internal abstract class HavingID
    {
        Ident _id;
        public HavingID(Ident id)
        {
            _id = id;
        }
        public Ident ID
        {
            get { return _id; }
        }
    }

    internal abstract class HavingNameAndID : HavingID, IHavingName
    {
        public HavingNameAndID(Ident id, string name)
            : base(id)
        {
            Name = name;
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    internal class LectureTime : HavingID, ILectureTime, ICloneable
    {
        public LectureTime(Ident id, WeekType week, Weekdays day, Time begin, Time end)
            : base(id)
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
        public object Clone()
        {
            return new LectureTime(ID, Week, Day, (Time)Begin.Clone(), (Time)End.Clone());
        }
    }

    internal class Subject : HavingNameAndID, ISubject, ICloneable
    {
        public Subject(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Subject(ID, Name);
        }
    }

    internal class Room : HavingNameAndID, IRoom, ICloneable
    {
        public Room(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Room(ID, Name);
        }
    }

    internal class Lecturer : HavingNameAndID, ILecturer, ICloneable
    {
        public Lecturer(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Lecturer(ID, Name);
        }
    }

    internal class Course : HavingNameAndID, ICourse, ICloneable
    {
        public Course(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Course(ID, Name);
        }
    }

    internal class Direction : HavingNameAndID, IDirection, ICloneable
    {
        public Direction(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Direction(ID, Name);
        }
    }

    internal class Group : HavingNameAndID, IGroup, ICloneable
    {
        public Group(Ident id, string name, ICourse course, IDirection direction)
            : base(id, name)
        {
            Course = course;
            Direction = direction;
        }
        public ICourse Course { get; set; }
        public IDirection Direction { get; set; }
        public object Clone()
        {
            return new Group(ID,
                             Name,
                             (ICourse)((Course)Course).Clone(),
                             (IDirection)((Direction)Direction).Clone());
        }
    }

    internal class Lecture : HavingID, ILecture, ICloneable
    {
        public Lecture(Ident id, ISubject subject, IGroup group, ILecturer lecturer, IRoom room, ILectureTime time)
            : base(id)
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
        public object Clone()
        {
            return new Lecture(ID,
                               (ISubject)((Subject)Subject).Clone(),
                               (IGroup)((Group)Group).Clone(),
                               (ILecturer)((Lecturer)Lecturer).Clone(),
                               (IRoom)((Room)Room).Clone(),
                               (ILectureTime)((LectureTime)Time).Clone());
        }
    }

    internal class LecturerCollection : ILecturerCollection
    {
        Dictionary<Ident, Lecturer> dictionary = new Dictionary<Ident, Lecturer>();
        Ident count = 0;

        public ILecturer Add(ILecturer lecturer)
        {
            count++;
            Lecturer newLecturer = new Lecturer(count, lecturer.Name);
            dictionary.Add(count, newLecturer);
            return (ILecturer)newLecturer.Clone();
        }
        public bool Remove(ILecturer lecturer)
        {
            bool wasRemoved = dictionary.Remove(((Lecturer)lecturer).ID);
            return wasRemoved;
        }
        public bool Submit(ILecturer lecturer)
        {
            Lecturer newLecturer = (Lecturer)lecturer;
            Lecturer oldLecturer;
            bool exists = dictionary.TryGetValue(newLecturer.ID, out oldLecturer);
            if (exists) dictionary[newLecturer.ID] = (Lecturer)newLecturer.Clone();
            return exists;
        }
        public IEnumerable<ILecturer> GetAll()
        {
            IEnumerable<Lecturer> collection = dictionary.Values;
            List<ILecturer> cloneCollection = new List<ILecturer>(dictionary.Count);
            foreach (Lecturer lecturer in collection) cloneCollection.Add((ILecturer)lecturer.Clone());
            return cloneCollection;
        }
    }

    internal class SubjectCollection : ISubjectCollection
    {
        Dictionary<Ident, Subject> dictionary = new Dictionary<Ident, Subject>();
        Ident count = 0;

        public ISubject Add(ISubject subject)
        {
            count++;
            Subject newSubject = new Subject(count, subject.Name);
            dictionary.Add(count, newSubject);
            return (ISubject)newSubject.Clone();
        }
        public bool Remove(ISubject subject)
        {
            bool wasRemoved = dictionary.Remove(((Subject)subject).ID);
            return wasRemoved;
        }
        public bool Submit(ISubject subject)
        {
            Subject newSubject = (Subject)subject;
            Subject oldSubject;
            bool exists = dictionary.TryGetValue(newSubject.ID, out oldSubject);
            if (exists) oldSubject = (Subject)newSubject.Clone();
            return exists;
        }
        public IEnumerable<ISubject> GetAll()
        {
            IEnumerable<Subject> collection = dictionary.Values;
            List<ISubject> cloneCollection = new List<ISubject>(dictionary.Count);
            foreach (Subject subject in collection) cloneCollection.Add((ISubject)subject.Clone());
            return cloneCollection;
        }
    }

    internal class GroupCollection : IGroupCollection
    {
        Dictionary<Ident, Group> dictionary = new Dictionary<Ident, Group>();
        Ident count = 0;

        public IGroup Add(IGroup group)
        {
            count++;
            Group newGroup = new Group(count, group.Name, group.Course, group.Direction);
            dictionary.Add(count, newGroup);
            return (IGroup)newGroup.Clone();
        }
        public bool Remove(IGroup group)
        {
            bool wasRemoved = dictionary.Remove(((Group)group).ID);
            if (wasRemoved) count--;
            return wasRemoved;
        }
        public bool Submit(IGroup group)
        {
            Group newGroup = (Group)group;
            Group oldGroup;
            bool exists = dictionary.TryGetValue(newGroup.ID, out oldGroup);
            if (exists) oldGroup = (Group)newGroup.Clone();
            return exists;
        }
        public IEnumerable<IGroup> GetAll()
        {
            IEnumerable<Group> collection = dictionary.Values;
            List<IGroup> cloneCollection = new List<IGroup>(dictionary.Count);
            foreach (Group group in collection) cloneCollection.Add((IGroup)group.Clone());
            return cloneCollection;
        }
    }

    internal class RoomCollection : IRoomCollection
    {
        Dictionary<Ident, Room> dictionary = new Dictionary<Ident, Room>();
        Ident count = 0;

        public IRoom Add(IRoom room)
        {
            count++;
            Room newRoom = new Room(count, room.Name);
            dictionary.Add(count, newRoom);
            return (IRoom)newRoom.Clone();
        }
        public bool Remove(IRoom room)
        {
            bool wasRemoved = dictionary.Remove(((Room)room).ID);
            return wasRemoved;
        }
        public bool Submit(IRoom room)
        {
            Room newRoom = (Room)room;
            Room oldRoom;
            bool exists = dictionary.TryGetValue(newRoom.ID, out oldRoom);
            if (exists) oldRoom = (Room)newRoom.Clone();
            return exists;
        }
        public IEnumerable<IRoom> GetAll()
        {
            IEnumerable<Room> collection = dictionary.Values;
            List<IRoom> cloneCollection = new List<IRoom>(dictionary.Count);
            foreach (Room room in collection) cloneCollection.Add((IRoom)room.Clone());
            return cloneCollection;
        }
    }

    internal class CourseCollection : ICourseCollection
    {
        Dictionary<Ident, Course> dictionary = new Dictionary<Ident, Course>();
        Ident count = 0;

        public ICourse Add(ICourse course)
        {
            count++;
            Course newCourse = new Course(count, course.Name);
            dictionary.Add(count, newCourse);
            return (ICourse)newCourse.Clone();
        }
        public bool Remove(ICourse course)
        {
            bool wasRemoved = dictionary.Remove(((Course)course).ID);
            return wasRemoved;
        }
        public bool Submit(ICourse course)
        {
            Course newCourse = (Course)course;
            Course oldCourse;
            bool exists = dictionary.TryGetValue(newCourse.ID, out oldCourse);
            if (exists) oldCourse = (Course)newCourse.Clone();
            return exists;
        }
        public IEnumerable<ICourse> GetAll()
        {
            IEnumerable<Course> collection = dictionary.Values;
            List<ICourse> cloneCollection = new List<ICourse>(dictionary.Count);
            foreach (Course course in collection) cloneCollection.Add((ICourse)course.Clone());
            return cloneCollection;
        }
    }

    internal class DirectionCollection : IDirectionCollection
    {
        Dictionary<Ident, Direction> dictionary = new Dictionary<Ident, Direction>();
        Ident count = 0;

        public IDirection Add(IDirection direction)
        {
            count++;
            Direction newDirection = new Direction(count, direction.Name);
            dictionary.Add(count, newDirection);
            return (IDirection)newDirection.Clone();
        }
        public bool Remove(IDirection direction)
        {
            bool wasRemoved = dictionary.Remove(((Direction)direction).ID);
            return wasRemoved;
        }
        public bool Submit(IDirection direction)
        {
            Direction newDirection = (Direction)direction;
            Direction oldDirection;
            bool exists = dictionary.TryGetValue(newDirection.ID, out oldDirection);
            if (exists) oldDirection = (Direction)newDirection.Clone();
            return exists;
        }
        public IEnumerable<IDirection> GetAll()
        {
            IEnumerable<Direction> collection = dictionary.Values;
            List<IDirection> cloneCollection = new List<IDirection>(dictionary.Count);
            foreach (Direction direction in collection) cloneCollection.Add((IDirection)direction.Clone());
            return cloneCollection;
        }
    }

    internal class LectureTimeCollection : ILectureTimeCollection
    {
        Dictionary<Ident, LectureTime> dictionary = new Dictionary<Ident, LectureTime>();
        Ident count = 0;

        public ILectureTime Add(ILectureTime lectureTime)
        {
            count++;
            LectureTime newLectureTime = new LectureTime(count,
                                                         lectureTime.Week,
                                                         lectureTime.Day,
                                                         (Time)lectureTime.Begin.Clone(),
                                                         (Time)lectureTime.End.Clone());
            dictionary.Add(count, newLectureTime);
            return (ILectureTime)newLectureTime.Clone();
        }
        public bool Remove(ILectureTime lectureTime)
        {
            bool wasRemoved = dictionary.Remove(((LectureTime)lectureTime).ID);
            return wasRemoved;
        }
        public bool Submit(ILectureTime lectureTime)
        {
            LectureTime newLectureTime = (LectureTime)lectureTime;
            LectureTime oldLectureTime;
            bool exists = dictionary.TryGetValue(newLectureTime.ID, out oldLectureTime);
            if (exists) oldLectureTime = (LectureTime)newLectureTime.Clone();
            return exists;
        }
        public IEnumerable<ILectureTime> GetAll()
        {
            IEnumerable<LectureTime> collection = dictionary.Values;
            List<ILectureTime> cloneCollection = new List<ILectureTime>(dictionary.Count);
            foreach (LectureTime time in collection) cloneCollection.Add((ILectureTime)time.Clone());
            return cloneCollection;
        }
    }

    internal class LectureCollection : ILectureCollection
    {
        Ident count = 0;
        Dictionary<Tuple<Ident, Ident>, Lecture> dictGroup = new Dictionary<Tuple<Ident, Ident>, Lecture>();
        Dictionary<Tuple<Ident, Ident>, Lecture> dictLecturer = new Dictionary<Tuple<Ident, Ident>, Lecture>();
        Dictionary<Tuple<Ident, Ident>, Lecture> dictRoom = new Dictionary<Tuple<Ident, Ident>, Lecture>();

        public ILecture Get(IGroup group, ILectureTime time)
        {
            Lecture lecture;
            Ident groupID = ((Group)group).ID;
            Ident timeID = ((LectureTime)time).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(groupID, timeID);
            bool exists = dictGroup.TryGetValue(key, out lecture);
            if (exists) return (ILecture)lecture.Clone();
            else return null;
        }
        public ILecture Get(ILecturer lecturer, ILectureTime time)
        {
            Lecture lecture;
            Ident identLecturer = ((Lecturer)lecturer).ID;
            Ident identTime = ((LectureTime)time).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(identLecturer, identTime);
            bool exists = dictGroup.TryGetValue(key, out lecture);
            if (exists) return (ILecture)lecture.Clone();
            else return null;
        }
        public ILecture Get(IRoom room, ILectureTime time)
        {
            Lecture lecture;
            Ident identRoom = ((Room)room).ID;
            Ident identTime = ((LectureTime)time).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(identRoom, identTime);
            bool exists = dictGroup.TryGetValue(key, out lecture);
            if (exists) return (ILecture)lecture.Clone();
            else return null;
        }
        public ILecture Add(ILecture lecture)
        {
            count++;
            Lecture newLecture = new Lecture(count,
                                             (ISubject)((Subject)lecture.Subject).Clone(),
                                             (IGroup)((Group)lecture.Group).Clone(),
                                             (ILecturer)((Lecturer)lecture.Lecturer).Clone(),
                                             (IRoom)((Room)lecture.Room).Clone(),
                                             (ILectureTime)((LectureTime)lecture.Time).Clone());

            Ident identGroup = ((Group)lecture.Group).ID,
                  identLecturer = ((Lecturer)lecture.Lecturer).ID,
                  identRoom = ((Room)lecture.Room).ID,
                  identTime = ((LectureTime)lecture.Time).ID;

            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime),
                                keyLecturer = new Tuple<Ident, Ident>(identLecturer, identTime),
                                keyRoom = new Tuple<Ident, Ident>(identRoom, identTime);

            dictGroup.Add(keyGroup, newLecture);
            dictLecturer.Add(keyLecturer, newLecture);
            dictRoom.Add(keyRoom, newLecture);

            return (ILecture)newLecture.Clone();
        }
        public bool Remove(ILecture lecture)
        {
            Ident identGroup = ((Group)lecture.Group).ID,
                  identLecturer = ((Lecturer)lecture.Lecturer).ID,
                  identRoom = ((Room)lecture.Room).ID,
                  identTime = ((LectureTime)lecture.Time).ID;

            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime),
                                keyLecturer = new Tuple<Ident, Ident>(identLecturer, identTime),
                                keyRoom = new Tuple<Ident, Ident>(identRoom, identTime);

            bool wasRemoved = dictGroup.Remove(keyGroup);
            dictLecturer.Remove(keyLecturer);
            dictRoom.Remove(keyRoom);

            return wasRemoved;
        }
        public bool Submit(ILecture lecture)
        {
            Ident identGroup = ((Group)lecture.Group).ID,
                  identTime = ((LectureTime)lecture.Time).ID;
            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime);
            Lecture lectureOfDictGroup;
            bool exists = dictGroup.TryGetValue(keyGroup, out lectureOfDictGroup);
            if (exists) lectureOfDictGroup = (Lecture)((Lecture)lecture).Clone();
            return exists;
        }
        public IEnumerable<ILecture> GetAll()
        {
            IEnumerable<Lecture> collection = dictGroup.Values;
            List<ILecture> cloneCollection = new List<ILecture>(dictGroup.Count);
            foreach (Lecture lecture in collection) cloneCollection.Add((ILecture)lecture.Clone());
            return cloneCollection;
        }
    }
}
