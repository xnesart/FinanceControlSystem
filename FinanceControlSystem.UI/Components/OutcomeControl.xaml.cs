﻿using FinanceControlSystem.Logics;
using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using System.IO;
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

            //DateTime currentDate = DatePickerOutcomeDate.SelectedDate;
            //string formattedDate = "";
            //if (currentDate.HasValue)
            //{
            //    //formattedDate = DatePickerOutcomeDate.Text.ToString();
            //    //string formattedDate = currentDate.ToString("dd.MM.yyyy HH:mm");
            //}

            DateTime? selectedDate = DatePickerOutcomeDate.SelectedDate;
            DateTime date;
            string formattedDate;
            if (selectedDate.HasValue)
            {
                date = selectedDate.Value;
                formattedDate = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //formattedDate = selectedDate.Value;
            }
            else
            {
                date = DateTime.Now;
                formattedDate = DateTime.Now.ToString();
            }


            //создаем транзакцию
            TransactionModel transaction = new TransactionModel()
            {
                Name = descriptionCategory,
                Type = TransactionType.Outcome,
                ClientsFinanceType = sClientsFinanceType,
                PaymentsCategoryType = sPaymentsCategoryType,
                Summ = dOutcomeSumm,
                IsApproved = true,
                Date = date,
            };

            int lastId = _dataStorage.GetTransactionLastID();

            //переносим данные транзакции в ListView
            ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = lastId, Outcome = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, Description = descriptionCategory, Date = formattedDate });

            SubstractTransactionForClientFinanceType(dOutcomeSumm, accountName);
            _dataStorage.AddTransaction(transaction);
            _dataStorage.SaveToJson(_dataStorage);
        }

        private void LoadListView()
        {
            int id = 1;
            List<TransactionModel> transactionsList = _dataStorage.GetAllTransactionModels();
            foreach (TransactionModel transaction in transactionsList)
            {
                if (transaction.Type == TransactionType.Outcome)
                {
                    string dOutcomeSumm = transaction.Summ.ToString();
                    string sPaymentsCategoryType = transaction.PaymentsCategoryType.ToString();
                    string sClientsFinanceType = transaction.ClientsFinanceType.ToString();
                    string descriptionCategory = transaction.Name.ToString();
                    string formattedDate = transaction.Date.ToString();
                    ListViewOutcome.Items.Add(new FinancialMovementsItem { ID = id, Outcome = dOutcomeSumm.ToString(), Category = sPaymentsCategoryType, Account = sClientsFinanceType, Description = descriptionCategory, Date = formattedDate });
                }

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

        private bool SubstractTransactionForClientFinanceType(decimal outcome, string clientFinanceTypeName)
        {
            List<ClientsFinanceModel> clientsFinanceModels = _dataStorage.GetAllClientModels().ToList();
            bool res = false;

            foreach (var clientsFinaceModel in clientsFinanceModels)
            {
                if (clientsFinaceModel.Name == $"{clientFinanceTypeName}")
                {
                    clientsFinaceModel.Balance -= outcome;
                    res = true;
                    break;
                }
            }

            return res;
        }
        public void GetUpdate()
        {
            ListViewOutcome.Items.Clear();
            _dataStorage = DataStorage.LoadFromJson();
            LoadListView();
            FillComboBoxAccountOfPayments();
        }
    }
}
