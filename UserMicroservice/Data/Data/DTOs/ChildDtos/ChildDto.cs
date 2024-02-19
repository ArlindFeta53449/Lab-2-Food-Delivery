using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs.ParentDtos;
using Data.DTOs.Payment;

namespace Data.DTOs.ChildDtos
{
    public class ChildDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public int ParentId { get; set; }
        public ParentDto Parent { get; set; }
    }
}
