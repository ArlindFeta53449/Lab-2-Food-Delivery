using AutoMapper;
using Business.Services.ParentServices;
using Data.DTOs.DirectorDtos;
using Data.DTOs.ParentDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.DirectorServices
{
    public class DirectorService : IDirectorService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DirectorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorDto>> GetAllDirectorsAsync()
        {
            var parents = await _context.Directors.ToListAsync();
            return _mapper.Map<IEnumerable<DirectorDto>>(parents);
        }

        public async Task<DirectorDto> GetDirectorAsync(int id)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<DirectorDto>(director);
        }

        public async Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto directorCreateDto)
        {
            var director = _mapper.Map<Director>(directorCreateDto);
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return _mapper.Map<DirectorDto>(director);
        }

        public async Task<DirectorDto> UpdateDirectorAsync(int id, DirectorDto directorDto)
        {
            var existingDirector = await _context.Directors.FirstOrDefaultAsync(a => a.Id == id);
            if (existingDirector == null)
            {
                throw new Exception("Parent not found");
            }

            _mapper.Map(directorDto, existingDirector);
            await _context.SaveChangesAsync();
            return _mapper.Map<DirectorDto>(existingDirector);
        }

        public async Task<bool> DeleteDirectorAsync(int id)
        {
            var director = await _context.Parents.FirstOrDefaultAsync(a => a.Id == id);
            if (director == null)
            {
                return false;
            }

            _context.Parents.Remove(director);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DirectorDto>> SearchDirectorsByNameAsync(string name)
        {
            var directors = await _context.Directors.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<IEnumerable<DirectorDto>>(directors);

        }
    }
}


