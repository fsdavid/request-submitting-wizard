using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizard.Models
{
    public class LineItem
    {
        public int LineId { get; set; }
        public int RequestId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }        
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public double BudgetObject { get; set; }
        public double Amount { get; set; }
        public AccLine[] AccLines { get; set; }
    }
}
