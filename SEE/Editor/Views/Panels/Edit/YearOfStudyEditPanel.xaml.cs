using System.Windows;
using System.Windows.Controls;

namespace Editor.Views.Panels.Edit
{
    /// <summary>
    /// Interaction logic for GroupEditorPanel.xaml
    /// </summary>
    public partial class YearOfStudyEditPanel : UserControl
    {
        public YearOfStudyEditPanel()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.GetType() + "");
        }
    }
}
