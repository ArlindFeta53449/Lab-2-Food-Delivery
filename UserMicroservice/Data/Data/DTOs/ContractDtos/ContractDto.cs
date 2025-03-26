using Data.DTOs.EmployeeDtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ContractDtos
{
    public class ContractDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime startDate { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }

        
    }
}
