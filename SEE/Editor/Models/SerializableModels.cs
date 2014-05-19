using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Editor.Models
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

        public Specialization ToNonSerializable()
        {
            return new Specialization { Name = Name };
        }
    }

    [Serializable]
    public class sYearOfStudy : sHavingName
    {
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

        public sGroup(Group group, sClassesSchedule schedule) : base(group)
        {
            Specialization = schedule.Specializations[group.Specialization.GetHashCode()];
            YearOfStudy = schedule.YearsOfStudy[group.YearOfStudy.GetHashCode()];
        }

        public Group ToNonSerializable(ScheduleAndDicts scheduleAndDicts)
        {
            return new Group
            {
                Name = Name,
                Specialization = scheduleAndDicts.Specializations[Specialization.ID],
                YearOfStudy = scheduleAndDicts.YearsOfStudy[YearOfStudy.ID]
            };
        }
    }

    [Serializable]
    public class sClassRecord : sHavingId
    {
        public sSubject Subject;
        public sLecturer Lecturer;
        public sClassroom Classroom;

        public sClassRecord(ClassRecord classRecord, sClassesSchedule schedule) : base(classRecord)
        {
            Subject = classRecord.Subject == null ? null : schedule.Subjects[classRecord.Subject.GetHashCode()];
            Lecturer = classRecord.Lecturer == null ? null : schedule.Lecturers[classRecord.Lecturer.GetHashCode()];
            Classroom = classRecord.Classroom == null ? null : schedule.Classrooms[classRecord.Classroom.GetHashCode()];
        }

        public ClassRecord ToNoneSerializable(ScheduleAndDicts scheduleAndDicts)
        {
            return new ClassRecord
            {
                Classroom = Classroom == null ? null : scheduleAndDicts.Classrooms[Classroom.ID],
                Lecturer = Lecturer == null ? null : scheduleAndDicts.Lecturers[Lecturer.ID],
                Subject = Subject == null ? null : scheduleAndDicts.Subjects[Subject.ID]
            };
        }
    }

    [Serializable]
    public class sClassesTable : sHavingId
    {
        public sYearOfStudy YearOfStudy;
        public sClassRecord[][] Table;

        public sClassesTable(ClassesTable table, sClassesSchedule schedule) : base(table.YearOfStudy)
        {  
            CopyYearOfStudy(table, schedule);
            CopyTable(table, schedule);
        }

        private void CopyYearOfStudy(ClassesTable table, sClassesSchedule schedule)
        {
            YearOfStudy = schedule.YearsOfStudy[table.YearOfStudy.GetHashCode()];
        }

        private void CopyTable(ClassesTable table, sClassesSchedule schedule)
        {
            var oldTable = table.Table;
            Table = new sClassRecord[oldTable.Length][];
            for (int i = 0; i < oldTable.Length; ++i)
            {
                Table[i] = new sClassRecord[oldTable[i].Length];
                for (int j = 0; j < oldTable[i].Length; ++j)
                    if (oldTable[i][j] != null)
                        Table[i][j] = new sClassRecord(oldTable[i][j], schedule);
            }       
        }

        public ClassesTable ToNonSerializable(ScheduleAndDicts scheduleAndDicts)
        {
            var yearOfStudy = scheduleAndDicts.YearsOfStudy[YearOfStudy.ID];
            var table = new ClassesTable(scheduleAndDicts.Schedule, yearOfStudy);

            for (int i = 0; i < Table.Length; ++i)
                for (int j = 0; j < Table[i].Length; ++j)
                    if (Table[i][j] != null)
                        table.Table[i][j] = Table[i][j].ToNoneSerializable(scheduleAndDicts);

            return table;
        }
    }

    public class ScheduleAndDicts
    {
        public ClassTime[] TimeLine;
        public Dictionary<int, Group> Groups;
        public Dictionary<int, Lecturer> Lecturers;
        public Dictionary<int, Classroom> Classrooms;
        public Dictionary<int, Subject> Subjects;
        public Dictionary<int, Specialization> Specializations;
        public Dictionary<int, YearOfStudy> YearsOfStudy;
        public ClassesTable[] Tables;
        public ClassesSchedule Schedule;
    }
    
    [Serializable]
    public class sClassesSchedule : sHavingId
    {
        public sClassTime[] TimeLine;
        public Dictionary<int, sGroup> Groups;
        public Dictionary<int, sLecturer> Lecturers;
        public Dictionary<int, sClassroom> Classrooms;
        public Dictionary<int, sSubject> Subjects;
        public Dictionary<int, sSpecialization> Specializations;
        public Dictionary<int, sYearOfStudy> YearsOfStudy;
        public sClassesTable[] Tables;

        # region Create

        public static sClassesSchedule Create(ClassesSchedule schedule)
        {
            return new sClassesSchedule(schedule);
        }

        private sClassesSchedule(ClassesSchedule schedule) : base(schedule.GetHashCode())
        {
            CopySubjects(schedule);
            CopySpecializations(schedule);
            CopyYearsOfStudy(schedule);
            CopyLecturers(schedule);
            CopyClassrooms(schedule);
            CopyTimeLine(schedule);
            CopyGroups(schedule);
            CopyTables(schedule);
        }

        private void CopySubjects(ClassesSchedule schedule)
        {
            Subjects = new Dictionary<int, sSubject>(schedule.Subjects.Count);
            foreach (var subject in schedule.Subjects)
                Subjects.Add(subject.GetHashCode(), new sSubject(subject));
        }

        private void CopySpecializations(ClassesSchedule schedule)
        {
            Specializations = new Dictionary<int, sSpecialization>(schedule.Specializations.Count);
            foreach (var specialization in schedule.Specializations)
                Specializations.Add(specialization.GetHashCode(), new sSpecialization(specialization));
        }

        private void CopyYearsOfStudy(ClassesSchedule schedule)
        {
            YearsOfStudy = new Dictionary<int, sYearOfStudy>(schedule.YearsOfStudy.Count);
            foreach (var yearOfStudy in schedule.YearsOfStudy)
                YearsOfStudy.Add(yearOfStudy.GetHashCode(), new sYearOfStudy(yearOfStudy));
        }

        private void CopyLecturers(ClassesSchedule schedule)
        {
            Lecturers = new Dictionary<int, sLecturer>(schedule.Lecturers.Count);
            foreach (var lecturer in schedule.Lecturers)
                Lecturers.Add(lecturer.GetHashCode(), new sLecturer(lecturer));
        }

        private void CopyClassrooms(ClassesSchedule schedule)
        {
            Classrooms = new Dictionary<int, sClassroom>(schedule.Classrooms.Count);
            foreach (var classroom in schedule.Classrooms)
                Classrooms.Add(classroom.GetHashCode(), new sClassroom(classroom));
        }

        private void CopyGroups(ClassesSchedule schedule)
        {
            Groups = new Dictionary<int, sGroup>(schedule.Groups.Count);
            foreach (var group in schedule.Groups)
                Groups.Add(group.GetHashCode(), new sGroup(group, this));
        }

        private void CopyTimeLine(ClassesSchedule schedule)
        {
            var oldTimeLine = schedule.TimeLine;
            TimeLine = new sClassTime[oldTimeLine.Count];
            for (int i = 0; i < oldTimeLine.Count; ++i)
                TimeLine[i] = new sClassTime(oldTimeLine[i]);
        }

        private void CopyTables(ClassesSchedule schedule)
        {
            var oldTables = schedule.Tables;
            Tables = new sClassesTable[oldTables.Count];
            for (int i = 0; i < oldTables.Count; ++i)
                Tables[i] = new sClassesTable(oldTables[i], this);
        }

        # endregion

        # region ToClassesSchedule

        public ClassesSchedule ToClassesSchedule()
        {
            var scheduleAndDicts = new ScheduleAndDicts();

            scheduleAndDicts.Schedule = new ClassesSchedule();
            NonSerializableTimeLine(scheduleAndDicts);
            NonSerializableLectures(scheduleAndDicts);
            NonSerializableClassrooms(scheduleAndDicts);
            NonSerializableSubjects(scheduleAndDicts);
            NonSerializableSpecializations(scheduleAndDicts);
            NonSerializableYearsOfStudy(scheduleAndDicts);
            NonSerializableGroups(scheduleAndDicts);
            NonSerializableTables(scheduleAndDicts);

            return scheduleAndDicts.Schedule;
        }

        private void NonSerializableTimeLine(ScheduleAndDicts scheduleAndDicts)
        {
            var length = TimeLine.Length;
            scheduleAndDicts.TimeLine = new ClassTime[length];
            for (int i = 0; i < length; ++i)
            {
                var time = TimeLine[i].ToNonSerializable();
                scheduleAndDicts.TimeLine[i] = time;
                scheduleAndDicts.Schedule.TimeLine.Add(time);
            }
        }

        private void NonSerializableLectures(ScheduleAndDicts scheduleAndDicts)
        {
            scheduleAndDicts.Lecturers = new Dictionary<int, Lecturer>(Lecturers.Count);
            foreach (var pair in Lecturers)
            {
                var lecturer = pair.Value.ToNonSerializable();
                scheduleAndDicts.Lecturers.Add(pair.Key, lecturer);
                scheduleAndDicts.Schedule.Lecturers.Add(lecturer);
            }
        }

        private void NonSerializableClassrooms(ScheduleAndDicts scheduleAndDicts)
        {
            scheduleAndDicts.Classrooms = new Dictionary<int, Classroom>(Classrooms.Count);
            foreach (var pair in Classrooms)
            {
                var classroom = pair.Value.ToNonSerializable();
                scheduleAndDicts.Classrooms.Add(pair.Key, classroom);
                scheduleAndDicts.Schedule.Classrooms.Add(classroom);
            }
        }

        private void NonSerializableSubjects(ScheduleAndDicts scheduleAndDicts)
        {
            scheduleAndDicts.Subjects = new Dictionary<int, Subject>(Subjects.Count);
            foreach (var pair in Subjects)
            {
                var subject = pair.Value.ToNonSerializable();
                scheduleAndDicts.Subjects.Add(pair.Key, subject);
                scheduleAndDicts.Schedule.Subjects.Add(subject);
            }
        }

        private void NonSerializableSpecializations(ScheduleAndDicts scheduleAndDicts)
        {
            scheduleAndDicts.Specializations = new Dictionary<int, Specialization>(Specializations.Count);
            foreach (var pair in Specializations)
            {
                var specialization = pair.Value.ToNonSerializable();
                scheduleAndDicts.Specializations.Add(pair.Key, specialization);
                scheduleAndDicts.Schedule.Specializations.Add(specialization);
            }
        }

        private void NonSerializableYearsOfStudy(ScheduleAndDicts scheduleAndDicts)
        {
            scheduleAndDicts.YearsOfStudy = new Dictionary<int, YearOfStudy>(YearsOfStudy.Count);
            foreach (var pair in YearsOfStudy)
            {
                var yearOfStudy = pair.Value.ToNonSerializable();
                scheduleAndDicts.YearsOfStudy.Add(pair.Key, yearOfStudy);
                scheduleAndDicts.Schedule.YearsOfStudy.Add(yearOfStudy);
            }
        }

        private void NonSerializableGroups(ScheduleAndDicts scheduleAndDicts)
        {
            scheduleAndDicts.Groups = new Dictionary<int, Group>(Groups.Count);
            foreach (var pair in Groups)
            {
                var group = pair.Value.ToNonSerializable(scheduleAndDicts);
                scheduleAndDicts.Groups.Add(pair.Key, group);
                scheduleAndDicts.Schedule.Groups.Add(group);
            }
        }

        private void NonSerializableTables(ScheduleAndDicts scheduleAndDicts)
        {
            var length = Tables.Length;
            scheduleAndDicts.Tables = new ClassesTable[length];
            for (int i = 0; i < length; ++i)
            {
                var table = Tables[i].ToNonSerializable(scheduleAndDicts);
                scheduleAndDicts.Tables[i] = table;
                scheduleAndDicts.Schedule.Tables.Add(table);
            }
        }

        #endregion

        #region Serialize

        public static void Save(sClassesSchedule schedule, string path)
        {
            var streamSave = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var serializer = new BinaryFormatter();

            serializer.Serialize(streamSave, schedule);
            streamSave.Close();
        }

        public static sClassesSchedule Load(string path)
        {
            var streamLoad = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var deserializer = new BinaryFormatter();

            var schedule = (sClassesSchedule)deserializer.Deserialize(streamLoad);
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
        public static void Save(ClassesSchedule schedule, string path)
        {
            sClassesSchedule.Create(schedule).Save(path);
        }

        public static ClassesSchedule Load(string path)
        {
            return sClassesSchedule.Load(path).ToClassesSchedule();
        }
    }
}