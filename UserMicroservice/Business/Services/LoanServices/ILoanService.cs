using Data.DTOs.LoanDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.LoanServices
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<LoanDto> GetLoanAsync(int id);
        Task<LoanDto> CreateLoanAsync(LoanCreateDto loanCreateDto);
        Task<LoanDto> UpdateLoanAsync(int id, LoanDto loanDto);
        Task<bool> DeleteLoanAsync(int id);

        Task<IEnumerable<LoanDto>> SearchLoansByCostumerAsync(string planetName);
    }
}
