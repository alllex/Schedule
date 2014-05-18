using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models.SearchConflicts
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
    }
}
