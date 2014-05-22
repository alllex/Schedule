using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ScheduleData
{

    public class ClassRecord : HavingId
    {
        protected bool Equals(ClassRecord other)
        {
            return Equals(_subject, other._subject) && Equals(_lecturer, other._lecturer) && Equals(_classroom, other._classroom);
        }

        #region Subject

        private Subject _subject;

        public Subject Subject
        {
            get { return _subject; }
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    RaisePropertyChanged(() => Subject);
                }
            }
        }

        #endregion

        #region Lecturer

        private Lecturer _lecturer;

        public Lecturer Lecturer
        {
            get { return _lecturer; }
            set
            {
                if (_lecturer != value)
                {
                    _lecturer = value;
                    RaisePropertyChanged(() => Lecturer);
                }
            }
        }

        #endregion

        #region Classroom

        private Classroom _classroom;

        public Classroom Classroom
        {
            get { return _classroom; }
            set
            {
                if (_classroom != value)
                {
                    _classroom = value;
                    RaisePropertyChanged(() => Classroom);
                }
            }
        }

        #endregion

        public static void Copy(ClassRecord from, ClassRecord to)
        {
            if (from == null || to == null) return;
            to.Subject = from.Subject;
            to.Lecturer = from.Lecturer;
            to.Classroom = from.Classroom;
        }
    }

    public class ClassesSchedule : HavingId
    {

        #region Properties

        #region TimeLine

        private ObservableCollection<ClassTime> _timeLine = new ObservableCollection<ClassTime>();

        public ObservableCollection<ClassTime> TimeLine
        {
            get { return _timeLine; }
            set
            {
                if (_timeLine != value)
                {
                    _timeLine = value;
                    RaisePropertyChanged(() => TimeLine);
                }
            }
        }

        #endregion

        #region Groups

        private ObservableCollection<Group> _groups = new ObservableCollection<Group>();

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups != value)
                {
                    _groups = value;
                    RaisePropertyChanged(() => Groups);
                }
            }
        }

        #endregion

        #region Lecturers

        private ObservableCollection<Lecturer> _lecturers = new ObservableCollection<Lecturer>();

        public ObservableCollection<Lecturer> Lecturers
        {
            get { return _lecturers; }
            set
            {
                if (_lecturers != value)
                {
                    _lecturers = value;
                    RaisePropertyChanged(() => Lecturers);
                }
            }
        }

        #endregion

        #region Classrooms

        private ObservableCollection<Classroom> _classrooms = new ObservableCollection<Classroom>();
        public ObservableCollection<Classroom> Classrooms
        {
            get { return _classrooms; }
            set
            {
                if (_classrooms != value)
                {
                    _classrooms = value;
                    RaisePropertyChanged(() => Classrooms);
                }
            }
        }

        #endregion

        #region Subjects

        private ObservableCollection<Subject> _subjects = new ObservableCollection<Subject>();

        public ObservableCollection<Subject> Subjects
        {
            get { return _subjects; }
            set
            {
                if (_subjects != value)
                {
                    _subjects = value;
                    RaisePropertyChanged(() => Subjects);
                }
            }
        }

        #endregion

        #region Specializations

        private ObservableCollection<Specialization> _specializations = new ObservableCollection<Specialization>();

        public ObservableCollection<Specialization> Specializations
        {
            get { return _specializations; }
            set
            {
                if (_specializations != value)
                {
                    _specializations = value;
                    RaisePropertyChanged(() => Specializations);
                }
            }
        }

        #endregion

        #region YearsOfStudy

        private ObservableCollection<YearOfStudy> _yearsOfStudy = new ObservableCollection<YearOfStudy>();

        public ObservableCollection<YearOfStudy> YearsOfStudy
        {
            get { return _yearsOfStudy; }
            set
            {
                if (_yearsOfStudy != value)
                {
                    _yearsOfStudy = value;
                    RaisePropertyChanged(() => YearsOfStudy);
                }
            }
        }

        #endregion

        #region Tables

        private ObservableCollection<ClassesTable> _tables = new ObservableCollection<ClassesTable>();

        public ObservableCollection<ClassesTable> Tables
        {
            get { return _tables; }
            set
            {
                if (_tables != value)
                {
                    _tables = value;
                    RaisePropertyChanged(() => Tables);
                }
            }
        }

        #endregion

        #endregion

        #region Adders

        public void AddYearOfStudy(YearOfStudy year)
        {
            YearsOfStudy.Add(year);
            Tables.Add(new ClassesTable(this, year));
        }

        public void AddSpecialization(Specialization specialization)
        {
            Specializations.Add(specialization);
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        public void AddClassroom(Classroom classroom)
        {
            Classrooms.Add(classroom);
        }

        public void AddLecturer(Lecturer lecturer)
        {
            Lecturers.Add(lecturer);
        }

        public void AddGroup(Group group)
        {
            Groups.Add(group);
            if (group == null || group.YearOfStudy == null) return;
            var table = Tables.First(t => t.YearOfStudy == group.YearOfStudy);
            if (table == null) return;
            table.AddGroup(group);
        }

        #endregion
        
        #region Removers

        public void RemoveYearOfStudy(YearOfStudy year)
        {
            YearsOfStudy.Remove(year);
            Tables.Remove((from t in Tables where t.YearOfStudy == year select t).First());
            foreach (var @group in Groups)
            {
                if (group.YearOfStudy == year)
                {
                    group.YearOfStudy = null;
                }
            }
        }

        public void RemoveSpecialization(Specialization specialization)
        {
            Specializations.Remove(specialization);
            foreach (var @group in Groups)
            {
                if (group.Specialization == specialization)
                {
                    group.Specialization = null;
                }
            }
        }

        public void RemoveSubject(Subject subject)
        {
            Subjects.Remove(subject);
            foreach (var classesTable in Tables)
            {
                for (int i = 0; i < classesTable.RowsCount(); i++)
                {
                    for (int j = 0; j < classesTable.ColumnsCount(); j++)
                    {
                        if (classesTable.Table[i][j].Subject == subject)
                        {
                            classesTable.Table[i][j].Subject = null;
                        }
                    }
                }
            }
        }

        public void RemoveClassroom(Classroom classroom)
        {
            Classrooms.Remove(classroom);
            foreach (var classesTable in Tables)
            {
                for (int i = 0; i < classesTable.RowsCount(); i++)
                {
                    for (int j = 0; j < classesTable.ColumnsCount(); j++)
                    {
                        if (classesTable.Table[i][j].Classroom == classroom)
                        {
                            classesTable.Table[i][j].Classroom = null;
                        }
                    }
                }
            }
        }

        public void RemoveLecturer(Lecturer lecturer)
        {
            Lecturers.Remove(lecturer);
            foreach (var classesTable in Tables)
            {
                for (int i = 0; i < classesTable.RowsCount(); i++)
                {
                    for (int j = 0; j < classesTable.ColumnsCount(); j++)
                    {
                        if (classesTable.Table[i][j].Lecturer == lecturer)
                        {
                            classesTable.Table[i][j].Lecturer = null;
                        }
                    }
                }
            }
        }

        public void RemoveGroup(Group group)
        {
            Groups.Remove(group);
            if (group == null || group.YearOfStudy == null) return;
            var table = Tables.First(t => t.YearOfStudy == group.YearOfStudy);
            if (table == null) return;
            table.RemoveGroup(group);
        }

        #endregion

        #region Ctor

        public ClassesSchedule()
        {
            Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.NewItems)
                    {
                        //Removed items
                        var group = item as Group;
                        if (group == null) continue;
                        group.PropertyChanged -= GroupOnPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        //Added items
                        var group = item as Group;
                        if (group == null) continue;
                        group.PropertyChanged += GroupOnPropertyChanged;
                    }
                    break;
            }

        }

        private void GroupOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var group = sender as Group;
            if (group == null) return;
            if (e.PropertyName == "YearOfStudy")
            {
                ClassesTable tableForAdd = null;
                ClassesTable tableForRemove = null;
                
                foreach (ClassesTable t in Tables)
                    if (t.YearOfStudy == @group.YearOfStudy)
                        tableForAdd = t;
                    else if (t.GroupIndexes.ContainsKey(@group))
                        tableForRemove = t;

                if (tableForRemove != null)
                {
                    var classes = tableForRemove.AllClassesOfGroup(group);
                    tableForRemove.RemoveGroup(group);
                    tableForAdd.AddGroup(group, classes);

                }
                else
                    tableForAdd.AddGroup(group);
            }
            else if (e.PropertyName == "Specialization")
            {
                if (group.YearOfStudy != null)
                {
                    ClassesTable tableForMove = null;
                    foreach (ClassesTable t in Tables)
                        if (t.YearOfStudy == @group.YearOfStudy)
                            tableForMove = t;

                    var classes = tableForMove.AllClassesOfGroup(group);
                    tableForMove.RemoveGroup(group);
                    tableForMove.AddGroup(group, classes);
                }
            }
        }

        public IEnumerable<Group> CorrectGroups()
        {
            return from g in Groups where g != null && g.YearOfStudy != null && g.Specialization != null select g;
        }

        #endregion

        #region Public

        public void CreateNewTables()
        {
            Tables.Clear();
            foreach (var yearOfStudy in YearsOfStudy)
            {
                Tables.Add(new ClassesTable(this, yearOfStudy));
            }
        }

        public bool HasGroups(YearOfStudy year)
        {
            return Groups.Any(g => g.YearOfStudy == year);
        }

        public void InitStdTimeLine()
        {
            var wds = Enum.GetValues(typeof(Weekdays));
            foreach (var weekday in wds)
            {
                for (int i = 0; i < ClassTime.ClassIntervals.Count(); i++)
                {
                    TimeLine.Add(new ClassTime { Day = (Weekdays)weekday, Number = i });
                }
            }
        }

        #endregion

        public ClassesTable GetClassesTable(YearOfStudy year)
        {
            var index = YearsOfStudy.IndexOf(year);
            return Tables[index];
        }

        public List<FullClassRecord> ToList()
        {
            var classes = new List<FullClassRecord>();
            
            foreach (var table in Tables)
                for (int i = 0; i < TimeLine.Count; ++i)
                    for (int j = 0; j < table.Groups.Count(); ++j)
                        if (table.Table[i][j] != null)
                            classes.Add(new FullClassRecord(TimeLine[i], table.Groups[j], table.Table[i][j]));

            return classes;
        }

        # region Save/Load

        public static void Save(ClassesSchedule schedule, string path)
        {
            SaveLoadSchedule.Save(schedule, path);
        }

        public static ClassesSchedule Load(string path)
        {
            return SaveLoadSchedule.Load(path);
        }

        public void Save(string path)
        {
            SaveLoadSchedule.Save(this, path);
        }

        # endregion
    }
}