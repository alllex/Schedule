using System.Windows;
using System.Windows.Controls;
using Editor.ViewModels.Cards;

namespace Editor.Views.Cards
{
    /// <summary>
    /// Interaction logic for ClassCardFullEditMode.xaml
    /// </summary>
    public partial class ClassCardFullEditMode : UserControl
    {
        public ClassCardFullEditMode()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var x = DataContext as ClassCardViewModel;
        }
    }
}
