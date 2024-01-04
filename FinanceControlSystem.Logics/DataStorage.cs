using FinanceControlSystem.Logics.Enum;
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

        public DataStorage()
        {
            _transactionsCategories = new Dictionary<int, TransactionCategoryModel>();
            _transactions = new Dictionary<int, TransactionModel>();
            _clientsFinance = new Dictionary<int, ClientsFinanceModel>();

            _transactionsCategoriesLastId = 1;
            _transactionsLastId = 1;
            _clientsFinanceLastId = 1;
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
            return _transactions.Values.ToList();
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
                result += transaction.Summ;
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

        public decimal CalculateRubFromClientFinanceModels()
        {
            decimal result = 0;

            List<ClientsFinanceModel> models = GetAllClientModels();
            foreach (var model in models)
            {
                if (model.Сurrency == Enum.CurrencyType.rub && model.Type != ClientsFinanceType.Debt && model.Type != ClientsFinanceType.CreditCard)
                {
                    result += model.Balance;
                }
            }
            return result;
        }
        #endregion

        #region SaveAndLoad
        public void SaveToJson(DataStorage data, string filePath = "DataStorageVault.json")
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Данные успешно сохранены в файл JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в JSON: {ex.Message}");
            }
        }
        public static DataStorage LoadFromJson(string filePath = "DataStorageVault.json")
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
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
