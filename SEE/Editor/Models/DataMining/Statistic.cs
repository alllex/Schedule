using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.DataMining
{
    abstract public class Statistic<@Subject>
    {
        private static int countWeekdays = Enum.GetValues(typeof(Weekdays)).Length ;

        protected ClassesSchedule _schedule;
        protected @Subject _subject;
        protected IEnumerable<FullClassRecord> _classes;
        
        public int CountOfClassesPerWeek { get; protected set; }
        public float AverageCountOfClassesPerDay { get; protected set; }
        public Dictionary<Weekdays, int> CountOfClassesPerWeekday = new Dictionary<Weekdays, int>(countWeekdays);

        protected Statistic(ClassesSchedule schedule, @Subject subject, Func<FullClassRecord, @Subject> getField)
        {
            _subject = subject;
            _schedule = schedule;
            SetClasses(getField);
            SetCounts();
        }

        protected void SetClasses(Func<FullClassRecord, @Subject> getField) 
        {
            _classes = _schedule.ToList().Where(f => getField(f).Equals(_subject));
        }

        protected void SetCounts()
        {
            SetCountOfClassesPerWeek();
            SetCountsOfClassesPerAllWeekdays();
            SetAverageCountOfClassesPerDay();
        }

        protected void SetCountOfClassesPerWeek()
        {
            CountOfClassesPerWeek = _classes.Count();
        }

        protected void SetCountsOfClassesPerAllWeekdays()
        {
            foreach (Weekdays weekday in Enum.GetValues(typeof(Weekdays)))
                SetCountOfClassesPerSpecificWeekday(weekday);
        }

        protected void SetCountOfClassesPerSpecificWeekday(Weekdays weekday)
        {
            var count = _classes.Where(c => c.Time.Day == weekday).Count();
            CountOfClassesPerWeekday.Add(weekday, count);
        }

        protected void SetAverageCountOfClassesPerDay()
        {
            AverageCountOfClassesPerDay = (float)CountOfClassesPerWeek / (float)countWeekdays;
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
