using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Models
{
    public class FullClassRecord : ClassRecord
    {
        public Group Group;
        public ClassTime Time;

        public FullClassRecord(ClassTime time,  Group group, Subject subject, 
                               Lecturer lecturer, Classroom classroom)
        {
            Group     = group;
            Time      = time;
            Subject   = subject;
            Lecturer  = lecturer;
            Classroom = classroom;
        }

        public FullClassRecord(ClassTime time,  Group group, ClassRecord @class)
        {
                Group = group;
                Time = time;
                Subject = @class.Subject;
                Lecturer = @class.Lecturer;
                Classroom = @class.Classroom;
        }
    }
}
