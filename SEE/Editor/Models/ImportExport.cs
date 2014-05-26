using ScheduleData;
using SEE;

namespace Editor.Models
{
    class ScheduleExporter
    {
        public static void ExcelExport(Schedule schedule, string path)
        {
            ExcelExporter.Export(path, schedule);
        }

        public static void DatabaseExport(Schedule schedule, string path)
        {
            SQLiteDatabaseIO.Save(schedule, path);
        }

    }

    class ScheduleImporter
    {
        public static Schedule ExcelImport(string path)
        {
            return ExcelImporter.Import(path);
        }

        public static Schedule DatabaseImport(string fileName)
        {
            return SQLiteDatabaseIO.Load(fileName);
        }
    }
}
