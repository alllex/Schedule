using ScheduleData;
using SEE;

namespace Editor.Models
{
    class ImportExportSchedule
    {
        public static void Export(Schedule schedule, string path)
        {
            Exporter.Export(path, sClassesSchedule.Create(schedule));
        }

        public static Schedule Import(string path)
        {
            return null; // Заглушка
        }
    }
}
