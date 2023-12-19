using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinanceControlSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountClient _accountClient;

        public MainWindow()
        {
            InitializeComponent();
            _accountClient = new AccountClient();
        }


        private void ButtonAddAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountModel accountModel = new AccountModel()
            {
                Name = TextBoxName.Text,
                Type = TextBoxType.Text,
                Balance = decimal.Parse(TextBoxBalance.Text),
            };
            if (accountModel.Name == "" || accountModel.Type == "" )
            {
                return;
            }
            _accountClient.AddAccount(accountModel);
            string name = TextBoxName.Text;
            string type = TextBoxType.Text;
            string balance = TextBoxBalance.Text;
            ListBoxListOfAccounts.Items.Add($"{name} и {type} и {balance}");
            TextBoxName.Text = "";
            TextBoxType.Text = "";
            TextBoxBalance.Text = "";
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
            if(e.Text == " ")
            {
                e.Handled = true;
            }
        }
    }
}