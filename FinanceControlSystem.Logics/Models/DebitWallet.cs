using FinanceControlSystem.Logics.Enum;

namespace FinanceControlSystem.Logics.Models
{
    public class DebitWallet : AbstractWalletModel
    {
        public DebitWallet(string name, decimal balance) : base(name, balance, WalletEnumType.DebitCard)
        {
            
        }
    }
}
