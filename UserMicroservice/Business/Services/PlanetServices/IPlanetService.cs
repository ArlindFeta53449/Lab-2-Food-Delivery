using Data.DTOs.PlanetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.PlanetServices
{
    public interface IPlanetService
    {
        Task<IEnumerable<PlanetDto>> GetAllPlanetsAsync();
        Task<PlanetDto> GetPlanetAsync(int id);
        Task<PlanetDto> CreatePlanetAsync(PlanetCreateDto planetCreateDto);
        Task<PlanetDto> UpdatePlanetAsync(int id, PlanetDto planetDto);
        Task<bool> DeletePlanetAsync(int id);

        Task<IEnumerable<PlanetDto>> SearchPlanetsByNameAsync(string name);

    }
}
