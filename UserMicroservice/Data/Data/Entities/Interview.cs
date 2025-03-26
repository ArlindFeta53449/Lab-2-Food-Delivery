using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Interview
    {
        public int Id { get; set; }
        public DateTime startDate { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<InterviewNotes> InterviewNotes { get; set; }
    }
}
