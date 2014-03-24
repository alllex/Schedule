using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Editor.UserControls;

namespace Editor.Views
{
    /// <summary>
    /// Interaction logic for EditCardWindow.xaml
    /// </summary>
    public partial class EditCardWindow : Window
    {
        public EditCardWindow()
        {
            InitializeComponent();
        }

        private Table _parent;
        public EditCardWindow(Table parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _parent.IsEnabled = true;
        }

        private void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                Close();
            }
        }
    }
}
