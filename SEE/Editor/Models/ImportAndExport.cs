using ScheduleData;
using SEE;

namespace Editor.Models
{
    class ScheduleExporter
    {
        public static void Export(Schedule schedule, string path)
        {
            Exporter.Export(path, schedule);
        }
    }

    class ScheduleImporter
    {
        public static Schedule Import(string path)
        {
            return Importer.Import(path);
        }
    }
}
