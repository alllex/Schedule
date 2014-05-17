using System.Collections.Generic;
using System.Windows;

namespace Editor.Helpers
{
    public static class ClipboardService
    {
        private static readonly Dictionary<string, object> Clipboard = new Dictionary<string, object>();  

        public static bool ContainsData<T>()
        {
            return Clipboard.ContainsKey(typeof(T).ToString());
        }

        public static void SetData<T>(T data)
        {
            string key = typeof (T).ToString();
            Clipboard[key] = data;
        }

        public static T GetData<T>() where T : class
        {
            string key = typeof(T).ToString();
            object value;
            var found = Clipboard.TryGetValue(key, out value);
            return found ? value as T : null;
        }
    }

}
