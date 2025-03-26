using Data.DTOs.InterviewDtos;
using Data.DTOs.InterviewNotesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.InterviewServices
{
    public interface IInterviewService
    {
        Task<IEnumerable<InterviewDto>> GetAllInterviewsAsync();
        Task<InterviewDto> GetInterviewAsync(int id);
        Task<InterviewDto> CreateInterviewAsync(InterviewCreateDto interviewCreateDto);
        Task<InterviewDto> UpdateInterviewAsync(int id, InterviewDto interviewDto);
        Task<bool> DeleteInterviewAsync(int id);

        Task<IEnumerable<InterviewDto>> SearchInterviewsByStartDateAsync(DateTime startDate);
        Task<IEnumerable<InterviewDto>> SearchInterviewsByIdAsync(int id);
    }
}
    