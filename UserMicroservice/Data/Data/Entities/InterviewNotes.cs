using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class InterviewNotes
    {
        public int Id { get; set; }
        public string note { get; set; }
        public bool isPassed { get; set; }
        public int InterviewId { get; set; }
        public Interview Interview { get; set; }
    }
}
