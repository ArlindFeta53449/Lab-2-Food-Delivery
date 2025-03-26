using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public LoanStatus Status { get; set; }  // Change to LoanStatus enum
        public bool isDeleted { get; set; }
        public int CostumerId { get; set; }
        public Costumer Costumer { get; set; }
    }

    // Enum definition for loan status
    public enum LoanStatus
    {
        Accepted,
        Cancelled,
        Pending
    }
}
