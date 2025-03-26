using Data.DTOs.InterviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.InterviewNotesDtos
{
    public class InterviewNotesCreateDto
    {
        public string note { get; set; }
        public bool isPassed { get; set; }
        public int InterviewId { get; set; }
    }
}
