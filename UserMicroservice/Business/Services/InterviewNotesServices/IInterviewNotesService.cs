using Data.DTOs.InterviewNotesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.InterviewNotesServices
{
    public interface IInterviewNotesService
    {
        Task<IEnumerable<InterviewNotesDto>> GetAllInterviewNotesAsync();
        Task<InterviewNotesDto> GetInterviewNotesAsync(int id);
        Task<InterviewNotesDto> CreateInterviewNotesAsync(InterviewNotesCreateDto interviewNotesCreateDto);
        Task<InterviewNotesDto> UpdateInterviewNotesAsync(int id, InterviewNotesDto interviewNotesDto);
        Task<bool> DeleteInterviewNotesAsync(int id);
        Task<IEnumerable<InterviewNotesDto>> SearchInterviewNotesByInterviewAsync(int interviewId);
    }
}
