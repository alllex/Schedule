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

    public class ObjectCollection<T> : IObjectCollection<T> where T : IHavingId, ICloneable
    {
        private Dictionary<int, T> _collection;

        public T Add(T t)
        {
            int key = t.Id;
            T value;
            if (_collection.TryGetValue(key, out value))
            {
                return (T) value.Clone();
            }
            _collection.Add(key, (T) t.Clone());
            return t;
        }

        public bool Remove(T t)
        {
            int key = t.Id;
            return _collection.Remove(key);
        }

        public bool Submit(T t)
        {
            int key = t.Id;
            T oldValue;
            if (_collection.TryGetValue(key, out oldValue))
            {
                _collection[key] = t;
                return true;
            }
            return false;
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Values.ToArray();
        }
    }

    public class LecturersCollection : IObjectCollection<ILecturer>
    {
        public ILecturer Add(ILecturer t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ILecturer t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(ILecturer t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILecturer> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public class RoomsCollection : IObjectCollection<IRoom>
    {
        public IRoom Add(IRoom t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IRoom t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(IRoom t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IRoom> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public class LecturesTable : ILectures
    {
        public ILecture Add(ILecture t)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ILecture t)
        {
            throw new NotImplementedException();
        }

        public bool Submit(ILecture t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILecture> GetAll()
        {
            throw new NotImplementedException();
        }

        public ILecture Get(IGroup @group, ILectureTime lectureTime)
        {
            throw new NotImplementedException();
        }

        public ILecture Get(ILecturer lecturer, ILectureTime lectureTime)
        {
            throw new NotImplementedException();
        }

        public ILecture Get(IRoom room, ILectureTime lectureTime)
        {
            throw new NotImplementedException();
        }
    }
}