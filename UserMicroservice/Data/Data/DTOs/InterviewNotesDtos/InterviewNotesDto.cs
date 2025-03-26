
using Data.DTOs.InterviewDtos;

namespace Data.DTOs.InterviewNotesDtos
{
    public class InterviewNotesDto
    {
        public int Id { get; set; }
        public string note { get; set; }
        public bool isPassed { get; set; }
        public int InterviewId { get; set; }
        public InterviewDto Interview { get; set; }
    }
}
