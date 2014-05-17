using System;
using System.Windows;
using System.Windows.Interop;

namespace Editor.Views.Cards
{
    /// <summary>
    /// Interaction logic for ClassCardEditMode.xaml
    /// </summary>
    public partial class ClassCardEditMode : Window
    {
        
        private readonly double _centerX = -1;
        private readonly double _centerY = -1;
        public ClassCardEditMode(double centerX, double centerY)
        {
            InitializeComponent();
            _centerX = centerX;
            _centerY = centerY;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Top = _centerY - Height/2;
            Left = _centerX - Width/2;
            UpdateLayout();
            Window_Loaded();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded()
        {
            var interopHelper = new WindowInteropHelper(this);
            var hwndSource = HwndSource.FromHwnd(interopHelper.Handle);
            hwndSource.AddHook(WndProcHook);
        }

        private IntPtr WndProcHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x84 /* WM_NCHITTEST */)
            {
                handled = true;
                return (IntPtr)1;
            }
            handled = false;
            return (IntPtr)0;
        }
    }
}
