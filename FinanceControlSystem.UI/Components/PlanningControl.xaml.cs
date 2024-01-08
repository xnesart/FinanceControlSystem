using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                if (transaction.Type == TransactionType.Outcome)
                {
                    int id = transaction.Id;
                    string dOutcomeSumm = transaction.Summ.ToString();
                    string sPaymentsCategoryType = transaction.PaymentsCategoryType.ToString();
                    string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                    string descriptionCategory = transaction.Name.ToString();
                    string formattedDate = transaction.Date.ToString();
                    ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = id, Outcome = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, Description = descriptionCategory, Date = formattedDate });
                }
            }
        }

    }
}
