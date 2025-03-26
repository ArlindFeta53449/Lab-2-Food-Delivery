using AutoMapper;
using Business.Services.SatelliteServices;
using Data.DTOs.SatelliteDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Services.SatelliteServices
{
    public class SatelliteService : ISatelliteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SatelliteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SatelliteDto>> GetAllSatellitesAsync()
        {
            var satellites = await _context.Satellites
                .Include(b => b.Planet) // Include the Parent property
                .ToListAsync();

            return _mapper.Map<IEnumerable<SatelliteDto>>(satellites);
        }

        public async Task<SatelliteDto> GetSatelliteAsync(int id)
        {
            var satellite = await _context.Satellites
                .Include(b => b.Planet) // Include the Parent property
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<SatelliteDto>(satellite);
        }

        public async Task<SatelliteDto> CreateSatelliteAsync(SatelliteCreateDto satelliteCreateDto)
        {

            var existingPlanet = await _context.Planets.FirstOrDefaultAsync(a => a.Id == satelliteCreateDto.PlanetId);
            if (existingPlanet == null)
            {
                throw new Exception("Planet not found");
            }

            var satellite = _mapper.Map<Satellite>(satelliteCreateDto);

            satellite.Planet = existingPlanet;
            satellite.isDeleted = false; // Set isDeleted to false by default

            _context.Satellites.Add(satellite);
            await _context.SaveChangesAsync();

            return _mapper.Map<SatelliteDto>(satellite);
        }

        public async Task<SatelliteDto> UpdateSatelliteAsync(int id, SatelliteDto satelliteDto)
        {
            var existingSatellite = await _context.Satellites.FirstOrDefaultAsync(b => b.Id == id);
            if (existingSatellite == null)
            {
                throw new Exception("Satellite not found");
            }

            _mapper.Map(satelliteDto, existingSatellite);
            await _context.SaveChangesAsync();
            return _mapper.Map<SatelliteDto>(existingSatellite);
        }
        public async Task<bool> DeleteSatelliteAsync(int id)
        {
            var satellite = await _context.Satellites.FirstOrDefaultAsync(a => a.Id == id);
            if (satellite == null)
            {
                return false;
            }

            satellite.isDeleted = true; // Set isDeleted to true
            //_context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SatelliteDto>> SearchSatellitesByPlanetAsync(string planetName)
        {
            var satellites = await _context.Satellites
                    .Where(satellite => satellite.Planet.Name.Contains(planetName))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<SatelliteDto>>(satellites);

        }

    }
}
