using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleData.SearchConflicts
{
    public enum ConflictType { Warning, Conflict }

    public struct Conflict
    {
        public string Message { get; set; }
        public ConflictType ConflictType { get; set; }
        public IEnumerable<ClassRecord> ConflictingClasses { get; set; }
        public int CardsCount { get; set; }

        public Conflict(string message, ConflictType conflictType, IEnumerable<ClassRecord> conflictingClasses) : this()
        {
            Message = message;
            ConflictType = conflictType;
            ConflictingClasses = conflictingClasses;
            CardsCount = conflictingClasses.Count();
        }

        public Conflict(String message, ConflictType conflictType, ClassRecord conflictingClass) : this()
        {
            Message = message;
            ConflictType = conflictType;
            ConflictingClasses = new List<ClassRecord> { conflictingClass };
            CardsCount = 1;
        }
    }
}
