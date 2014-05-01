using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleData
{
    public class Schedule : ISchedule
    {
        public IObjectCollection<ILectureTime> TimeLine { get; private set; }
        public IObjectCollection<IGroup> Groups { get; private set; }
        public IObjectCollection<ILecturer> Lecturers { get; private set; }
        public IObjectCollection<IRoom> Rooms { get; private set; }
        public ILectures Lectures { get; private set; }
    }
}