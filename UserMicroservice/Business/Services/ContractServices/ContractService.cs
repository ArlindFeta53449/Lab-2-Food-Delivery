using AutoMapper;
using Data.DTOs.ContractDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.ContractServices
{
    public class ContractService : IContractService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContractService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractDto>> GetAllContractsAsync()
        {
            var contracts = await _context.Contracts
                .Include(b => b.Employee) // Include the Parent property
                .ToListAsync();

            return _mapper.Map<IEnumerable<ContractDto>>(contracts);
        }

        public async Task<ContractDto> GetContractAsync(int id)
        {
            var contract = await _context.Contracts
                .Include(b => b.Employee) // Include the Parent property
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<ContractDto>(contract);
        }

        public async Task<ContractDto> CreateContractAsync(ContractCreateDto contractCreateDto)
        {

            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == contractCreateDto.EmployeeId);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            var contract = _mapper.Map<Contract>(contractCreateDto);

            contract.Employee = existingEmployee;

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContractDto>(contract);
        }

        public async Task<ContractDto> UpdateContractAsync(int id, ContractDto contractDto)
        {
            var existingContract = await _context.Contracts.FirstOrDefaultAsync(b => b.Id == id);
            if (existingContract == null)
            {
                throw new Exception("Contract not found");
            }

            _mapper.Map(contractDto, existingContract);
            await _context.SaveChangesAsync();
            return _mapper.Map<ContractDto>(existingContract);
        }

        public async Task<bool> DeleteContractAsync(int id)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(b => b.Id == id);
            if (contract == null)
            {
                return false;
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ContractDto>> SearchContractsByStartDateAsync(DateTime startDate)
        {
            var contracts = await _context.Contracts
                .Where(contract => contract.StartDate.Date == startDate.Date) // Adjust the condition based on your actual data model
                .ToListAsync();

            return _mapper.Map<IEnumerable<ContractDto>>(contracts);
        }


        public async Task<IEnumerable<ContractDto>> SearchContractsByEmployeeAsync(string name)
        {
            var contracts = await _context.Contracts
                    .Where(contract => contract.Employee.Name.Contains(name))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<ContractDto>>(contracts);

        }
    }
}
