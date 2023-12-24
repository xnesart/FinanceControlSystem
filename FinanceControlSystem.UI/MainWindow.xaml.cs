using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Models;
using FinanseControleSystem.Logic.Models;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

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
            DataStorage data = DataStorage.ReadFromJsonFile<DataStorage>("DataStorage.json");
            if(data == null)
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
            string type = TextBoxType.Text;
            string balance = TextBoxBalance.Text;
            ListBoxListOfAccounts.Items.Add($"Название {name}, тип {type}, баланс {balance}");
            TextBoxName.Text = "";
            TextBoxType.Text = "";
            TextBoxBalance.Text = "";
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
            string balance = TextBoxBalance.Text;

            TransactionCategoryModel category = new TransactionCategoryModel()
            {
                Name = Name,
            };
            ListBoxListOfAccounts.Items.Add($"Название {name}");
            string type = TextBoxType.Text;
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
            
            DataStorage.WriteToJsonFile(_dataStorage);
        }
    }
}