using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.DTOs.ParentDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Business.Services.ParentServices
{
    public class ParentService : IParentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ParentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParentDto>> GetAllParentsAsync()
        {
            var parents = await _context.Parents.ToListAsync();
            return _mapper.Map<IEnumerable<ParentDto>>(parents);
        }

        public async Task<ParentDto> GetParentAsync(int id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<ParentDto>(parent);
        }

        public async Task<ParentDto> CreateParentAsync(ParentCreateDto parentCreateDto)
        {
            var parent = _mapper.Map<Parent>(parentCreateDto);
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
            return _mapper.Map<ParentDto>(parent);
        }

        public async Task<ParentDto> UpdateParentAsync(int id, ParentDto parentDto)
        {
            var existingParent = await _context.Parents.FirstOrDefaultAsync(a => a.Id == id);
            if (existingParent == null)
            {
                throw new Exception("Parent not found");
            }

            _mapper.Map(parentDto, existingParent);
            await _context.SaveChangesAsync();
            return _mapper.Map<ParentDto>(existingParent);
        }

        public async Task<bool> DeleteParentAsync(int id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(a => a.Id == id);
            if (parent == null)
            {
                return false;
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ParentDto>> SearchParentsByNameAsync(string name)
        {
            var parents = await _context.Parents.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<IEnumerable<ParentDto>>(parents);

        }
    }
}
