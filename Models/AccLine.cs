using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizard.Models
{
    public class AccLine
    {
        public int ItemId { get; set; }
        public int LineId { get; set; }
        public int SubCenterNo { get; set; }
        public double Amount { get; set; }
    }
}
