using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for FinanceListItem.xaml
    /// </summary>
    public partial class FinanceListItem : UserControl
    {
        public FinanceListItem(ClientsFinanceModel model)
        {
            InitializeComponent();

            LabelName.Content = model.Name;
            LabelBalance.Content = $"{model.Balance} {model.Сurrency}";

            if (model.Type == ClientsFinanceType.DebetCard)
            {
                GridItem.Background = new SolidColorBrush(Colors.GreenYellow);
            }
            else if (model.Type == ClientsFinanceType.CreditCard)
            {
                GridItem.Background = new SolidColorBrush(Colors.IndianRed);
            }
            else if (model.Type == ClientsFinanceType.BankAccount)
            {
                GridItem.Background = new SolidColorBrush(Colors.BlueViolet);
            }
            else if (model.Type == ClientsFinanceType.Cash)
            {
                GridItem.Background = new SolidColorBrush(Colors.Green);
            }
            else if (model.Type == ClientsFinanceType.Debt)
            {
                GridItem.Background = new SolidColorBrush(Colors.OrangeRed);
            }
        }
    }
}
