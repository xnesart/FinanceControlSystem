using FinanceControlSystem.Logics.Models;
using FinanseControleSystem.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControlSystem.Logics
{
    public class DataStorage
    {
        private Dictionary<int, TransactionCategoryModel> _transactionsCategories;
        private Dictionary<int, TransactionModel> _transactions;
        private Dictionary<int, ClientsFinanceModel> _clientsFinance;

        private int _transactionsCategoriesLastId;
        private int _transactionsLastId;
        private int _clientsFinanceLastId;

        public DataStorage()
        {
            _transactionsCategories = new Dictionary<int, TransactionCategoryModel>();
            _transactions = new Dictionary<int, TransactionModel>();
            _clientsFinance = new Dictionary<int, ClientsFinanceModel>();

            _transactionsCategoriesLastId = 1;
        }

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

        public void AddTransaction(TransactionModel transaction)
        {
            transaction.Id = _transactionsLastId;
            _transactions.Add(transaction.Id, transaction);
            _transactionsLastId++;
        }
        public void RemoveTransactionByID(int id) {
            _transactions.Remove(id);
        }
        public TransactionModel GetCategoryByID(int id)
        {
            return _transactions[id];
        }
        public List<TransactionModel> GetAllTransactionModels ()
        {
            return _transactions.Values.ToList();
        }
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
        public ClientsFinanceModel GetClientModelByID (int id)
        {
            return _clientsFinance[id];
        }
        public List<ClientsFinanceModel> GetAllClientModels()
        {
            return _clientsFinance.Values.ToList();
        }

    }
}
