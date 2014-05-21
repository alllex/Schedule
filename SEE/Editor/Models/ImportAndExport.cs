using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEE;

namespace Editor.Models
{
    class ImportAndExport
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
