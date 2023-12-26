using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        private AccountClient _accountClient;
        private DataStorage _dataStorage;
        public MainControl()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();
            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }
            _accountClient = new AccountClient();
            LoadListBox();
        }

        private void ButtonAddAccount_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBoxNameOfAccount.Text;
            string currency = TextBoxCurrency.Text;
            decimal balance = decimal.Parse(TextBoxBalance.Text);
            ClientsFinanceType type = ClientsFinanceType.DebetCard;
            if (RadioButtonDebit.IsChecked == true)
            {
                type = ClientsFinanceType.DebetCard;
            }
            if (RadioButtonCredit.IsChecked == true)
            {
                type = ClientsFinanceType.CreditCard;
            }
            if (RadioButtonBankAccount.IsChecked == true)
            {
                type = ClientsFinanceType.BankAccount;
            }
            if (RadioButtonCash.IsChecked == true)
            {
                type = ClientsFinanceType.Cash;
            }
            if (RadioButtonDebt.IsChecked == true)
            {
                type = ClientsFinanceType.Debt;
            }
            ClientsFinanceModel model = new ClientsFinanceModel()
            {
                Name = name,
                Сurrency = currency,
                Balance = balance,
                Type = type,
            };
            _dataStorage.AddClientFinanceModel(model);
            _dataStorage.SaveToJson(_dataStorage);
            _dataStorage = DataStorage.LoadFromJson();
            LoadListBox();
        }

        private void LoadListBox()
        {
            ListBoxMain.Items.Clear();
            foreach(ClientsFinanceModel model in _dataStorage.GetAllClientModels())
            {
                Label label = new Label();
                label.Content = model.Name;
                SetLabelBackground(ref label, model);
                ListBoxMain.Items.Add(label);
            }
        }

        private void ButtonDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBoxNameOfAccount.Text;
            foreach(var model in _dataStorage.GetAllClientModels())
            {
                if (model.Name == name)
                {
                    _dataStorage.RemoveClientModelByID(model.Id);
                    _dataStorage.SaveToJson(_dataStorage);
                    _dataStorage = DataStorage.LoadFromJson();
                    break;
                }
            }
            LoadListBox();
        }

        private void SetLabelBackground(ref Label label, ClientsFinanceModel model)
        {
            if(model.Type == ClientsFinanceType.DebetCard)
            {
                label.Background = new SolidColorBrush(Colors.Aqua);
            }
        }
    }
}
