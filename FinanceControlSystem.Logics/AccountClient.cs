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
    }
}
