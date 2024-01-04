using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            LoadListView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal sum = decimal.Parse(TextBoxSumOfIncome.Text);
            string description = TextBoxIncomeDescription.Text;
            string sCategoryOfIncome = ComboBoxIncomeCategory.SelectedItem.ToString();
            string sClientFinanceType = ComboBoxClientsFinanceType.SelectedItem.ToString();


            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd.MM.yyyy HH:mm");

            //создаем транзакцию
            TransactionModel transaction = new TransactionModel()
            {
                Name = description,
                Type = TransactionType.Income,
                ClientsFinanceType = sClientFinanceType,
                PaymentsCategoryType = sCategoryOfIncome,
                Summ = sum,
                IsApproved = true,
                Date = DateTime.ParseExact(formattedDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
            };

            int lastId = _dataStorage.GetTransactionLastID();

            //переносим данные транзакции в ListView
            ListViewIncome.Items.Add(new FinancialMovementsItem { ID = lastId, Income = sum.ToString(), Category = sCategoryOfIncome, Account = sClientFinanceType, Description = description, Date = formattedDate });

            SumTransactionForClientFinanceType(sum, sClientFinanceType);
            _dataStorage.AddTransaction(transaction);
            _dataStorage.SaveToJson(_dataStorage);
        }

        private void LoadListView()
        {
            int id = 1;
            List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            foreach (TransactionModel transaction in transactionsList)
            {
                if(transaction.Type == TransactionType.Income)
                {
                    string sum = transaction.Summ.ToString();
                    string sCategoryOfIncome = transaction.PaymentsCategoryType.ToString();
                    string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                    string description = transaction.Name.ToString();
                    string formattedDate = transaction.Date.ToString();
                    ListViewIncome.Items.Add(new FinancialMovementsItem { ID = id, Income = sum.ToString(), Category = sCategoryOfIncome, Account = sClientsFinanceType, Description = description, Date = formattedDate });
                    
                }
                id++;
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

        public void GetUpdate()
        {
            ListViewIncome.Items.Clear();
            _dataStorage = DataStorage.LoadFromJson();
            LoadListView();
        }
    }
}
