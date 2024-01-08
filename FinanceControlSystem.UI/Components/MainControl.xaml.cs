using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.Windows;
using System.Windows.Controls;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        private AccountClient _accountClient;
        private DataStorage _dataStorage;
        private Dictionary<int, int> _listBoxItemsId;
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
            string description = TextBoxDescription.Text;

            decimal balance;

            if (!decimal.TryParse(TextBoxBalance.Text, out balance))
            {
                // Обработка случая, когда преобразование не удалось
                MessageBox.Show("Некорректный формат числа. Пожалуйста, введите правильное число.");
                return;
            }

            ClientsFinanceType type = GetClientFinanceTypeFromComboBox();
            CurrencyType currency = GetCurrencyTypeFromComboBox();
            ClientsFinanceModel model = new ClientsFinanceModel()
            {
                Name = name,
                Сurrency = currency,
                Balance = balance,
                Type = type,
                Desciption = description
            };
            _dataStorage.AddClientFinanceModel(model);
            _dataStorage.SaveToJson();
            _dataStorage = DataStorage.LoadFromJson();
            LoadListBox();
            TextBoxNameOfAccount.Text = "";
            TextBoxBalance.Text = "";

        }

        private void ButtonDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBoxNameOfAccount.Text;
            foreach (var model in _dataStorage.GetAllClientModels())
            {
                if (model.Name == name)
                {
                    _dataStorage.RemoveClientModelByID(model.Id);
                    _dataStorage.SaveToJson();
                    _dataStorage = DataStorage.LoadFromJson();
                    break;
                }
            }
            LoadListBox();
        }

        private ClientsFinanceType GetClientFinanceTypeFromComboBox()
        {
            ClientsFinanceType type = ClientsFinanceType.DebetCard;
            if (ComboBoxClientsFinanceType.SelectedItem is ClientsFinanceType.DebetCard)
            {
                type = ClientsFinanceType.DebetCard;
            }
            if (ComboBoxClientsFinanceType.SelectedItem is ClientsFinanceType.CreditCard)
            {
                type = ClientsFinanceType.CreditCard;
            }
            if (ComboBoxClientsFinanceType.SelectedItem is ClientsFinanceType.BankAccount)
            {
                type = ClientsFinanceType.BankAccount;
            }
            if (ComboBoxClientsFinanceType.SelectedItem is ClientsFinanceType.Cash)
            {
                type = ClientsFinanceType.Cash;
            }
            if (ComboBoxClientsFinanceType.SelectedItem is ClientsFinanceType.Debt)
            {
                type = ClientsFinanceType.Debt;
            }

            return type;
        }

        private CurrencyType GetCurrencyTypeFromComboBox()
        {
            CurrencyType currency = CurrencyType.rub;

            if (ComboBoxCurrencyType.SelectedItem is CurrencyType.eur)
            {
                currency = CurrencyType.eur;
            }
            if (ComboBoxCurrencyType.SelectedItem is CurrencyType.us)
            {
                currency = CurrencyType.us;
            }

            return currency;
        }

        private void LoadListBox()
        {
            ListBoxMain.Items.Clear();
            _listBoxItemsId = new Dictionary<int, int>();
            foreach (ClientsFinanceModel model in _dataStorage.GetAllClientModels())
            {
                int number = ListBoxMain.Items.Add(new FinanceListItem(model));
                _listBoxItemsId.Add(number, model.Id);
            }
        }

        private void ListBoxMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxMain.SelectedIndex != -1)
            {

                int modelId = _listBoxItemsId[ListBoxMain.SelectedIndex];
                ClientsFinanceModel model = _dataStorage.GetClientModelByID(modelId);
                TextBoxNameOfAccount.Text = model.Name;
                ComboBoxClientsFinanceType.SelectedItem = model.Type;
                ComboBoxCurrencyType.SelectedItem = model.Сurrency;
                TextBoxBalance.Text = model.Balance.ToString();
                TextBoxDescription.Text = model.Desciption;
            }
        }
        public void GetUpdate()
        {
            _dataStorage = DataStorage.LoadFromJson();
            LoadListBox();
        }
    }
}
