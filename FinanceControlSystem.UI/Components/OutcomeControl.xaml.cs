using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for OutcomeControl.xaml
    /// </summary>
    public partial class OutcomeControl : UserControl
    {
        private DataStorage _dataStorage;
        public OutcomeControl()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();

            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }

            FillComboBoxAccountOfPayments();
            LoadListView();
        }
        private void FillComboBoxAccountOfPayments()
        {

            ComboBoxClientsFinanceType.Items.Clear();

            List<string> financeTypesNames = GetListOfFinanceTypesForComboBoxAccountOfPayments();
            ComboBoxClientsFinanceType.ItemsSource = financeTypesNames;
        }

        private void ButtonAddOutcome_Click(object sender, RoutedEventArgs e)
        {
            string sPaymentsCategoryType = "";
            string sClientsFinanceType = "";
            decimal dOutcomeSumm = decimal.Parse(TextBoxOutcomeSumm.Text);

            if (ComboBoxPaymentsCategoryType.SelectedItem is null || ComboBoxClientsFinanceType.SelectedItem is null)
            {
                ComboBoxPaymentsCategoryType.SelectedIndex = 1;
                ComboBoxClientsFinanceType.SelectedIndex = 1;
            }
            else
            {
                sPaymentsCategoryType = ComboBoxPaymentsCategoryType.SelectedItem.ToString();
                sClientsFinanceType = ComboBoxClientsFinanceType.SelectedItem.ToString();
            }

            string accountName = ComboBoxClientsFinanceType.SelectedItem.ToString();
            
            string descriptionCategory = TextBoxOutcomeName.Text;

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd.MM.yyyy HH:mm");

            //создаем транзакцию
            TransactionModel transaction = new TransactionModel()
            {
                Name = descriptionCategory,
                Type = TransactionType.Outcome,
                ClientsFinanceType = sClientsFinanceType,
                PaymentsCategoryType = sPaymentsCategoryType,
                Summ = dOutcomeSumm,
                IsApproved = true,
                Date = DateTime.ParseExact(formattedDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
            };

            int lastId = _dataStorage.GetTransactionLastID();

            //переносим данные транзакции в ListView
            ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = lastId, Outcome = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, Description = descriptionCategory, Date = formattedDate });

            _dataStorage.AddTransaction(transaction);
            _dataStorage.SaveToJson(_dataStorage);
        }

        private void LoadListView()
        {
            int id = 1;
            List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            foreach (TransactionModel transaction in transactionsList)
            {
                string dOutcomeSumm = transaction.Summ.ToString();
                string sPaymentsCategoryType = transaction.PaymentsCategoryType.ToString();
                string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                string descriptionCategory = transaction.Name.ToString();
                string formattedDate = transaction.Date.ToString();
                ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = id, Outcome = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, Description = descriptionCategory, Date = formattedDate });
                id++;
            }
        }

        private List<string> GetListOfFinanceTypesForComboBoxAccountOfPayments()
        {
            List<string> financeTypesNames = new List<string>();
            List<ClientsFinanceModel> clients = _dataStorage.GetAllClientModels();

            foreach (ClientsFinanceModel client in clients)
            {
                financeTypesNames.Add(client.Name);
            }

            return financeTypesNames;
        }
        private int GetAccountIdForTransaction(string transactionName)
        {
            int accountId = -1;
            List<ClientsFinanceModel> list = _dataStorage.GetAllClientModels();

            foreach (ClientsFinanceModel client in list)
            {
                if (client.Name == transactionName)
                {
                    accountId = client.Id;
                    break;
                }
            }

            return accountId;
        }
    }
}
