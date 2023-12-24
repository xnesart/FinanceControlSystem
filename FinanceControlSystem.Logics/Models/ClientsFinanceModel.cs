using FinanceControlSystem.Logics.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControlSystem.Logics.Models
{
    public  class ClientsFinanceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Сurrency { get; set; }

        public decimal Balance { get; set; }

        public string Desciption { get; set; }

        public ClientsFinanceType Type { get; set; }

    }
}
