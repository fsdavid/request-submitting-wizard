using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizard.Models
{
    public class ReturnRequests
    {
        public Request[] OwnRequests { get; set; }
        public Request[] RequestsToApprove { get; set; }
    }
}
