using AutoMapper;
using Business.Services.LoanServices;
using Data.DTOs.LoanDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Services.LoanServices
{
    public class LoanService : ILoanService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LoanService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _context.Loans
                .Include(b => b.Costumer) // Include the Parent property
                .ToListAsync();

            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<LoanDto> GetLoanAsync(int id)
        {
            var loan = await _context.Loans
                .Include(b => b.Costumer) // Include the Parent property
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> CreateLoanAsync(LoanCreateDto loanCreateDto)
        {

            var existingCostumer = await _context.Costumers.FirstOrDefaultAsync(a => a.Id == loanCreateDto.CostumerId);
            if (existingCostumer == null)
            {
                throw new Exception("Costumer not found");
            }

            var loan = _mapper.Map<Loan>(loanCreateDto);

            loan.Costumer = existingCostumer;
            loan.isDeleted = false; // Set isDeleted to false by default

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> UpdateLoanAsync(int id, LoanDto loanDto)
        {
            var existingLoan = await _context.Loans.FirstOrDefaultAsync(b => b.Id == id);
            if (existingLoan == null)
            {
                throw new Exception("Loan not found");
            }

            _mapper.Map(loanDto, existingLoan);
            await _context.SaveChangesAsync();
            return _mapper.Map<LoanDto>(existingLoan);
        }
        public async Task<bool> DeleteLoanAsync(int id)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(a => a.Id == id);
            if (loan == null)
            {
                return false;
            }

            loan.isDeleted = true; // Set isDeleted to true
            //_context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LoanDto>> SearchLoansByCostumerAsync(string costumerName)
        {
            var loans = await _context.Loans
                    .Where(loan => loan.Costumer.Name.Contains(costumerName))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);

        }

    }
}
