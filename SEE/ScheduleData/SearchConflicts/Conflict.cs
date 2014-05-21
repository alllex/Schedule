using System;
using System.Collections.Generic;

namespace ScheduleData.SearchConflicts
{
    public enum ConflictType { Warning, Conflict }

    public struct Conflict
    {
        public String Message;
        public ConflictType ConflictType;
        public IEnumerable<FullClassRecord> ConflictingClasses;

        public Conflict(String message, ConflictType conflictType, IEnumerable<FullClassRecord> conflictingClasses)
        {
            Message = message;
            ConflictType = conflictType;
            ConflictingClasses = conflictingClasses;
        }

        public Conflict(String message, ConflictType conflictType, FullClassRecord conflictingClass)
        {
            Message = message;
            ConflictType = conflictType;
            ConflictingClasses = new List<FullClassRecord> { conflictingClass };
        }
    }
}
