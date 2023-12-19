using FinanceControlSystem.Logics.Models;

namespace FinanceControlSystem.Logics
{
    public class AccountClient
    {
        private List<AccountModel> _accounts;
        public AccountClient()
        {
            _accounts = new List<AccountModel>();
        }

        public void AddAccount(AccountModel account)
        {
            _accounts.Add(account);
        }
        public bool RemoveAccount(string accountName)
        {
            bool res = false;
            foreach (var account in _accounts)
            {
                if (account.Name == accountName)
                {
                    _accounts.Remove(account);
                    res = true;
                }
            }
            return res;
        }
    }
}
