using FinanceControlSystem.Logics.Models;

namespace FinanceControlSystem.Logics
{
    public class AccountClient
    {
        private List<ClientsFinanceModel> _accounts;
        public AccountClient()
        {
            _accounts = new List<ClientsFinanceModel>();
        }

        public void AddAccount(ClientsFinanceModel account)
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
