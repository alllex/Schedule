using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleData.DataMining
{

    public class ClassesPerWeekday
    {
        public Weekdays Weekday { get; set; }   
        public int Count { get; set; }
    }

    abstract public class Statistic<TSubject>
    {
        // ReSharper disable once StaticFieldInGenericType
        private static readonly int CountWeekdays = Enum.GetValues(typeof(Weekdays)).Length ;

        protected Schedule Schedule;
        protected IEnumerable<FullClassRecord> Classes;

        public TSubject Subject;
        public int CountOfClassesPerWeek { get; protected set; }
        public float AverageCountOfClassesPerDay { get; protected set; }
        public List<ClassesPerWeekday> CountOfClassesPerWeekday { get; protected set; }

        protected Statistic(Schedule schedule, TSubject subject, Func<FullClassRecord, TSubject> getField)
        {
            Subject = subject;
            Schedule = schedule;
            SetClasses(getField);
            SetCounts();
        }

        protected void SetClasses(Func<FullClassRecord, TSubject> getField) 
        {
            Classes = Schedule.ToList().Where(f => { var field = getField(f); 
                                                     return field == null ? false : field.Equals(Subject); 
                                                   });
        }


        protected void SetCounts()
        {
            SetCountOfClassesPerWeek();
            SetCountsOfClassesPerAllWeekdays();
            SetAverageCountOfClassesPerDay();
        }

        protected void SetCountOfClassesPerWeek()
        {
            CountOfClassesPerWeek = Classes.Count();
        }

        protected void SetCountsOfClassesPerAllWeekdays()
        {
            CountOfClassesPerWeekday = new List<ClassesPerWeekday>();
            foreach (Weekdays weekday in Enum.GetValues(typeof (Weekdays)))
            {
                var count = Classes.Count(c => c.ClassTime.Day == weekday);
                CountOfClassesPerWeekday.Add(new ClassesPerWeekday{ Weekday = weekday, Count = count});
            }
        }

        protected void SetAverageCountOfClassesPerDay()
        {
            AverageCountOfClassesPerDay = CountOfClassesPerWeek / (float)CountWeekdays;
        }
    }
    
    // !!! old Statistic !!!
    //abstract public class Statistic
    //{
    //    private static int countWeekdays = Enum.GetValues(typeof(Weekdays)).Length ;

    //    protected ClassesSchedule _schedule;
    //    protected IEnumerable<FullClassRecord> _classes;
    //    public int CountOfClassesPerOddWeek { get; protected set; }
    //    public int CountOfClassesPerEvenWeek { get; protected set; }
    //    public float AverageCountOfClassesPerWeek { get; protected set; }
    //    public float AverageCountOfClassesPerDay { get; protected set; }

    //    public Dictionary<Weekdays, int> CountOfClassesPerOddWeekday = new Dictionary<Weekdays, int>(countWeekdays);
    //    public Dictionary<Weekdays, int> CountOfClassesPerEvenWeekday = new Dictionary<Weekdays, int>(countWeekdays);
    //    public Dictionary<Weekdays, float> AverageCountOfClassesPerWeekday = new Dictionary<Weekdays, float>(countWeekdays);

    //    protected void SetCounts()
    //    {
    //        SetCountOfClassesPerWeek();
    //        SetCountsOfClassesPerAllWeekdays();
    //        SetAverageCountOfClassesPerDay();
    //    }

    //    protected void SetCountOfClassesPerWeek()
    //    {
    //        CountOfClassesPerOddWeek = _classes.Where(c => c.Time.Week == WeekType.Odd ||
    //                                                        c.Time.Week == WeekType.Both).Count();

    //        CountOfClassesPerEvenWeek = _classes.Where(c => c.Time.Week == WeekType.Even ||
    //                                                        c.Time.Week == WeekType.Both).Count();

    //        AverageCountOfClassesPerWeek = (float)(CountOfClassesPerOddWeek + CountOfClassesPerEvenWeek) / 2.0f;
    //    }

    //    protected void SetCountsOfClassesPerAllWeekdays()
    //    {
    //        foreach (Weekdays weekday in Enum.GetValues(typeof(Weekdays)))
    //            SetCountOfClassesPerSpecificWeekday(weekday);
    //    }

    //    protected void SetCountOfClassesPerSpecificWeekday(Weekdays weekday)
    //    {
    //        var oddWeekday = _classes.Where(c => c.Time.Day == weekday &&
    //                                            (c.Time.Week == WeekType.Odd ||
    //                                             c.Time.Week == WeekType.Both)).Count();

    //        var evenWeekday = _classes.Where(c => c.Time.Day == weekday &&
    //                                             (c.Time.Week == WeekType.Even ||
    //                                              c.Time.Week == WeekType.Both)).Count();

    //        var averageWeekday = (float)(oddWeekday + evenWeekday) / 2.0f;

    //        CountOfClassesPerOddWeekday.Add(weekday, oddWeekday);
    //        CountOfClassesPerEvenWeekday.Add(weekday, evenWeekday);
    //        AverageCountOfClassesPerWeekday.Add(weekday, averageWeekday);
    //    }

    //    protected void SetAverageCountOfClassesPerDay()
    //    {
    //        AverageCountOfClassesPerDay = AverageCountOfClassesPerWeek / countWeekdays;
    //    }
    //}
}
