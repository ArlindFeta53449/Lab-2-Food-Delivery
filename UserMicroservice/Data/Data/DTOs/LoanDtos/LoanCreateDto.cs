using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.DTOs.LoanDtos
{
    public class LoanCreateDto
    {
        public int Amount { get; set; }
        public LoanStatus Status {  get; set; }
        public bool isDeleted { get; set; }

        public int CostumerId { get; set; }
    }
}
