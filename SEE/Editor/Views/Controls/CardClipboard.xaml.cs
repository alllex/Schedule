using System.Windows.Controls;
using System.Windows.Input;
using Editor.ViewModels.Cards;
using Editor.Views.Cards;

namespace Editor.Views.Controls
{
    /// <summary>
    /// Interaction logic for CardsPool.xaml
    /// </summary>
    public partial class CardClipboard : UserControl
    {
        public CardClipboard()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var classCard = sender as ClassCardViewMode;
            if (classCard == null) return;
            var model = classCard.DataContext as ClassCardViewModel;
            if (model == null) return;
            //model.IsSelected = true;

        }

        
    }
}
