using ScheduleData;
using SEE;

namespace Editor.Models
{
    class ImportExportSchedule
    {
        public static void Export(ClassesSchedule schedule, string path)
        {
            Exporter.Export(path, sClassesSchedule.Create(schedule));
        }

        public static ClassesSchedule Import(string path)
        {
            return null; // Заглушка
        }
    }
}
