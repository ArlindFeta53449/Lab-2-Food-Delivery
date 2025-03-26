using Data.DTOs.DirectorDtos;
using Data.DTOs.ParentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.DirectorServices
{
    public interface IDirectorService
    {
        Task<IEnumerable<DirectorDto>> GetAllDirectorsAsync();
        Task<DirectorDto> GetDirectorAsync(int id);
        Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto directorCreateDto);
        Task<DirectorDto> UpdateDirectorAsync(int id, DirectorDto directorDto);
        Task<bool> DeleteDirectorAsync(int id);

        Task<IEnumerable<DirectorDto>> SearchDirectorsByNameAsync(string name);
    }
}

