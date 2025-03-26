using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.InterviewDtos
{
    public class InterviewCreateDto
    {
        public DateTime startDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
