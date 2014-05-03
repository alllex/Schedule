using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using ScheduleData.Interfaces;

namespace ScheduleData
{

    using Ident = UInt32;

    internal abstract class HavingID
    {
        public HavingID(Ident id)
        {
            ID = id;
        }

        public Ident ID { get; private set; }
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
        public int CompareTo(object obj)
        {
            var t = (IHavingName)obj;
            return t == null ? 1 : String.Compare(Name, t.Name, StringComparison.Ordinal);
        }
    }

    internal class ClassTime : HavingID, IClassTime, ICloneable
    {
        protected bool Equals(ClassTime other)
        {
            return Week == other.Week && Day == other.Day && Equals(Begin, other.Begin) && Equals(End, other.End);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int) Week;
                hashCode = (hashCode*397) ^ (int) Day;
                hashCode = (hashCode*397) ^ (Begin != null ? Begin.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (End != null ? End.GetHashCode() : 0);
                return hashCode;
            }
        }

        public ClassTime(Ident id, WeekType week, Weekdays day, Time begin, Time end)
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
            return new ClassTime(ID, Week, Day, (Time)Begin.Clone(), (Time)End.Clone());
        }

        public int CompareTo(object t)
        {
            var other = (Time)t;
            if (other == null) return 1;
            return ((ITimeInterval)this).CompareTo(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClassTime) obj);
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

        public ClassType ClassType { get; set; }
    }

    internal class Classroom : HavingNameAndID, IClassroom, ICloneable
    {
        public Classroom(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Classroom(ID, Name);
        }

        public string Address { get; set; }
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

        public string Degree { get; set; }
        public IDepartment Department { get; set; }
    }

    internal class YearOfStudy : HavingNameAndID, IYearOfStudy, ICloneable
    {
        public YearOfStudy(Ident id, string name)
            : base(id, name)
        {
        }

        public object Clone()
        {
            return new YearOfStudy(ID, Name);
        }
    }

    internal class Specialization : HavingNameAndID, ISpecialization, ICloneable
    {
        public Specialization(Ident id, string name)
            : base(id, name)
        {
        }
        public object Clone()
        {
            return new Specialization(ID, Name);
        }
    }

    internal class Group : HavingNameAndID, IGroup, ICloneable
    {
        public Group(Ident id, string name, IYearOfStudy yearOfStudy, ISpecialization specialization)
            : base(id, name)
        {
            YearOfStudy = yearOfStudy;
            Specialization = specialization;
        }
        public IYearOfStudy YearOfStudy { get; set; }
        public ISpecialization Specialization { get; set; }
        public object Clone()
        {
            return new Group(ID,
                             Name,
                             (IYearOfStudy)((YearOfStudy)YearOfStudy).Clone(),
                             (ISpecialization)((Specialization)Specialization).Clone());
        }
    }

    internal class Class : HavingID, IClass, ICloneable
    {
        public Class(Ident id, ISubject subject, IGroup group, ILecturer lecturer, IClassroom classroom, IClassTime time)
            : base(id)
        {
            Subject = subject;
            Group = group;
            Lecturer = lecturer;
            Classroom = classroom;
            Time = time;
        }
        public ISubject Subject { get; set; }
        public IGroup Group { get; set; }
        public ILecturer Lecturer { get; set; }
        public IClassroom Classroom { get; set; }
        public IClassTime Time { get; set; }
        public object Clone()
        {
            return new Class(ID,
                               (ISubject)((Subject)Subject).Clone(),
                               (IGroup)((Group)Group).Clone(),
                               (ILecturer)((Lecturer)Lecturer).Clone(),
                               (IClassroom)((Classroom)Classroom).Clone(),
                               (IClassTime)((ClassTime)Time).Clone());
        }
        public int CompareTo(object obj)
        {
            var t = (IClass)obj;
            return t == null ? 1 : Subject.CompareTo(t.Subject);
        }
    }

    internal class LecturerCollection : ILecturerCollection
    {
        readonly Dictionary<Ident, Lecturer> _dictionary = new Dictionary<Ident, Lecturer>();
        Ident _count = 0;

        public ILecturer Add(ILecturer lecturer)
        {
            _count++;
            var newLecturer = new Lecturer(_count, lecturer.Name);
            _dictionary.Add(_count, newLecturer);
            return (ILecturer)newLecturer.Clone();
        }

        public bool Remove(ILecturer lecture)
        {
            var l = (Lecturer) lecture;
            return l != null && _dictionary.Remove(l.ID);
        }

        public bool Submit(ILecturer lecture)
        {
            var newLecturer = (Lecturer)lecture;
            if (newLecturer == null) return false;
            Lecturer oldLecturer;
            var exists = _dictionary.TryGetValue(newLecturer.ID, out oldLecturer);
            if (exists) _dictionary[newLecturer.ID] = (Lecturer)newLecturer.Clone();
            return exists;
        }

        public IEnumerable<ILecturer> GetAll()
        {
            var cloneCollection = new List<ILecturer>();
            cloneCollection.AddRange(_dictionary.Values.Select(lecturer => (ILecturer) lecturer.Clone()));
            return cloneCollection;
        }
    }

    internal class SubjectCollection : ISubjectCollection
    {
        readonly Dictionary<Ident, Subject> _dictionary = new Dictionary<Ident, Subject>();
        Ident _count = 0;

        public ISubject Add(ISubject lecturer)
        {
            _count++;
            var newSubject = new Subject(_count, lecturer.Name);
            _dictionary.Add(_count, newSubject);
            return (ISubject)newSubject.Clone();
        }

        public bool Remove(ISubject subject)
        {
            var ct = (Subject) subject;
            return ct != null && _dictionary.Remove(((Subject)subject).ID);
        }

        public bool Submit(ISubject subject)
        {
            var newSubject = (Subject)subject;
            if (newSubject == null) return false;
            Subject oldSubject;
            var exists = _dictionary.TryGetValue(newSubject.ID, out oldSubject);
            if (exists) _dictionary[newSubject.ID] = (Subject)newSubject.Clone();
            return exists;
        }

        public IEnumerable<ISubject> GetAll()
        {
            var cloneCollection = new List<ISubject>();
            cloneCollection.AddRange(_dictionary.Values.Select(subject => (ISubject)subject.Clone()));
            return cloneCollection;
        }
    }

    internal class GroupCollection : IGroupCollection
    {
        readonly Dictionary<Ident, Group> _dictionary = new Dictionary<Ident, Group>();
        Ident _count = 0;

        public IGroup Add(IGroup @group)
        {
            _count++;
            var newGroup = new Group(_count, @group.Name, @group.YearOfStudy, @group.Specialization);
            _dictionary.Add(_count, newGroup);
            return (IGroup)newGroup.Clone();
        }

        public bool Remove(IGroup @group)
        {
            var g = (Group) @group;
            return g != null && _dictionary.Remove(g.ID);
        }

        public bool Submit(IGroup @group)
        {
            var newGroup = (Group)@group;
            if (newGroup == null) return false;
            Group oldGroup;
            var exists = _dictionary.TryGetValue(newGroup.ID, out oldGroup);
            if (exists) _dictionary[newGroup.ID] = (Group)newGroup.Clone();
            return exists;
        }

        public IEnumerable<IGroup> GetAll()
        {
            var cloneCollection = new List<IGroup>();
            cloneCollection.AddRange(_dictionary.Values.Select(@group => (IGroup) @group.Clone()));
            return cloneCollection;
        }
    }

    internal class ClassroomCollection : IClassroomCollection
    {
        readonly Dictionary<Ident, Classroom> _dictionary = new Dictionary<Ident, Classroom>();
        Ident _count = 0;

        public IClassroom Add(IClassroom classroom)
        {
            _count++;
            Classroom newClassroom = new Classroom(_count, classroom.Name);
            _dictionary.Add(_count, newClassroom);
            return (IClassroom)newClassroom.Clone();
        }

        public bool Remove(IClassroom classroom)
        {
            var c = (Classroom) classroom;
            return c != null && _dictionary.Remove(((Classroom)classroom).ID);
        }

        public bool Submit(IClassroom classroom)
        {
            var newClassroom = (Classroom)classroom;
            if (newClassroom == null) return false;
            Classroom oldClassroom;
            bool exists = _dictionary.TryGetValue(newClassroom.ID, out oldClassroom);
            if (exists) _dictionary[newClassroom.ID] = (Classroom)newClassroom.Clone();
            return exists;
        }

        public IEnumerable<IClassroom> GetAll()
        {
            var cloneCollection = new List<IClassroom>();
            cloneCollection.AddRange(_dictionary.Values.Select(room => (IClassroom) room.Clone()));
            return cloneCollection;
        }
    }

    internal class YearOfStudyCollection : IYearOfStudyCollection
    {
        readonly Dictionary<Ident, YearOfStudy> _dictionary = new Dictionary<Ident, YearOfStudy>();
        Ident _count = 0;

        public IYearOfStudy Add(IYearOfStudy year)
        {
            _count++;
            YearOfStudy newYearOfStudy = new YearOfStudy(_count, year.Name);
            _dictionary.Add(_count, newYearOfStudy);
            return (IYearOfStudy)newYearOfStudy.Clone();
        }
        public bool Remove(IYearOfStudy year)
        {
            bool wasRemoved = _dictionary.Remove(((YearOfStudy)year).ID);
            return wasRemoved;
        }
        public bool Submit(IYearOfStudy year)
        {
            YearOfStudy newYearOfStudy = (YearOfStudy)year;
            YearOfStudy oldYearOfStudy;
            bool exists = _dictionary.TryGetValue(newYearOfStudy.ID, out oldYearOfStudy);
            if (exists) _dictionary[newYearOfStudy.ID] = (YearOfStudy)newYearOfStudy.Clone();
            return exists;
        }
        public IEnumerable<IYearOfStudy> GetAll()
        {
            var cloneCollection = new List<IYearOfStudy>();
            cloneCollection.AddRange(_dictionary.Values.Select(year => (IYearOfStudy)year.Clone()));
            return cloneCollection;
        }
    }

    internal class SpecializationCollection : ISpecializationCollection
    {
        readonly Dictionary<Ident, Specialization> _dictionary = new Dictionary<Ident, Specialization>();
        Ident _count = 0;

        public ISpecialization Add(ISpecialization lecturer)
        {
            _count++;
            Specialization newSpecialization = new Specialization(_count, lecturer.Name);
            _dictionary.Add(_count, newSpecialization);
            return (ISpecialization)newSpecialization.Clone();
        }
        public bool Remove(ISpecialization subject)
        {
            bool wasRemoved = _dictionary.Remove(((Specialization)subject).ID);
            return wasRemoved;
        }
        public bool Submit(ISpecialization subject)
        {
            Specialization newSpecialization = (Specialization)subject;
            Specialization oldSpecialization;
            bool exists = _dictionary.TryGetValue(newSpecialization.ID, out oldSpecialization);
            if (exists) _dictionary[newSpecialization.ID] = (Specialization)newSpecialization.Clone();
            return exists;
        }
        public IEnumerable<ISpecialization> GetAll()
        {
            var cloneCollection = new List<ISpecialization>();
            cloneCollection.AddRange(_dictionary.Values.Select(spec => (ISpecialization)spec.Clone()));
            return cloneCollection;
        }
    }

    internal class ClassTimeCollection : IClassTimeCollection
    {
        readonly Dictionary<Ident, ClassTime> _dictionary = new Dictionary<Ident, ClassTime>();
        Ident _count = 0;

        public IClassTime Add(IClassTime lecturer)
        {
            _count++;
            ClassTime newClassTime = new ClassTime(_count,
                                                         lecturer.Week,
                                                         lecturer.Day,
                                                         (Time)lecturer.Begin.Clone(),
                                                         (Time)lecturer.End.Clone());
            _dictionary.Add(_count, newClassTime);
            return (IClassTime)newClassTime.Clone();
        }
        public bool Remove(IClassTime subject)
        {
            bool wasRemoved = _dictionary.Remove(((ClassTime)subject).ID);
            return wasRemoved;
        }
        public bool Submit(IClassTime subject)
        {
            ClassTime newClassTime = (ClassTime)subject;
            ClassTime oldClassTime;
            bool exists = _dictionary.TryGetValue(newClassTime.ID, out oldClassTime);
            if (exists) _dictionary[newClassTime.ID] = (ClassTime)newClassTime.Clone();
            return exists;
        }
        public IEnumerable<IClassTime> GetAll()
        {
            var cloneCollection = new List<IClassTime>();
            cloneCollection.AddRange(_dictionary.Values.Select(t => (IClassTime)t.Clone()));
            return cloneCollection;
        }
    }

    internal class ClassCollection : IClassCollection
    {
        Ident _count = 0;
        readonly Dictionary<Tuple<Ident, Ident>, Class> _dictGroup = new Dictionary<Tuple<Ident, Ident>, Class>();
        readonly Dictionary<Tuple<Ident, Ident>, Class> _dictLecturer = new Dictionary<Tuple<Ident, Ident>, Class>();
        readonly Dictionary<Tuple<Ident, Ident>, Class> _dictRoom = new Dictionary<Tuple<Ident, Ident>, Class>();

        public IClass Get(IGroup group, IClassTime classTime)
        {
            Class @class;
            Ident groupID = ((Group)group).ID;
            Ident timeID = ((ClassTime)classTime).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(groupID, timeID);
            bool exists = _dictGroup.TryGetValue(key, out @class);
            if (exists) return (IClass)@class.Clone();
            return null;
        }
        public IClass Get(ILecturer lecturer, IClassTime classTime)
        {
            Class @class;
            Ident identLecturer = ((Lecturer)lecturer).ID;
            Ident identTime = ((ClassTime)classTime).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(identLecturer, identTime);
            bool exists = _dictLecturer.TryGetValue(key, out @class);
            if (exists) return (IClass)@class.Clone();
            else return null;
        }
        public IClass Get(IClassroom classroom, IClassTime classTime)
        {
            Class @class;
            Ident identRoom = ((Classroom)classroom).ID;
            Ident identTime = ((ClassTime)classTime).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(identRoom, identTime);
            bool exists = _dictRoom.TryGetValue(key, out @class);
            if (exists) return (IClass)@class.Clone();
            else return null;
        }
        public IClass Add(IClass @class)
        {
            _count++;
            Class newClass = new Class(_count,
                                             (ISubject)((Subject)@class.Subject).Clone(),
                                             (IGroup)((Group)@class.Group).Clone(),
                                             (ILecturer)((Lecturer)@class.Lecturer).Clone(),
                                             (IClassroom)((Classroom)@class.Classroom).Clone(),
                                             (IClassTime)((ClassTime)@class.Time).Clone());

            Ident identGroup = ((Group)@class.Group).ID,
                  identLecturer = ((Lecturer)@class.Lecturer).ID,
                  identRoom = ((Classroom)@class.Classroom).ID,
                  identTime = ((ClassTime)@class.Time).ID;

            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime),
                                keyLecturer = new Tuple<Ident, Ident>(identLecturer, identTime),
                                keyRoom = new Tuple<Ident, Ident>(identRoom, identTime);

            _dictGroup.Add(keyGroup, newClass);
            _dictLecturer.Add(keyLecturer, newClass);
            _dictRoom.Add(keyRoom, newClass);

            return (IClass)newClass.Clone();
        }

        public bool Remove(IClass @class)
        {
            Ident identGroup = ((Group)@class.Group).ID,
                  identLecturer = ((Lecturer)@class.Lecturer).ID,
                  identRoom = ((Classroom)@class.Classroom).ID,
                  identTime = ((ClassTime)@class.Time).ID;

            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime),
                                keyLecturer = new Tuple<Ident, Ident>(identLecturer, identTime),
                                keyRoom = new Tuple<Ident, Ident>(identRoom, identTime);

            bool wasRemoved = _dictGroup.Remove(keyGroup);
            _dictLecturer.Remove(keyLecturer);
            _dictRoom.Remove(keyRoom);

            return wasRemoved;
        }

        public bool Submit(IClass @class)
        {
            Ident identGroup = ((Group)@class.Group).ID,
                  identTime = ((ClassTime)@class.Time).ID;
            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime);
            Class classOfDictGroup;
            bool exists = _dictGroup.TryGetValue(keyGroup, out classOfDictGroup);
            if (exists) classOfDictGroup = (Class)((Class)@class).Clone();
            return exists;
        }

        public IEnumerable<IClass> GetAll()
        {
            var cloneCollection = new List<IClass>();
            cloneCollection.AddRange(_dictGroup.Values.Select(t => (IClass)t.Clone()));
            return cloneCollection;
        }
    }
}
