using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ScheduleData
{

    [Serializable]
    abstract public class sHavingId
    {
        public int ID;

        protected sHavingId(int id)
        {
            ID = id;
        }

        protected sHavingId(HavingId havingId)
        {
            ID = havingId.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }    
    
    [Serializable]
    abstract public class sHavingName : sHavingId
    {
        public string Name;

        protected sHavingName(int id, string name) : base(id)
        {
            Name = name;
        }

        protected sHavingName(HavingName havingName) : base(havingName)
        {
            Name = havingName.Name;
        }
    }

    [Serializable]
    public class sClassTime : sHavingId
    {
        public Weekdays Day;
        public int Number;

        public sClassTime(int id, Weekdays day, int number) : base(id)
        {
            Day = day;
            Number = number;
        }

        public sClassTime(ClassTime time) : base(time)
        {
            Day = time.Day;
            Number = time.Number;
        }

        public ClassTime ToNonSerializable()
        {
            return new ClassTime { Day = Day, Number = Number };
        }
    }

    [Serializable]
    public class sSubject : sHavingName
    {
        public sSubject(int id, string name) : base(id, name)
        {
        }
        
        public sSubject(Subject subject) : base(subject)
        {
        }

        public Subject ToNonSerializable()
        {
            return new Subject { Name = Name };
        }
    }

    [Serializable]
    public class sSpecialization : sHavingName
    {
        public sSpecialization(Specialization specialization) : base(specialization)
        {
        }

        public sSpecialization(int id, string name) : base(id, name)
        {
        }

        public Specialization ToNonSerializable()
        {
            return new Specialization { Name = Name };
        }
    }

    [Serializable]
    public class sYearOfStudy : sHavingName
    {
        public sYearOfStudy(int id, string name) : base(id, name)
        {
        }
        
        public sYearOfStudy(YearOfStudy yearOfStudy) : base(yearOfStudy)
        {
        }

        public YearOfStudy ToNonSerializable()
        {
            return new YearOfStudy {Name = Name};
        }
    }

    [Serializable]
    public class sLecturer : sHavingName
    {
        public string Degree;
        public string Department;

        public sLecturer(int id, string name, string degree, string department) : base(id, name)
        {
            Degree = degree;
            Department = department;
        }
        
        public sLecturer(Lecturer lecturer) : base(lecturer)
        {
            Degree = lecturer.Degree;
            Department = lecturer.Department;
        }

        public Lecturer ToNonSerializable()
        {
            return new Lecturer { Name = Name, Degree = Degree, Department = Department };
        }
    }

    [Serializable]
    public class sClassroom : sHavingName
    {
        public string Address;

        public sClassroom(int id, string name, string address) : base(id, name)
        {
            Address = address;
        }
        
        public sClassroom(Classroom classroom) : base(classroom)
        {
            Address = classroom.Address;
        }

        public Classroom ToNonSerializable()
        {
            return new Classroom { Name = Name, Address = Address };
        }
    }

    [Serializable]
    public class sGroup : sHavingName
    {
        public sSpecialization Specialization;
        public sYearOfStudy YearOfStudy;

        public sGroup(int id, string name, sSpecialization specialization, sYearOfStudy yearOfStudy) : base(id, name)
        {
            Specialization = specialization;
            YearOfStudy = yearOfStudy;
        }
        
        public sGroup(Group group, sSchedule schedule) : base(group)
        {
            if (group == null) return;
            Specialization = group.Specialization == null ? null : schedule.sDictionaries.Specializations[group.Specialization.GetHashCode()];
            YearOfStudy = group.YearOfStudy == null ? null : schedule.sDictionaries.YearsOfStudy[group.YearOfStudy.GetHashCode()];
        }

        public Group ToNonSerializable(sSchedule schedule)
        {
            return new Group
            {
                Name = Name,
                Specialization = Specialization == null ? null : schedule.Dictionaries.Specializations[Specialization.ID],
                YearOfStudy = YearOfStudy == null ? null : schedule.Dictionaries.YearsOfStudy[YearOfStudy.ID]
            };
        }
    }

    [Serializable]
    public class sClassRecord : sHavingId
    {
        public sSubject Subject;
        public sLecturer Lecturer;
        public sClassroom Classroom;
        public sClassTime ClassTime;
        public sGroup Group;

        public sClassRecord(int id, sSubject subject, sLecturer lecturer, sClassroom classroom, sClassTime classTime, sGroup group) : base(id)
        {
            Subject = subject;
            Lecturer = lecturer;
            Classroom = classroom;
            ClassTime = classTime;
            Group = group;          
        }
        
        public sClassRecord(ClassRecord classRecord, sSchedule schedule) : base(classRecord)
        {
            if (classRecord == null) return;
            Subject = classRecord.Subject == null ? null : schedule.sDictionaries.Subjects[classRecord.Subject.GetHashCode()];
            Lecturer = classRecord.Lecturer == null ? null : schedule.sDictionaries.Lecturers[classRecord.Lecturer.GetHashCode()];
            Classroom = classRecord.Classroom == null ? null : schedule.sDictionaries.Classrooms[classRecord.Classroom.GetHashCode()];
            ClassTime = classRecord.ClassTime == null ? null : schedule.sDictionaries.TimeLine[classRecord.ClassTime.GetHashCode()];
            Group = classRecord.Group == null ? null : schedule.sDictionaries.Groups[classRecord.Group.GetHashCode()];
        }

        public ClassRecord ToNoneSerializable(sSchedule schedule)
        {
            return new ClassRecord
            {
                Classroom = Classroom == null ? null : schedule.Dictionaries.Classrooms[Classroom.ID],
                ClassTime = ClassTime == null ? null : schedule.Dictionaries.TimeLine[ClassTime.ID],
                Lecturer = Lecturer == null ? null : schedule.Dictionaries.Lecturers[Lecturer.ID],
                Subject = Subject == null ? null : schedule.Dictionaries.Subjects[Subject.ID],
                Group = Group == null ? null : schedule.Dictionaries.Groups[Group.ID]
            };
        }
    }

    public class Dictionaries
    {
        public Dictionary<int, ClassTime> TimeLine;
        public Dictionary<int, Group> Groups;
        public Dictionary<int, Lecturer> Lecturers;
        public Dictionary<int, Classroom> Classrooms;
        public Dictionary<int, Subject> Subjects;
        public Dictionary<int, Specialization> Specializations;
        public Dictionary<int, YearOfStudy> YearsOfStudy;
        public Dictionary<int, ClassRecord> ClassRecords;
    }

    [Serializable]
    public class sDictionaries
    {
        public Dictionary<int, sClassTime> TimeLine;
        public Dictionary<int, sGroup> Groups;
        public Dictionary<int, sLecturer> Lecturers;
        public Dictionary<int, sClassroom> Classrooms;
        public Dictionary<int, sSubject> Subjects;
        public Dictionary<int, sSpecialization> Specializations;
        public Dictionary<int, sYearOfStudy> YearsOfStudy;
        public Dictionary<int, sClassRecord> ClassRecords;
    }
    
    [Serializable]
    public class sSchedule : sHavingId
    {
        public sClassTime[] TimeLine;
        public sGroup[] Groups;
        public sLecturer[] Lecturers;
        public sClassroom[] Classrooms;
        public sSubject[] Subjects;
        public sSpecialization[] Specializations;
        public sYearOfStudy[] YearsOfStudy;
        public sClassRecord[] ClassRecords;

        [NonSerialized]
        public sDictionaries sDictionaries;

        [NonSerialized]
        public Schedule Schedule;

        [NonSerialized]
        public Dictionaries Dictionaries;

        # region Create

        public static sSchedule Create(Schedule schedule)
        {
            return new sSchedule(schedule);
        }

        private sSchedule(Schedule schedule) : base(schedule.GetHashCode())
        {
            sDictionaries = new sDictionaries();
            Schedule = schedule;

            CopySubjects();
            CopySpecializations();
            CopyYearsOfStudy();
            CopyLecturers();
            CopyClassrooms();
            CopyTimeLine();
            CopyGroups();
            CopyClassRecords();
        }

        private void Copy<T, sT>(ObservableCollection<T> from, sT[] to, Dictionary<int, sT> dict, Func<T, sT> converter)
        {

            var count = from.Count;
            dict = new Dictionary<int, sT>(count);
            to = new sT[count];
            for (int i = 0; i < count; ++i)
            {
                var oldObject = from[i];
                var newObject = converter(oldObject);
                to[i] = newObject;
                dict.Add(oldObject.GetHashCode(), newObject);
            }
        }

        private void CopySubjects()
        {
            Copy(Schedule.Subjects, Subjects, sDictionaries.Subjects, s => new sSubject(s));
        }
        
        private void CopySpecializations()
        {
            Copy(Schedule.Specializations, Specializations, sDictionaries.Specializations, s => new sSpecialization(s)); 
        }

        private void CopyYearsOfStudy()
        {
            Copy(Schedule.YearsOfStudy, YearsOfStudy, sDictionaries.YearsOfStudy, y => new sYearOfStudy(y));
        }

        private void CopyLecturers()
        {
            Copy(Schedule.Lecturers, Lecturers, sDictionaries.Lecturers, l => new sLecturer(l));
        }

        private void CopyClassrooms()
        {
            Copy(Schedule.Classrooms, Classrooms, sDictionaries.Classrooms, c => new sClassroom(c));
        }

        private void CopyTimeLine()
        {
            Copy(Schedule.TimeLine, TimeLine, sDictionaries.TimeLine, t => new sClassTime(t));
        }
        
        private void CopyGroups()
        {
            Copy(Schedule.Groups, Groups, sDictionaries.Groups, g => new sGroup(g, this));
        }

        private void CopyClassRecords()
        {
            Copy(Schedule.ClassRecords, ClassRecords, sDictionaries.ClassRecords, c => new sClassRecord(c, this));
        }

        # endregion

        # region ToMainSchedule

        public Schedule ToMainSchedule()
        {
            Dictionaries = new Dictionaries();
            Schedule = new Schedule();

            NonSerializableTimeLine();
            NonSerializableLectures();
            NonSerializableClassrooms();
            NonSerializableSubjects();
            NonSerializableSpecializations();
            NonSerializableYearsOfStudy();
            NonSerializableGroups();
            NonSerializableClassRecords();

            return Schedule;
        }

        private void Copy<sT, T>(sT[] from, ObservableCollection<T> to, Dictionary<int, T> dict, Func<sT, T> converter)
        {

            var count = from.Length;
            dict = new Dictionary<int, T>(count);
            for (int i = 0; i < count; ++i)
            {
                var oldObject = from[i];
                var newObject = converter(oldObject);
                to.Add(newObject);
                dict.Add(oldObject.GetHashCode(), newObject);
            }
        }

        private void NonSerializableTimeLine()
        {
            Copy(TimeLine, Schedule.TimeLine, Dictionaries.TimeLine, t => t.ToNonSerializable());
        }

        private void NonSerializableLectures()
        {
            Copy(Lecturers, Schedule.Lecturers, Dictionaries.Lecturers, l => l.ToNonSerializable());
        }

        private void NonSerializableClassrooms()
        {
            Copy(Classrooms, Schedule.Classrooms, Dictionaries.Classrooms, c => c.ToNonSerializable());
        }

        private void NonSerializableSubjects()
        {
            Copy(Subjects, Schedule.Subjects, Dictionaries.Subjects, s => s.ToNonSerializable());
        }

        private void NonSerializableSpecializations()
        {
            Copy(Specializations, Schedule.Specializations, Dictionaries.Specializations, s => s.ToNonSerializable());
        }

        private void NonSerializableYearsOfStudy()
        {
            Copy(YearsOfStudy, Schedule.YearsOfStudy, Dictionaries.YearsOfStudy, y => y.ToNonSerializable());
        }

        private void NonSerializableGroups()
        {
            Copy(Groups, Schedule.Groups, Dictionaries.Groups, g => g.ToNonSerializable(this));
        }

        private void NonSerializableClassRecords()
        {
            Copy(ClassRecords, Schedule.ClassRecords, Dictionaries.ClassRecords, c => c.ToNoneSerializable(this));
        }

        #endregion

        #region Serialize

        public static void Save(sSchedule schedule, string path)
        {
            var streamSave = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var serializer = new BinaryFormatter();

            serializer.Serialize(streamSave, schedule);
            streamSave.Close();
        }

        public static sSchedule Load(string path)
        {
            var streamLoad = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var deserializer = new BinaryFormatter();

            var schedule = (sSchedule)deserializer.Deserialize(streamLoad);
            streamLoad.Close();
            return schedule;
        }

        public void Save(string path)
        {
            Save(this, path);
        }

        # endregion
    }

    public class SaveLoadSchedule
    {
        public static void Save(Schedule schedule, string path)
        {
            sSchedule.Create(schedule).Save(path);
        }

        public static Schedule Load(string path)
        {
            return sSchedule.Load(path).ToMainSchedule();
        }
    }
}