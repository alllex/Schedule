using System.Globalization;
using System.Threading;
using System.Windows;
using Editor.Views.Windows;

namespace Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
        }

        public static void ChangeCulture(CultureInfo newCulture)
        {
            if (newCulture.Name == Thread.CurrentThread.CurrentCulture.Name) return;
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            var oldWindow = Current.MainWindow;
            Current.MainWindow = new EditorWindow();
            Current.MainWindow.Show();
            oldWindow.Close();
        }
    }
}
