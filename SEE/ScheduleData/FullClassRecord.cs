namespace ScheduleData
{
    public class FullClassRecord : ClassRecord
    {

        public FullClassRecord(ClassTime classTime,  Group group, Subject subject, 
                               Lecturer lecturer, Classroom classroom)
        {
            Group     = group;
            ClassTime      = classTime;
            Subject   = subject;
            Lecturer  = lecturer;
            Classroom = classroom;
        }

        public FullClassRecord(ClassRecord classRecord)
        {
            Group = classRecord.Group;
            ClassTime = classRecord.ClassTime;
            Subject = classRecord.Subject;
            Lecturer = classRecord.Lecturer;
            Classroom = classRecord.Classroom;
        }

        public FullClassRecord(ClassTime classTime,  Group group, ClassRecord @class)
        {
                Group = group;
                ClassTime = classTime;
                Subject = @class.Subject;
                Lecturer = @class.Lecturer;
                Classroom = @class.Classroom;
        }
    }
}
