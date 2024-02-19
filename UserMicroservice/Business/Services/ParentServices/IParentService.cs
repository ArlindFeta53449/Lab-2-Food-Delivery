using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs.ParentDtos;


namespace Business.Services.ParentServices
{
    public interface IParentService
    {
        Task<IEnumerable<ParentDto>> GetAllParentsAsync();
        Task<ParentDto> GetParentAsync(int id);
        Task<ParentDto> CreateParentAsync(ParentCreateDto parentCreateDto);
        Task<ParentDto> UpdateParentAsync(int id, ParentDto parentDto);
        Task<bool> DeleteParentAsync(int id);

        Task<IEnumerable<ParentDto>> SearchParentsByNameAsync(string name);

    }
}
