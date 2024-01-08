using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Models;
using System.IO;
using System.Windows.Controls;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for PlanningControl.xaml
    /// </summary>
    public partial class PlanningControl : UserControl
    {
        private DataStorage _dataStorage;
        public PlanningControl()
        {
            InitializeComponent();
            _dataStorage = new DataStorage();
            _dataStorage = DataStorage.LoadFromJson();

            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }

            if (File.Exists("DataStorageVault.json"))
            {
                LoadListView();
                FillComboBoxAccountOfPayments();
            }
        }

        private void FillComboBoxAccountOfPayments()
        {
            List<string> financeTypesNames = GetListOfFinanceTypesForComboBoxAccountOfPayments();
            ComboBoxClientsFinanceType.ItemsSource = financeTypesNames;
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

        private void LoadListView()
        {
            ListViewOutcome.Items.Clear();

            List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            foreach (TransactionModel transaction in transactionsList)
            {
                int id = transaction.Id;
                string dOutcomeSumm = transaction.Summ.ToString();
                string sPaymentsCategoryType = transaction.PaymentsCategoryType.ToString();
                string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                string descriptionCategory = transaction.Name.ToString();
                string formattedDate = transaction.Date.ToString();
                bool income = transaction.IsIncome;
                bool approved = transaction.IsApproved;
                ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = id, Summ = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, IsIncome = income, IsApproved = approved, Description = descriptionCategory, Date = formattedDate });
            }
        }
        private void LoadListViewOutcome()
        {
            ListViewOutcome.Items.Clear();

            List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            foreach (TransactionModel transaction in transactionsList)
            {
                if(transaction.Type == Logics.Enum.TransactionType.Outcome)
                {
                    int id = transaction.Id;
                    string dOutcomeSumm = transaction.Summ.ToString();
                    string sPaymentsCategoryType = transaction.PaymentsCategoryType.ToString();
                    string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                    string descriptionCategory = transaction.Name.ToString();
                    string formattedDate = transaction.Date.ToString();
                    bool income = transaction.IsIncome;
                    bool approved = transaction.IsApproved;
                    ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = id, Summ = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, IsIncome = income, IsApproved = approved, Description = descriptionCategory, Date = formattedDate });
                }
            }
        }

        private void CheckBoxShowOutcomeOnly_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadListViewOutcome();
        }

        private void CheckBoxShowOutcomeOnly_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadListView();
        }
    }
}
