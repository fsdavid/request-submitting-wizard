using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wizard.Models
{
    public class Request
    {
        public int RequestNo { get; set; }
        public string Requester { get; set; }
        public string RequestOffice { get; set; }
        public string RequestOffice2 { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestStatus { get; set; }
        public string RequestType { get; set; }
        public DateTime RequiredDeliveryDate { get; set; }
        public string TechnicalContact { get; set; }
        public string Phone { get; set; }
        public bool TechnicalApprovalRequired { get; set; }
        public string Description { get; set; }
        public bool RenewalContract { get; set; }
        public string Supplier { get; set; }
        public LineItem[] LineItems { get; set; }
        public Note[] Notes { get; set; }
    }
}
