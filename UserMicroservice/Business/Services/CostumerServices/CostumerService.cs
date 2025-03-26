using AutoMapper;
using Business.Services.CostumerServices;
using Data.DTOs.CostumerDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.CostumerServices
{
    public class CostumerService : ICostumerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CostumerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CostumerDto>> GetAllCostumersAsync()
        {
            var costumers = await _context.Costumers.ToListAsync();
            return _mapper.Map<IEnumerable<CostumerDto>>(costumers);
        }

        public async Task<CostumerDto> GetCostumerAsync(int id)
        {
            var costumer = await _context.Costumers.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<CostumerDto>(costumer);
        }

        public async Task<CostumerDto> CreateCostumerAsync(CostumerCreateDto costumerCreateDto)
        {
            var costumer = _mapper.Map<Costumer>(costumerCreateDto);
            _context.Costumers.Add(costumer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CostumerDto>(costumer);
        }

        public async Task<CostumerDto> UpdateCostumerAsync(int id, CostumerDto costumerDto)
        {
            var existingCostumer = await _context.Costumers.FirstOrDefaultAsync(a => a.Id == id);
            if (existingCostumer == null)
            {
                throw new Exception("Costumer not found");
            }

            _mapper.Map(costumerDto, existingCostumer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CostumerDto>(existingCostumer);
        }

        public async Task<bool> DeleteCostumerAsync(int id)
        {
            var costumer = await _context.Costumers.FirstOrDefaultAsync(a => a.Id == id);
            if (costumer == null)
            {
                return false;
            }

            _context.Costumers.Remove(costumer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CostumerDto>> SearchCostumersByNameAsync(string name)
        {
            var costumers = await _context.Costumers.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<IEnumerable<CostumerDto>>(costumers);

        }
    }
}
