using AutoMapper;
using Business.Services.PlanetServices;
using Data.DTOs.PlanetDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.PlanetServices
{
    public class PlanetService : IPlanetService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlanetService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlanetDto>> GetAllPlanetsAsync()
        {
            var planets = await _context.Planets.ToListAsync();
            return _mapper.Map<IEnumerable<PlanetDto>>(planets);
        }

        public async Task<PlanetDto> GetPlanetAsync(int id)
        {
            var planet = await _context.Planets.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<PlanetDto>(planet);
        }

        public async Task<PlanetDto> CreatePlanetAsync(PlanetCreateDto planetCreateDto)
        {
            var planet = _mapper.Map<Planet>(planetCreateDto);
            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanetDto>(planet);
        }

        public async Task<PlanetDto> UpdatePlanetAsync(int id, PlanetDto planetDto)
        {
            var existingPlanet = await _context.Planets.FirstOrDefaultAsync(a => a.Id == id);
            if (existingPlanet == null)
            {
                throw new Exception("Planet not found");
            }

            _mapper.Map(planetDto, existingPlanet);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanetDto>(existingPlanet);
        }

        public async Task<bool> DeletePlanetAsync(int id)
        {
            var planet = await _context.Planets.FirstOrDefaultAsync(a => a.Id == id);
            if (planet == null)
            {
                return false;
            }

            _context.Planets.Remove(planet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PlanetDto>> SearchPlanetsByNameAsync(string name)
        {
            var planets = await _context.Planets.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<IEnumerable<PlanetDto>>(planets);

        }
    }
}
