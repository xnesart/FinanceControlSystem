using FinanceControlSystem.Logics.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControlSystem.Logics.Models
{
    public abstract class AbstractWalletModel
    {
        public string Name { get; set; }
        public WalletEnumType Type { get; set; }

        public decimal Balance { get; set; }

        protected AbstractWalletModel(string name,decimal balance, WalletEnumType type) {
            Name = name;
            Type = type;
            Balance = balance;
        }

    }
}
