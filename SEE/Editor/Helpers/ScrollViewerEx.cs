using System.Windows.Controls;
using System.Windows.Input;

namespace Editor.Helpers
{
    class ScrollViewerEx : ScrollViewer
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down)
            {
                e.Handled = false;
                return;
            }
//            if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
//            {
//                if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down)
//                {
//                    return;
//                }
//            }
            base.OnKeyDown(e);
        }
    }
}
