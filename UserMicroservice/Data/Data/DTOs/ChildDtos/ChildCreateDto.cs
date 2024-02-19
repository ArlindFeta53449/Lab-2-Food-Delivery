using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.ChildDtos
{
    public class ChildCreateDto
    {
        public string Name { get; set; }
        public string Difficulty { get; set; }

        public int ParentId { get; set; }
    }
}
