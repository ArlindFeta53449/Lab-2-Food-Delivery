using Data.DTOs.CostumerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;


namespace Data.DTOs.LoanDtos
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public bool isDeleted { get; set; }
        public int CostumerId { get; set; }
        public CostumerDto Costumer { get; set; }
    }
}
