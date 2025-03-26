using AutoMapper;
using Business.Services.EmployeeServices;
using Data.DTOs.EmployeeDtos;
using Data.DTOs.InterviewDtos;
using Data.DTOs.InterviewNotesDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.InterviewServices
{
    public class InterviewService : IInterviewService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InterviewService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterviewDto>> GetAllInterviewsAsync()
        {
            var interviews = await _context.Interviews.ToListAsync();
            return _mapper.Map<IEnumerable<InterviewDto>>(interviews);
        }

        public async Task<InterviewDto> GetInterviewAsync(int id)
        {
            var interview = await _context.Interviews.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<InterviewDto>(interview);
        }

        public async Task<InterviewDto> CreateInterviewAsync(InterviewCreateDto interviewCreateDto)
        {
            var interview = _mapper.Map<Interview>(interviewCreateDto);
            interview.isDeleted = false;
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();
            return _mapper.Map<InterviewDto>(interview);
        }

        public async Task<InterviewDto> UpdateInterviewAsync(int id, InterviewDto interviewDto)
        {
            var existingInterview = await _context.Interviews.FirstOrDefaultAsync(a => a.Id == id);
            if (existingInterview == null)
            {
                throw new Exception("Interview not found");
            }

            _mapper.Map(interviewDto, existingInterview);
            await _context.SaveChangesAsync();
            return _mapper.Map<InterviewDto>(existingInterview);
        }

        public async Task<bool> DeleteInterviewAsync(int id)
        {
            var interview = await _context.Interviews.FirstOrDefaultAsync(a => a.Id == id);
            if (interview == null)
            {
                return false;
            }

            interview.isDeleted = true; // Set isDeleted to true
            //_context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InterviewDto>> SearchInterviewsByIdAsync(int id)
        {
            var interviews = await _context.Interviews.Where(p => p.Id==id).ToListAsync();
            return _mapper.Map<IEnumerable<InterviewDto>>(interviews);

        }

        public async Task<IEnumerable<InterviewDto>> SearchInterviewsByStartDateAsync(DateTime startDate)
        {
            var interviews = await _context.Interviews
                .Where(interview => interview.startDate.Date == startDate.Date) // Adjust the condition based on your actual data model
                .ToListAsync();

            return _mapper.Map<IEnumerable<InterviewDto>>(interviews);
        }
    }
}
