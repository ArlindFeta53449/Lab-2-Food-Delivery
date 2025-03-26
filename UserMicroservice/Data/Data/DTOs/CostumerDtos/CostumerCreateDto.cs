using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.CostumerDtos
{
    public class CostumerCreateDto
    {
        public string Name { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
