using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Editor.Models;

namespace Editor.Models.SerializableModels
{

    [Serializable]
    abstract public class sHavingId
    {
        public int ID;

        public sHavingId(int id)
        {
            ID = id;
        }

        public sHavingId(HavingId havingId)
        {
            ID = havingId.GetHashCode();
        }
    }    
    
    [Serializable]
    abstract public class sHavingName : sHavingId
    {
        public string Name;

        public sHavingName(int id, string name) : base(id)
        {
            Name = name;
        }

        public sHavingName(HavingName havingName) : base(havingName)
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
    }

    [Serializable]
    public class sSubject : sHavingName
    {
        public sSubject(Subject subject) : base(subject)
        {
        }
    }

    [Serializable]
    public class sSpecialization : sHavingName
    {
        public sSpecialization(Specialization specialization) : base(specialization)
        {
        }
    }

    [Serializable]
    public class sYearOfStudy : sHavingName
    {
        public sYearOfStudy(YearOfStudy yearOfStudy) : base(yearOfStudy)
        {
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
    }

    [Serializable]
    public class sClassroom : sHavingName
    {
        public string Address;

        public sClassroom(Classroom classroom) : base(classroom)
        {
            Address = classroom.Address;
        }
    }

    [Serializable]
    public class sGroup : sHavingName
    {
        public sSpecialization Specialization;
        public sYearOfStudy YearOfStudy;

        public sGroup(Group group, sClassesSchedule schedule)
            : base(group)
        {
            Specialization = schedule.Specializations[group.Specialization.GetHashCode()];
            YearOfStudy = schedule.YearsOfStudy[group.YearOfStudy.GetHashCode()];
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
            Subject = schedule.Subjects[classRecord.Subject.GetHashCode()];
            Lecturer = schedule.Lecturers[classRecord.Lecturer.GetHashCode()];
            Classroom = schedule.Classrooms[classRecord.Classroom.GetHashCode()];
        }
    }

    [Serializable]
    public class sClassesTable : sHavingId
    {
        public sYearOfStudy YearOfStudy;
        public sGroup[] Groups;
        public sClassRecord[][] Table;

        public sClassesTable(ClassesTable table, sClassesSchedule schedule) : base(table.YearOfStudy)
        {  
            CopyYearOfStudy(table);
            YearOfStudy = new sYearOfStudy(table.YearOfStudy);
            CopyGroups(table, schedule);
            CopyTable(table, schedule);
        }

        private void CopyYearOfStudy(ClassesTable table)
        {
            YearOfStudy = new sYearOfStudy(table.YearOfStudy);
        }

        private void CopyGroups(ClassesTable table, sClassesSchedule schedule)
        {
            var oldGroups = table.Groups;
            Groups = new sGroup[oldGroups.Length];
            for (int i = 0; i < oldGroups.Length; ++i)
                Groups[i] = schedule.Groups[oldGroups[i].GetHashCode()];
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
        public Dictionary<int, sClassesTable> Tables;

        # region Copy

        public sClassesSchedule(ClassesSchedule schedule) : base(schedule.GetHashCode())
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
            Tables = new Dictionary<int, sClassesTable>(schedule.Tables.Count);
            foreach (var table in schedule.Tables)
                Tables.Add(table.YearOfStudy.GetHashCode(), new sClassesTable(table, this));
        }

        # endregion

        # region Serialize

        public static void Save(sClassesSchedule schedule, string path)
        {
            FileStream streamSave = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(streamSave, schedule);
            streamSave.Close();
        }

        public static sClassesSchedule Load(string path)
        {
            FileStream streamLoad = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter deserializer = new BinaryFormatter();

            var schedule = (sClassesSchedule)deserializer.Deserialize(streamLoad);
            streamLoad.Close();
            return schedule;
        }

        public void Save(string path)
        {
            sClassesSchedule.Save(this, path);
        }

        # endregion
    }
}
