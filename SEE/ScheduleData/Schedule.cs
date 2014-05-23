using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleData
{

//    public class ClassRecord : HavingId
//    {
//        protected bool Equals(ClassRecord other)
//        {
//            return Equals(_subject, other._subject) && Equals(_lecturer, other._lecturer) && Equals(_classroom, other._classroom);
//        }
//
//        #region Subject
//
//        private Subject _subject;
//
//        public Subject Subject
//        {
//            get { return _subject; }
//            set
//            {
//                if (_subject != value)
//                {
//                    _subject = value;
//                    RaisePropertyChanged(() => Subject);
//                }
//            }
//        }
//
//        #endregion
//
//        #region Lecturer
//
//        private Lecturer _lecturer;
//
//        public Lecturer Lecturer
//        {
//            get { return _lecturer; }
//            set
//            {
//                if (_lecturer != value)
//                {
//                    _lecturer = value;
//                    RaisePropertyChanged(() => Lecturer);
//                }
//            }
//        }
//
//        #endregion
//
//        #region Classroom
//
//        private Classroom _classroom;
//
//        public Classroom Classroom
//        {
//            get { return _classroom; }
//            set
//            {
//                if (_classroom != value)
//                {
//                    _classroom = value;
//                    RaisePropertyChanged(() => Classroom);
//                }
//            }
//        }
//
//        #endregion
//
//        public static void Copy(ClassRecord from, ClassRecord to)
//        {
//            if (from == null || to == null) return;
//            to.Subject = from.Subject;
//            to.Lecturer = from.Lecturer;
//            to.Classroom = from.Classroom;
//        }
//    }

    public class Schedule : HavingId
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

        #region ClassRecords

        private ObservableCollection<ClassRecord> _classRecords = new ObservableCollection<ClassRecord>();

        public ObservableCollection<ClassRecord> ClassRecords
        {
            get { return _classRecords; }
            set
            {
                if (_classRecords != value)
                {
                    _classRecords = value;
                    RaisePropertyChanged(() => ClassRecords);
                }
            }
        }

        #endregion

        #endregion

        #region Adders

        public void AddYearOfStudy(YearOfStudy year)
        {
            YearsOfStudy.Add(year);
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
        }

        #endregion
        
        #region Removers

        public void RemoveYearOfStudy(YearOfStudy year)
        {
            YearsOfStudy.Remove(year);
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
            foreach (var classRecord in ClassRecords)
            {
                if (classRecord.Subject == subject)
                {
                    classRecord.Subject = null;
                }
            }
        }

        public void RemoveClassroom(Classroom classroom)
        {
            Classrooms.Remove(classroom);
            foreach (var classRecord in ClassRecords)
            {
                if (classRecord.Classroom == classroom)
                {
                    classRecord.Classroom = null;
                }
            }
        }

        public void RemoveLecturer(Lecturer lecturer)
        {
            Lecturers.Remove(lecturer);
            foreach (var classRecord in ClassRecords)
            {
                if (classRecord.Lecturer == lecturer)
                {
                    classRecord.Lecturer = null;
                }
            }
        }

        public void RemoveGroup(Group group)
        {
            Groups.Remove(group);
            foreach (var classRecord in ClassRecords)
            {
                if (classRecord.Group == group)
                {
                    classRecord.Group = null;
                }
            }

        }

        #endregion

        #region Ctor

        public Schedule()
        {
//            Groups.CollectionChanged += GroupsOnCollectionChanged;
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

        public void InitByOne()
        {
            var year = new YearOfStudy { Name = "Новый курс" };
            var spec = new Specialization { Name = "Специальность" };
            var group = new Group { Name = "Группа", YearOfStudy = year, Specialization = spec };
            AddYearOfStudy(year);
            AddSpecialization(spec);
            AddGroup(group);
        }

//        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//        {
//            if (e.NewItems == null) return;
//            switch (e.Action)
//            {
//                case NotifyCollectionChangedAction.Remove:
//                    foreach (var item in e.NewItems)
//                    {
//                        //Removed items
//                        var group = item as Group;
//                        if (group == null) continue;
//                        group.PropertyChanged -= GroupOnPropertyChanged;
//                    }
//                    break;
//                case NotifyCollectionChangedAction.Add:
//                    foreach (var item in e.NewItems)
//                    {
//                        //Added items
//                        var group = item as Group;
//                        if (group == null) continue;
//                        group.PropertyChanged += GroupOnPropertyChanged;
//                    }
//                    break;
//            }
//
//        }
//
//        private void GroupOnPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            var group = sender as Group;
//            if (group == null) return;
//            if (e.PropertyName == "YearOfStudy")
//            {
//                ClassesTable tableForAdd = null;
//                ClassesTable tableForRemove = null;
//                
//                foreach (ClassesTable t in Tables)
//                    if (t.YearOfStudy == @group.YearOfStudy)
//                        tableForAdd = t;
//                    else if (t.GroupIndexes.ContainsKey(@group))
//                        tableForRemove = t;
//
//                if (tableForRemove != null)
//                {
//                    var classes = tableForRemove.AllClassesOfGroup(group);
//                    tableForRemove.RemoveGroup(group);
//                    tableForAdd.AddGroup(group, classes);
//
//                }
//                else
//                    tableForAdd.AddGroup(group);
//            }
//            else if (e.PropertyName == "Specialization")
//            {
//                if (group.YearOfStudy != null)
//                {
//                    ClassesTable tableForMove = null;
//                    foreach (ClassesTable t in Tables)
//                        if (t.YearOfStudy == @group.YearOfStudy)
//                            tableForMove = t;
//
//                    var classes = tableForMove.AllClassesOfGroup(group);
//                    tableForMove.RemoveGroup(group);
//                    tableForMove.AddGroup(group, classes);
//                }
//            }
//        }

        public IEnumerable<Group> CorrectGroups()
        {
            return from g in Groups where g != null && g.YearOfStudy != null && g.Specialization != null select g;
        }

        #endregion

        #region Public

        public List<Group> GroupsBySpecialization()
        {
            var groups = new List<Group>();
            foreach (var spec in Specializations)
            {
                groups.AddRange(from g in Groups where g.Specialization == spec select g);
            }
            return groups;
        }

//        public void CreateNewTables()
//        {
//            Tables.Clear();
//            foreach (var yearOfStudy in YearsOfStudy)
//            {
//                Tables.Add(new ClassesTable(this, yearOfStudy));
//            }
//        }

        public bool HasGroups(YearOfStudy year)
        {
            return Groups.Any(g => g.YearOfStudy == year);
        }

        #endregion

        public List<FullClassRecord> ToList()
        {
            return (from c in ClassRecords select (new FullClassRecord(c))).ToList();
        }
    }
}