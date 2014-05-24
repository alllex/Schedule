using System.Collections.Generic;
using System.Linq;
using ScheduleData;

namespace Editor.Models
{
    public abstract class ClassesTable<TSubject>
    {

        public List<TSubject> Subjects;
        public Dictionary<TSubject, int> SubjectIndexes = new Dictionary<TSubject, int>();
        public Dictionary<ClassTime, int> TimeIndexes = new Dictionary<ClassTime, int>();

        protected Schedule Schedule;
        protected ClassRecord[][] Table;

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

        protected void CreateTable()
        {
            var rowsCount = TimeCardsCount();
            var colsCount = SubjectsCount();
            Table = new ClassRecord[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                Table[i] = new ClassRecord[colsCount];
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
            if (timeIndex < 0 || timeIndex >= TimeCardsCount() || subjectIndex < 0 || subjectIndex >= SubjectsCount())
                return null;
            return Table[timeIndex][subjectIndex];
        }

        public ClassRecord SetClass(int timeIndex, int subjectIndex, ClassRecord classRecord)
        {
            if (!AreIndexesCorrect(timeIndex, subjectIndex)) return null;
            if (Table[timeIndex][subjectIndex] != null)
            {
                Schedule.RemoveClass(Table[timeIndex][subjectIndex]);
            }
            Table[timeIndex][subjectIndex] = classRecord;
            Schedule.AddClass(classRecord);
            return classRecord;
        }

        public void RemoveClass(int timeIndex, int subjectIndex)
        {
            if (!AreIndexesCorrect(timeIndex, subjectIndex)) return;
            if (Table[timeIndex][subjectIndex] != null)
            {
                Schedule.RemoveClass(Table[timeIndex][subjectIndex]);
            }
            Table[timeIndex][subjectIndex] = null;
        }

        private bool AreIndexesCorrect(int timeIndex, int subjectIndex)
        {
            return timeIndex >= 0 && timeIndex < TimeCardsCount() && subjectIndex >= 0 && subjectIndex < SubjectsCount();
        }
    }
}