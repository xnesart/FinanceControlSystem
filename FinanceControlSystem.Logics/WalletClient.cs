using FinanceControlSystem.Logics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControlSystem.Logics
{
    public class WalletClient
    {
        private List<AbstractWalletModel> _wallets;

        public WalletClient() {
            _wallets = new List<AbstractWalletModel>();
        }

        public void AddWallet(AbstractWalletModel wallet)
        {
            _wallets.Add(wallet);
        }

    }
}
