﻿using FinanceControlSystem.Logics.Enum;
using FinanceControlSystem.Logics.Models;
using Newtonsoft.Json;

namespace FinanceControlSystem.Logics
{
    [Serializable]
    public class DataStorage
    {
        [JsonProperty]
        private Dictionary<int, TransactionCategoryModel> _transactionsCategories;
        [JsonProperty]
        private Dictionary<int, TransactionModel> _transactions;
        [JsonProperty]
        private Dictionary<int, ClientsFinanceModel> _clientsFinance;

        [JsonProperty]
        private int _transactionsCategoriesLastId;
        [JsonProperty]
        private int _transactionsLastId;
        [JsonProperty]
        private int _clientsFinanceLastId;
        private static string _filePath;

        public DataStorage()
        {
            _transactionsCategories = new Dictionary<int, TransactionCategoryModel>();
            _transactions = new Dictionary<int, TransactionModel>();
            _clientsFinance = new Dictionary<int, ClientsFinanceModel>();

            _transactionsCategoriesLastId = 1;
            _transactionsLastId = 1;
            _clientsFinanceLastId = 1;
            _filePath = Options.FilePath;
        }
        #region TransactionCategories
        public void AddCategory(TransactionCategoryModel category)
        {
            category.Id = _transactionsCategoriesLastId;
            _transactionsCategories.Add(_transactionsCategoriesLastId, category);
            _transactionsCategoriesLastId++;
        }

        public void RemoveCategoryById(int id)
        {
            _transactionsCategories.Remove(id);
        }

        public TransactionCategoryModel GetCategoryModelById(int id)
        {
            return _transactionsCategories[id];
        }

        public List<TransactionCategoryModel> GetAllCategoryModels()
        {
            return _transactionsCategories.Values.ToList();
        }
        #endregion

        #region Transactions
        public void AddTransaction(TransactionModel transaction)
        {
            transaction.Id = _transactionsLastId;
            _transactions.Add(transaction.Id, transaction);
            _transactionsLastId++;
        }

        public void RemoveTransactionByID(int id)
        {
            _transactions.Remove(id);
        }

        public TransactionModel GetCategoryByID(int id)
        {
            return _transactions[id];
        }

        public List<TransactionModel> GetAllTransactionModels()
        {
            List<TransactionModel> transactionList = new List<TransactionModel>();
            if (_transactions != null)
            {
                return _transactions.Values.ToList();
            }
            else
            {
                return transactionList;
            }
        }

        public int GetTransactionLastID()
        {
            int result = _transactionsLastId;
            return result;
        }

        public decimal CalculateOutcome()
        {
            decimal result = 0;
            List<TransactionModel> transactions = GetAllTransactionModels();
            foreach (var transaction in transactions)
            {
                if (transaction.Type == TransactionType.Outcome)
                {
                    result += transaction.Summ;

                }
            }

            return result;
        }

        public decimal CalculateIncome()
        {
            decimal result = 0;
            List<TransactionModel> transactions = GetAllTransactionModels();
            foreach (var transaction in transactions)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    result += transaction.Summ;

                }
            }

            return result;

        }

        public decimal CalculateDebt()
        {
            decimal result = 0;
            List<TransactionModel> transactions = GetAllTransactionModels();
            foreach (var transaction in transactions)
            {
                if (transaction.Type == TransactionType.Outcome && transaction.IsDebt == true)
                {
                    result -= transaction.Summ;

                }
            }

            return result;
        }
        #endregion

        #region ClientFinanceModel
        public void AddClientFinanceModel(ClientsFinanceModel model)
        {
            model.Id = _clientsFinanceLastId;
            _clientsFinance.Add(model.Id, model);
            _clientsFinanceLastId++;
        }

        public void RemoveClientModelByID(int id)
        {
            _clientsFinance.Remove(id);
        }

        public ClientsFinanceModel GetClientModelByID(int id)
        {
            return _clientsFinance[id];
        }

        public List<ClientsFinanceModel> GetAllClientModels()
        {
            return _clientsFinance.Values.ToList();
        }

        public decimal GetBalanceFromDebetAndCashModels()
        {
            decimal balance = 0;
            List<ClientsFinanceModel> models = GetAllClientModels();

            foreach (var model in models)
            {
                if (model.Сurrency == Enum.CurrencyType.rub && model.Type != ClientsFinanceType.Debt && model.Type != ClientsFinanceType.CreditCard)
                {
                    balance += model.Balance;
                }
            }

            return balance;
        }

        //не работает!
        //public decimal CalculateRubFromClientFinanceModels()
        //{
        //    decimal result = 0;

        //    List<ClientsFinanceModel> models = GetAllClientModels();
        //    foreach (var model in models)
        //    {
        //        if (model.Сurrency == Enum.CurrencyType.rub && model.Type != ClientsFinanceType.Debt && model.Type != ClientsFinanceType.CreditCard)
        //        {
        //            result += model.Balance;
        //        }
        //    }

        //    result = CalculateRubFromTransactions(result);

        //    return result;
        //}

        //private decimal CalculateRubFromTransactions(decimal sum)
        //{
        //    decimal result = sum;
        //    List<TransactionModel> transactions = GetAllTransactionModels();

        //    foreach (var transaction in transactions)
        //    {
        //        if (transaction.Type == TransactionType.Outcome && transaction.IsApproved == true)
        //        {
        //            result -= transaction.Summ;
        //        }
        //        else if (transaction.Type == TransactionType.Income && transaction.IsApproved == true)
        //        {
        //            result += transaction.Summ;
        //        }
        //    }

        //    return result;
        //}
        #endregion

        #region SaveAndLoad
        public void SaveToJson()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(_filePath, jsonData);
                Console.WriteLine("Данные успешно сохранены в файл JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в JSON: {ex.Message}");
            }
        }
        public static DataStorage LoadFromJson()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    DataStorage loadedData = JsonConvert.DeserializeObject<DataStorage>(jsonData);
                    Console.WriteLine("Данные успешно загружены из файла JSON.");
                    return loadedData;
                }
                else
                {
                    Console.WriteLine("Файл JSON не найден.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке из JSON: {ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
