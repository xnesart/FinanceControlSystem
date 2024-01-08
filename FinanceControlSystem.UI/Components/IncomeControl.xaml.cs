using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FinanceControlSystem.UI.Components
{
    /// <summary>
    /// Interaction logic for IncomeControl.xaml
    /// </summary>
    public partial class IncomeControl : UserControl
    {
        private DataStorage _dataStorage;
        public IncomeControl()
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

            //ComboBoxClientsFinanceType.Items.Clear();

            List<string> financeTypesNames = GetListOfFinanceTypesForComboBoxAccountOfPayments();
            ComboBoxClientsFinanceType.ItemsSource = financeTypesNames;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal sum = decimal.Parse(TextBoxSumOfIncome.Text);
            string description = TextBoxIncomeDescription.Text;
            string sCategoryOfIncome = ComboBoxIncomeCategory.SelectedItem.ToString();
            string sClientFinanceType = ComboBoxClientsFinanceType.SelectedItem.ToString();

            DateTime? selectedDate = DatePickerIncomeDate.SelectedDate;
            DateTime date;
            string formattedDate;
            if (selectedDate.HasValue)
            {
                date = selectedDate.Value;
                formattedDate = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                date = DateTime.Now;
                formattedDate = DateTime.Now.ToString();
            }

            //создаем транзакцию
            TransactionModel transaction = new TransactionModel()
            {
                Name = description,
                Type = TransactionType.Income,
                ClientsFinanceType = sClientFinanceType,
                PaymentsCategoryType = sCategoryOfIncome,
                Summ = sum,
                IsApproved = true,
                Date = date,
            };

            int lastId = _dataStorage.GetTransactionLastID();

            //переносим данные транзакции в ListView
            ListViewIncome.Items.Add(new FinancialMovementsItem { ID = lastId, Income = sum.ToString(), Category = sCategoryOfIncome, Account = sClientFinanceType, Description = description, Date = formattedDate });

            SumTransactionForClientFinanceType(sum, sClientFinanceType);
            _dataStorage.AddTransaction(transaction);
            _dataStorage.SaveToJson();
        }

        private void ButtonDeleteIncome_Click(object sender, RoutedEventArgs e)
        {
            int idIncome = int.Parse(TextBoxRemoveIncome.Text);
            List<TransactionModel> models = _dataStorage.GetAllTransactionModels();

            foreach (TransactionModel model in models)
            {
                if (model.Id == idIncome && model.Type == TransactionType.Income)
                {
                    _dataStorage.RemoveTransactionByID(idIncome);
                    break;
                }
            }

            _dataStorage.SaveToJson();
            LoadListView();
            FillComboBoxAccountOfPayments();
        }

        private void LoadListView()
        {
            //int id = 1;
            //List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            //foreach (TransactionModel transaction in transactionsList)
            //{
            //    if (transaction.Type == TransactionType.Income)
            //    {
            //        string sum = transaction.Summ.ToString();
            //        string sCategoryOfIncome = transaction.PaymentsCategoryType.ToString();
            //        string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
            //        string description = transaction.Name.ToString();
            //        string formattedDate = transaction.Date.ToString();
            //        ListViewIncome.Items.Add(new FinancialMovementsItem { ID = id, Income = sum.ToString(), Category = sCategoryOfIncome, Account = sClientsFinanceType, Description = description, Date = formattedDate });
            //    }
            //    id++;
            //}
            ListViewIncome.Items.Clear();

            List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            foreach (TransactionModel transaction in transactionsList)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    int id = transaction.Id;
                    string sCategoryOfIncome = transaction.Summ.ToString();
                    string sPaymentsCategoryType = transaction.PaymentsCategoryType.ToString();
                    string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                    string descriptionCategory = transaction.Name.ToString();
                    string formattedDate = transaction.Date.ToString();
                    ListViewIncome.Items.Add(new FinancialMovementsItem { ID = id, Income = sCategoryOfIncome.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, Description = descriptionCategory, Date = formattedDate });
                }
            }
        }

        private bool SumTransactionForClientFinanceType(decimal income, string clientFinanceTypeName)
        {
            List<ClientsFinanceModel> clientsFinanceModels = _dataStorage.GetAllClientModels().ToList();
            bool res = false;

            foreach (var clientsFinaceModel in clientsFinanceModels)
            {
                if (clientsFinaceModel.Name == $"{clientFinanceTypeName}")
                {
                    clientsFinaceModel.Balance += income;
                    res = true;
                    break;
                }
            }

            return res;
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

        public void GetUpdate()
        {
            ListViewIncome.Items.Clear();
            _dataStorage = DataStorage.LoadFromJson();
            LoadListView();
            FillComboBoxAccountOfPayments();
        }


    }
}
