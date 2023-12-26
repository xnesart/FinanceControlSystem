using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Models;
using FinanseControleSystem.Logic.Models;
using System.Windows;
using System.Windows.Input;

namespace FinanceControlSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountClient _accountClient;
        private DataStorage _dataStorage;

        public MainWindow()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();
            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }
            _accountClient = new AccountClient();
        }


        private void ButtonAddAccount_Click(object sender, RoutedEventArgs e)
        {
            ClientsFinanceModel accountModel = new ClientsFinanceModel()
            {
                Name = TextBoxName.Text,
                Type = Logics.Enum.ClientsFinanceType.BankAccount,
                Balance = decimal.Parse(TextBoxBalance.Text),
            };

            if (accountModel.Name == "")
            {
                return;
            }

            _accountClient.AddAccount(accountModel);
            string name = TextBoxName.Text;
            //string type = TextBoxType.Text;
            string balance = TextBoxBalance.Text;
            ListBoxListOfAccounts.Items.Add($"Название {name},  баланс {balance}");
            TransactionCategoryModel category = new TransactionCategoryModel()
            {
                Name = name,
            };
            ClientsFinanceModel finance = new ClientsFinanceModel()
            {
                Name = name,
            };
            TransactionModel transaction = new TransactionModel()
            {
                Name = name,
            };
            TextBoxName.Text = "";
            //TextBoxType.Text = "";
            TextBoxBalance.Text = "";
            _dataStorage.AddTransaction(transaction);
            _dataStorage.AddClientFinanceModel(finance);
            _dataStorage.AddCategory(category);
        }
        private void ButtonAddWallet_Click(object sender, RoutedEventArgs e)
        {
            //DebitWallet debitWallet = new DebitWallet(TextBoxName.Text, decimal.Parse(TextBoxBalance.Text));
            //_walletClient.AddWallet(debitWallet);
            //string name = TextBoxName.Text;
            //string type = TextBoxType.Text;
            //string balance = TextBoxBalance.Text;
            //ListBoxListOfAccounts.Items.Add($"Название {name}, тип {type}, баланс {balance}");
            string name = TextBoxName.Text;

            TransactionCategoryModel category = new TransactionCategoryModel()
            {
                Name = name,
            };
            _dataStorage.AddCategory(category);
            ListBoxListOfAccounts.Items.Add($"Название {name}");
        }

        private void ButtonRemoveAccount_Click(object sender, RoutedEventArgs e)
        {
            //    string name = TextBoxName.Text;
            //    foreach (var item in ListBoxListOfAccounts.Items)
            //    {
            //        if(item is AccountModel account && account.Name == name)
            //        {
            //            _accountClient.RemoveAccount(name);
            //            ListBoxListOfAccounts.Items.Remove(item);
            //        }
            //    }

        }
        private void TextBoxBalance_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Предотвращаем ввод пробелов
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBoxBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Предотвращаем ввод символа, если это не цифра или точка
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true;
            }
            if (e.Text == " ")
            {
                e.Handled = true;
            }
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            if (MyPopup.IsOpen)
            {
                MyPopup.IsOpen = false;
            }
            else
            {
                MyPopup.IsOpen = true;
            }

        }

        private void ButtonSaveDB_Click(object sender, RoutedEventArgs e)
        {

            _dataStorage.SaveToJson(_dataStorage);
            //DataStorage.WriteToXmlFile("vault.xml", _dataStorage);
        }

        private void ButtonLoadBD_Click(object sender, RoutedEventArgs e)
        {
            List<TransactionCategoryModel> categories = _dataStorage.GetAllCategoryModels();

            ListBoxListOfAccounts.Items.Clear();

            foreach (var category in categories)
            {
                ListBoxListOfAccounts.Items.Add(category.Name);
            }
        }
    }
}