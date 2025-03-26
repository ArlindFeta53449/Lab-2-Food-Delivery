using Data.DTOs.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.ContractServices
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllContractsAsync();
        Task<ContractDto> GetContractAsync(int id);
        Task<ContractDto> CreateContractAsync(ContractCreateDto contractCreateDto);
        Task<ContractDto> UpdateContractAsync(int id, ContractDto contractDto);
        Task<bool> DeleteContractAsync(int id);

        Task<IEnumerable<ContractDto>> SearchContractsByStartDateAsync(DateTime startDate);
        Task<IEnumerable<ContractDto>> SearchContractsByEmployeeAsync(string employeeName);
    }
}
