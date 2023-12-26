using FinanceControlSystem.Logics.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControlSystem.Logics.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Summ { get; set; }

        public DateTime Date { get; set; }

        public int ClientsFinanceId { get; set; }

        public int CategoryId { get; set; }

        public TransactionType Type { get; set; }

        public bool IsApproved { get; set; }
    }
}
