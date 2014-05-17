using System.Windows;

namespace Editor.Helpers
{
    public class ClipboardService
    {

        public static bool ContainsData<T>() where T:class
        {
            return Clipboard.ContainsData(typeof(T).ToString());
        }

        public static void SetData<T>(T data) where T:class
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetData(typeof(T).ToString(), data);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                System.Threading.Thread.Sleep(0);
                try
                {
                    Clipboard.Clear();
                    Clipboard.SetData(typeof(T).ToString(), data);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Can't Access Clipboard");
                }
            }
        }

        public static T GetData<T>() where T : class
        {
            try
            {
                if (ContainsData<T>())
                {
                    var x = Clipboard.GetData(typeof (T).ToString());
                    return x as T;
                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                System.Threading.Thread.Sleep(0);
                try
                {
                    if (ContainsData<T>())
                    {
                        return Clipboard.GetData(typeof(T).ToString()) as T;
                    }
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Can't Access Clipboard");
                }
            }
            return null;
        }
    }

}
