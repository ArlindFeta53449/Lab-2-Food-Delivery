using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ContractDtos
{
    public class ContractCreateDto
    {
        public String Name {  get; set; }
        public DateTime StartDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
