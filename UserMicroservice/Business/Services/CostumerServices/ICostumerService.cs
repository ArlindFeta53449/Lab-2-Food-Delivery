using Data.DTOs.CostumerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.CostumerServices
{
    public interface ICostumerService
    {
        Task<IEnumerable<CostumerDto>> GetAllCostumersAsync();
        Task<CostumerDto> GetCostumerAsync(int id);
        Task<CostumerDto> CreateCostumerAsync(CostumerCreateDto costumerCreateDto);
        Task<CostumerDto> UpdateCostumerAsync(int id, CostumerDto costumerDto);
        Task<bool> DeleteCostumerAsync(int id);

        Task<IEnumerable<CostumerDto>> SearchCostumersByNameAsync(string name);
    }
}
