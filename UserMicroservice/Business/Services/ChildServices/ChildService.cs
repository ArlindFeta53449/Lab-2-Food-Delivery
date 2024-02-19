
using AutoMapper;
using Data.DTOs.ChildDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Business.Services.ChildServices
{
    public class ChildService : IChildService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ChildService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChildDto>> GetAllChildrenAsync()
        {
            var children = await _context.Children
                .Include(b => b.Parent) // Include the Parent property
                .ToListAsync();

            return _mapper.Map<IEnumerable<ChildDto>>(children);
        }

        public async Task<ChildDto> GetChildAsync(int id)
        {
            var child = await _context.Children
                .Include(b => b.Parent) // Include the Parent property
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<ChildDto>(child);
        }

        public async Task<ChildDto> CreateChildAsync(ChildCreateDto childCreateDto)
        {

            var existingParent = await _context.Parents.FirstOrDefaultAsync(a => a.Id == childCreateDto.ParentId);
            if (existingParent == null)
            {
                throw new Exception("Parent not found");
            }

            var child = _mapper.Map<Child>(childCreateDto);

            child.Parent = existingParent;

            _context.Children.Add(child);
            await _context.SaveChangesAsync();

            return _mapper.Map<ChildDto>(child);
        }

        public async Task<ChildDto> UpdateChildAsync(int id, ChildDto childDto)
        {
            var existingChild = await _context.Children.FirstOrDefaultAsync(b => b.Id == id);
            if (existingChild == null)
            {
                throw new Exception("Child not found");
            }

            _mapper.Map(childDto, existingChild);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChildDto>(existingChild);
        }

        public async Task<bool> DeleteChildAsync(int id)
        {
            var child = await _context.Children.FirstOrDefaultAsync(b => b.Id == id);
            if (child == null)
            {
                return false;
            }

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ChildDto>> SearchChildrenByDifficultyAsync(string parentName)
        {
            var children = await _context.Children
                    .Where(child => child.Parent.Name.Contains(parentName))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<ChildDto>>(children);

        }

        public async Task<IEnumerable<ChildDto>> SearchChildrenByParentAsync(string name)
        {
            var children = await _context.Children
                    .Where(child => child.Parent.Name.Contains(name))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<ChildDto>>(children);

        }

    }
}
