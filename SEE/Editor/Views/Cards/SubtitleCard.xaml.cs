using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Editor.Views.Cards
{
    /// <summary>
    /// Interaction logic for SubtitleCard.xaml
    /// </summary>
    public partial class SubtitleCard : UserControl
    {
        public SubtitleCard()
        {
            InitializeComponent();
        }

        private void TextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2) return;
            TextBlock.Visibility = Visibility.Collapsed;
            TextBox.Visibility = Visibility.Visible;
        }

        private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox.Visibility = Visibility.Collapsed;
            TextBlock.Visibility = Visibility.Visible;
        }
    }
}
