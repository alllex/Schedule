using System.Collections.Generic;
using System.Linq;

namespace ScheduleData
{
    public abstract class ClassesTable<TSubject>
    {

        public List<TSubject> Subjects;
        public Dictionary<TSubject, int> SubjectIndexes = new Dictionary<TSubject, int>();
        public Dictionary<ClassTime, int> TimeIndexes = new Dictionary<ClassTime, int>();

        protected Schedule Schedule;
        //protected ClassRecord[][] Table;
        protected Dictionary<TSubject, Dictionary<ClassTime, ClassRecord>> TableDictionary = new Dictionary<TSubject, Dictionary<ClassTime, ClassRecord>>(); 

        protected ClassesTable(Schedule schedule)
        {
            Schedule = schedule;
            SetTime();
        }

        private void SetTime()
        {
            TimeIndexes.Clear();
            for (int i = 0; i < Schedule.TimeLine.Count(); i++)
                TimeIndexes.Add(Schedule.TimeLine[i], i);
        }

        protected void SetSubjectIndexes()
        {
            SubjectIndexes.Clear();
            for (int i = 0; i < Subjects.Count(); i++)
                SubjectIndexes.Add(Subjects[i], i);
        }

//        protected void CreateTable()
//        {
//            var rowsCount = TimeCardsCount();
//            var colsCount = SubjectsCount();
//            Table = new ClassRecord[rowsCount][];
//            for (int i = 0; i < rowsCount; i++)
//            {
//                Table[i] = new ClassRecord[colsCount];
//            }
//        }

        protected void CreateTableDictionary()
        {
            foreach (var subject in Subjects)
            {
                TableDictionary[subject] = new Dictionary<ClassTime, ClassRecord>();
            }
        }

        public int TimeCardsCount()
        {
            return TimeIndexes.Count;
        }

        public int SubjectsCount()
        {
            return SubjectIndexes.Count;
        }

        public ClassRecord GetClass(int timeIndex, int subjectIndex)
        {
            if (!AreIndexesCorrect(timeIndex, subjectIndex)) return null;
            var subject = Subjects[subjectIndex];
            var time = Schedule.TimeLine[timeIndex];
            return TableDictionary[subject].ContainsKey(time) ? TableDictionary[subject][time] : null;
        }

        public ClassRecord SetClass(int timeIndex, int subjectIndex, ClassRecord classRecord)
        {
            if (!AreIndexesCorrect(timeIndex, subjectIndex)) return null;
            var subject = Subjects[subjectIndex];
            var time = Schedule.TimeLine[timeIndex];
            if (TableDictionary[subject].ContainsKey(time))
            {
                Schedule.RemoveClass(TableDictionary[subject][time]);
            }
            Schedule.AddClass(classRecord);
            TableDictionary[subject][time] = classRecord;
            return classRecord;
        }

        public void RemoveClass(int timeIndex, int subjectIndex)
        {
            if (!AreIndexesCorrect(timeIndex, subjectIndex)) return;
            var subject = Subjects[subjectIndex];
            var time = Schedule.TimeLine[timeIndex];
            if (TableDictionary[subject].ContainsKey(time))
            {
                Schedule.RemoveClass(TableDictionary[subject][time]);
                TableDictionary[subject].Remove(time);
            }
        }

        private bool AreIndexesCorrect(int timeIndex, int subjectIndex)
        {
            return timeIndex >= 0 && timeIndex < TimeCardsCount() && subjectIndex >= 0 && subjectIndex < SubjectsCount();
        }
    }
}