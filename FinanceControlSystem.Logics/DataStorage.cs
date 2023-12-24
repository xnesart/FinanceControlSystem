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

        public void DetachCategoryById(int id)
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
    }
}
