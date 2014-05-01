using System;
using System.Collections.Generic;
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

        public uint ID { get; private set; }
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

    internal class ClassTime : HavingID, IClassTime, ICloneable
    {
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
    }

    internal class LecturerCollection : ILecturerCollection
    {
        Dictionary<Ident, Lecturer> dictionary = new Dictionary<Ident, Lecturer>();
        Ident _count = 0;

        public ILecturer Add(ILecturer classTime)
        {
            _count++;
            Lecturer newLecturer = new Lecturer(_count, classTime.Name);
            dictionary.Add(_count, newLecturer);
            return (ILecturer)newLecturer.Clone();
        }
        public bool Remove(ILecturer classTime)
        {
            bool wasRemoved = dictionary.Remove(((Lecturer)classTime).ID);
            return wasRemoved;
        }
        public bool Submit(ILecturer classTime)
        {
            Lecturer newLecturer = (Lecturer)classTime;
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
        Ident _count = 0;

        public ISubject Add(ISubject classTime)
        {
            _count++;
            Subject newSubject = new Subject(_count, classTime.Name);
            dictionary.Add(_count, newSubject);
            return (ISubject)newSubject.Clone();
        }
        public bool Remove(ISubject classTime)
        {
            bool wasRemoved = dictionary.Remove(((Subject)classTime).ID);
            return wasRemoved;
        }
        public bool Submit(ISubject classTime)
        {
            Subject newSubject = (Subject)classTime;
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
        Ident _count = 0;

        public IGroup Add(IGroup classTime)
        {
            _count++;
            Group newGroup = new Group(_count, classTime.Name, classTime.YearOfStudy, classTime.Specialization);
            dictionary.Add(_count, newGroup);
            return (IGroup)newGroup.Clone();
        }
        public bool Remove(IGroup classTime)
        {
            bool wasRemoved = dictionary.Remove(((Group)classTime).ID);
            if (wasRemoved) _count--;
            return wasRemoved;
        }
        public bool Submit(IGroup classTime)
        {
            Group newGroup = (Group)classTime;
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

    internal class ClassroomCollection : IClassroomCollection
    {
        Dictionary<Ident, Classroom> dictionary = new Dictionary<Ident, Classroom>();
        Ident _count = 0;

        public IClassroom Add(IClassroom classTime)
        {
            _count++;
            Classroom newClassroom = new Classroom(_count, classTime.Name);
            dictionary.Add(_count, newClassroom);
            return (IClassroom)newClassroom.Clone();
        }
        public bool Remove(IClassroom classTime)
        {
            bool wasRemoved = dictionary.Remove(((Classroom)classTime).ID);
            return wasRemoved;
        }
        public bool Submit(IClassroom classTime)
        {
            Classroom newClassroom = (Classroom)classTime;
            Classroom oldClassroom;
            bool exists = dictionary.TryGetValue(newClassroom.ID, out oldClassroom);
            if (exists) oldClassroom = (Classroom)newClassroom.Clone();
            return exists;
        }
        public IEnumerable<IClassroom> GetAll()
        {
            IEnumerable<Classroom> collection = dictionary.Values;
            List<IClassroom> cloneCollection = new List<IClassroom>(dictionary.Count);
            foreach (Classroom room in collection) cloneCollection.Add((IClassroom)room.Clone());
            return cloneCollection;
        }
    }

    internal class YearOfStudyCollection : IYearOfStudyCollection
    {
        Dictionary<Ident, YearOfStudy> dictionary = new Dictionary<Ident, YearOfStudy>();
        Ident _count = 0;

        public IYearOfStudy Add(IYearOfStudy classTime)
        {
            _count++;
            YearOfStudy newYearOfStudy = new YearOfStudy(_count, classTime.Name);
            dictionary.Add(_count, newYearOfStudy);
            return (IYearOfStudy)newYearOfStudy.Clone();
        }
        public bool Remove(IYearOfStudy classTime)
        {
            bool wasRemoved = dictionary.Remove(((YearOfStudy)classTime).ID);
            return wasRemoved;
        }
        public bool Submit(IYearOfStudy classTime)
        {
            YearOfStudy newYearOfStudy = (YearOfStudy)classTime;
            YearOfStudy oldYearOfStudy;
            bool exists = dictionary.TryGetValue(newYearOfStudy.ID, out oldYearOfStudy);
            if (exists) oldYearOfStudy = (YearOfStudy)newYearOfStudy.Clone();
            return exists;
        }
        public IEnumerable<IYearOfStudy> GetAll()
        {
            IEnumerable<YearOfStudy> collection = dictionary.Values;
            List<IYearOfStudy> cloneCollection = new List<IYearOfStudy>(dictionary.Count);
            foreach (YearOfStudy course in collection) cloneCollection.Add((IYearOfStudy)course.Clone());
            return cloneCollection;
        }
    }

    internal class SpecializationCollection : ISpecializationCollection
    {
        Dictionary<Ident, Specialization> dictionary = new Dictionary<Ident, Specialization>();
        Ident _count = 0;

        public ISpecialization Add(ISpecialization classTime)
        {
            _count++;
            Specialization newSpecialization = new Specialization(_count, classTime.Name);
            dictionary.Add(_count, newSpecialization);
            return (ISpecialization)newSpecialization.Clone();
        }
        public bool Remove(ISpecialization classTime)
        {
            bool wasRemoved = dictionary.Remove(((Specialization)classTime).ID);
            return wasRemoved;
        }
        public bool Submit(ISpecialization classTime)
        {
            Specialization newSpecialization = (Specialization)classTime;
            Specialization oldSpecialization;
            bool exists = dictionary.TryGetValue(newSpecialization.ID, out oldSpecialization);
            if (exists) oldSpecialization = (Specialization)newSpecialization.Clone();
            return exists;
        }
        public IEnumerable<ISpecialization> GetAll()
        {
            IEnumerable<Specialization> collection = dictionary.Values;
            List<ISpecialization> cloneCollection = new List<ISpecialization>(dictionary.Count);
            foreach (Specialization direction in collection) cloneCollection.Add((ISpecialization)direction.Clone());
            return cloneCollection;
        }
    }

    internal class ClassTimeCollection : IClassTimeCollection
    {
        Dictionary<Ident, ClassTime> dictionary = new Dictionary<Ident, ClassTime>();
        Ident _count = 0;

        public IClassTime Add(IClassTime classTime)
        {
            _count++;
            ClassTime newClassTime = new ClassTime(_count,
                                                         classTime.Week,
                                                         classTime.Day,
                                                         (Time)classTime.Begin.Clone(),
                                                         (Time)classTime.End.Clone());
            dictionary.Add(_count, newClassTime);
            return (IClassTime)newClassTime.Clone();
        }
        public bool Remove(IClassTime classTime)
        {
            bool wasRemoved = dictionary.Remove(((ClassTime)classTime).ID);
            return wasRemoved;
        }
        public bool Submit(IClassTime classTime)
        {
            ClassTime newClassTime = (ClassTime)classTime;
            ClassTime oldClassTime;
            bool exists = dictionary.TryGetValue(newClassTime.ID, out oldClassTime);
            if (exists) oldClassTime = (ClassTime)newClassTime.Clone();
            return exists;
        }
        public IEnumerable<IClassTime> GetAll()
        {
            IEnumerable<ClassTime> collection = dictionary.Values;
            List<IClassTime> cloneCollection = new List<IClassTime>(dictionary.Count);
            foreach (ClassTime time in collection) cloneCollection.Add((IClassTime)time.Clone());
            return cloneCollection;
        }
    }

    internal class ClassCollection : IClassCollection
    {
        Ident _count = 0;
        Dictionary<Tuple<Ident, Ident>, Class> dictGroup = new Dictionary<Tuple<Ident, Ident>, Class>();
        Dictionary<Tuple<Ident, Ident>, Class> dictLecturer = new Dictionary<Tuple<Ident, Ident>, Class>();
        Dictionary<Tuple<Ident, Ident>, Class> dictRoom = new Dictionary<Tuple<Ident, Ident>, Class>();

        public IClass Get(IGroup group, IClassTime classTime)
        {
            Class @class;
            Ident groupID = ((Group)group).ID;
            Ident timeID = ((ClassTime)classTime).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(groupID, timeID);
            bool exists = dictGroup.TryGetValue(key, out @class);
            if (exists) return (IClass)@class.Clone();
            else return null;
        }
        public IClass Get(ILecturer lecturer, IClassTime classTime)
        {
            Class @class;
            Ident identLecturer = ((Lecturer)lecturer).ID;
            Ident identTime = ((ClassTime)classTime).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(identLecturer, identTime);
            bool exists = dictGroup.TryGetValue(key, out @class);
            if (exists) return (IClass)@class.Clone();
            else return null;
        }
        public IClass Get(IClassroom classroom, IClassTime classTime)
        {
            Class @class;
            Ident identRoom = ((Classroom)classroom).ID;
            Ident identTime = ((ClassTime)classTime).ID;
            Tuple<Ident, Ident> key = new Tuple<Ident, Ident>(identRoom, identTime);
            bool exists = dictGroup.TryGetValue(key, out @class);
            if (exists) return (IClass)@class.Clone();
            else return null;
        }
        public IClass Add(IClass classTime)
        {
            _count++;
            Class newClass = new Class(_count,
                                             (ISubject)((Subject)classTime.Subject).Clone(),
                                             (IGroup)((Group)classTime.Group).Clone(),
                                             (ILecturer)((Lecturer)classTime.Lecturer).Clone(),
                                             (IClassroom)((Classroom)classTime.Classroom).Clone(),
                                             (IClassTime)((ClassTime)classTime.Time).Clone());

            Ident identGroup = ((Group)classTime.Group).ID,
                  identLecturer = ((Lecturer)classTime.Lecturer).ID,
                  identRoom = ((Classroom)classTime.Classroom).ID,
                  identTime = ((ClassTime)classTime.Time).ID;

            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime),
                                keyLecturer = new Tuple<Ident, Ident>(identLecturer, identTime),
                                keyRoom = new Tuple<Ident, Ident>(identRoom, identTime);

            dictGroup.Add(keyGroup, newClass);
            dictLecturer.Add(keyLecturer, newClass);
            dictRoom.Add(keyRoom, newClass);

            return (IClass)newClass.Clone();
        }
        public bool Remove(IClass classTime)
        {
            Ident identGroup = ((Group)classTime.Group).ID,
                  identLecturer = ((Lecturer)classTime.Lecturer).ID,
                  identRoom = ((Classroom)classTime.Classroom).ID,
                  identTime = ((ClassTime)classTime.Time).ID;

            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime),
                                keyLecturer = new Tuple<Ident, Ident>(identLecturer, identTime),
                                keyRoom = new Tuple<Ident, Ident>(identRoom, identTime);

            bool wasRemoved = dictGroup.Remove(keyGroup);
            dictLecturer.Remove(keyLecturer);
            dictRoom.Remove(keyRoom);

            return wasRemoved;
        }
        public bool Submit(IClass classTime)
        {
            Ident identGroup = ((Group)classTime.Group).ID,
                  identTime = ((ClassTime)classTime.Time).ID;
            Tuple<Ident, Ident> keyGroup = new Tuple<Ident, Ident>(identGroup, identTime);
            Class classOfDictGroup;
            bool exists = dictGroup.TryGetValue(keyGroup, out classOfDictGroup);
            if (exists) classOfDictGroup = (Class)((Class)classTime).Clone();
            return exists;
        }
        public IEnumerable<IClass> GetAll()
        {
            IEnumerable<Class> collection = dictGroup.Values;
            List<IClass> cloneCollection = new List<IClass>(dictGroup.Count);
            foreach (Class lecture in collection) cloneCollection.Add((IClass)lecture.Clone());
            return cloneCollection;
        }
    }
}
