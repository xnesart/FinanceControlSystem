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
            };
            _dataStorage.AddClientFinanceModel(model);
            _dataStorage.SaveToJson(_dataStorage);
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
                    _dataStorage.SaveToJson(_dataStorage);
                    _dataStorage = DataStorage.LoadFromJson();
                    break;
                }
            }
            LoadListBox();
        }

        private void SetLabelBackground(ref Label label, ClientsFinanceModel model)
        {
            if (model.Type == ClientsFinanceType.DebetCard)
            {
                label.Background = new SolidColorBrush(Colors.Aqua);
            }
            if (model.Type == ClientsFinanceType.CreditCard)
            {
                label.Background = new SolidColorBrush(Colors.DarkCyan);
            }
            if (model.Type == ClientsFinanceType.BankAccount)
            {
                label.Background = new SolidColorBrush(Colors.MediumVioletRed);
            }
            if (model.Type == ClientsFinanceType.Cash)
            {
                label.Background = new SolidColorBrush(Colors.Olive);
            }
            if (model.Type == ClientsFinanceType.Debt)
            {
                label.Background = new SolidColorBrush(Colors.Red);
            }

        }

        private ClientsFinanceType GetClientFinanceTypeFromComboBox()
        {
            ClientsFinanceType type = ClientsFinanceType.DebetCard;
            if (ComboBoxCategoryOfAccount.SelectedItem is ClientsFinanceType.DebetCard)
            {
                type = ClientsFinanceType.DebetCard;
            }
            if (ComboBoxCategoryOfAccount.SelectedItem is ClientsFinanceType.CreditCard)
            {
                type = ClientsFinanceType.CreditCard;
            }
            if (ComboBoxCategoryOfAccount.SelectedItem is ClientsFinanceType.BankAccount)
            {
                type = ClientsFinanceType.BankAccount;
            }
            if (ComboBoxCategoryOfAccount.SelectedItem is ClientsFinanceType.Cash)
            {
                type = ClientsFinanceType.Cash;
            }
            if (ComboBoxCategoryOfAccount.SelectedItem is ClientsFinanceType.Debt)
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
            foreach (ClientsFinanceModel model in _dataStorage.GetAllClientModels())
            {
                Label label = new Label();
                label.Content = model.Name;
                SetLabelBackground(ref label, model);
                ListBoxMain.Items.Add(label);
            }
        }

        private void ComboBoxCategoryOfAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxCurrencyType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
