using Data.DTOs.SatelliteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.SatelliteServices
{
    public interface ISatelliteService
    {
        Task<IEnumerable<SatelliteDto>> GetAllSatellitesAsync();
        Task<SatelliteDto> GetSatelliteAsync(int id);
        Task<SatelliteDto> CreateSatelliteAsync(SatelliteCreateDto satelliteCreateDto);
        Task<SatelliteDto> UpdateSatelliteAsync(int id, SatelliteDto satelliteDto);
        Task<bool> DeleteSatelliteAsync(int id);

        Task<IEnumerable<SatelliteDto>> SearchSatellitesByPlanetAsync(string planetName);
    }
}
