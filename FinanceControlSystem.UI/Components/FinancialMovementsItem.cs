using FinanceControlSystem.Logics.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControlSystem.UI.Components
{
    public class FinancialMovementsItem
    {
        public int ID { get; set; }
        public string Outcome { get; set; }
        public string Income { get; set; }
        public string Category { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    }
}
