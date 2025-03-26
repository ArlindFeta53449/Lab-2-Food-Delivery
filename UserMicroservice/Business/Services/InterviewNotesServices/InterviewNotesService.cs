using AutoMapper;
using Business.Services.InterviewNotesServices;
using Data.DTOs.InterviewNotesDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.InterviewNotesServices
{
    public class InterviewNotesService : IInterviewNotesService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InterviewNotesService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterviewNotesDto>> GetAllInterviewNotesAsync()
        {
            var interviewNotes = await _context.InterviewsNotes
                .Include(b => b.Interview) // Include the Parent property
                .ToListAsync();

            return _mapper.Map<IEnumerable<InterviewNotesDto>>(interviewNotes);
        }

        public async Task<InterviewNotesDto> GetInterviewNotesAsync(int id)
        {
            var interviewNotes = await _context.InterviewsNotes
                .Include(b => b.Interview) // Include the Parent property
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<InterviewNotesDto>(interviewNotes);
        }

        public async Task<InterviewNotesDto> CreateInterviewNotesAsync(InterviewNotesCreateDto interviewNotesCreateDto)
        {

            var existingInterview = await _context.Interviews.FirstOrDefaultAsync(a => a.Id == interviewNotesCreateDto.InterviewId);
            if (existingInterview == null)
            {
                throw new Exception("InterviewNotes not found");
            }

            var interviewNotes = _mapper.Map<InterviewNotes>(interviewNotesCreateDto);

            interviewNotes.Interview = existingInterview;

            _context.InterviewsNotes.Add(interviewNotes);
            await _context.SaveChangesAsync();

            return _mapper.Map<InterviewNotesDto>(interviewNotes);
        }

        public async Task<InterviewNotesDto> UpdateInterviewNotesAsync(int id, InterviewNotesDto interviewNotesDto)
        {
            var existingInterviewNotes = await _context.InterviewsNotes.FirstOrDefaultAsync(b => b.Id == id);
            if (existingInterviewNotes == null)
            {
                throw new Exception("InterviewNotes not found");
            }

            _mapper.Map(interviewNotesDto, existingInterviewNotes);
            await _context.SaveChangesAsync();
            return _mapper.Map<InterviewNotesDto>(existingInterviewNotes);
        }

        public async Task<bool> DeleteInterviewNotesAsync(int id)
        {
            var interviewNotes = await _context.InterviewsNotes.FirstOrDefaultAsync(b => b.Id == id);
            if (interviewNotes == null)
            {
                return false;
            }

            _context.InterviewsNotes.Remove(interviewNotes);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InterviewNotesDto>> SearchInterviewNotesByInterviewAsync(int interviewId)
        {
            var interviewNotes = await _context.InterviewsNotes
                    .Where(interviewNotes => interviewNotes.Interview.Id == interviewId)
                    .ToListAsync();
            return _mapper.Map<IEnumerable<InterviewNotesDto>>(interviewNotes);

        }
    }
}
