using AutoMapper;
using Data.DTOs.MovieDtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;


namespace Business.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _context.Movies
                .Include(b => b.Director) // Include the Director property
                .ToListAsync();

            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _context.Movies
                .Include(b => b.Director) // Include the Parent property
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto)
        {

            var existingDirector = await _context.Directors.FirstOrDefaultAsync(a => a.Id == movieCreateDto.DirectorId);
            if (existingDirector == null)
            {
                throw new Exception("Director not found");
            }

            var movie = _mapper.Map<Movie>(movieCreateDto);

            movie.Director = existingDirector;

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMovieAsync(int id, MovieDto movieDto)
        {
            var existingMovie = await _context.Movies.FirstOrDefaultAsync(b => b.Id == id);
            if (existingMovie == null)
            {
                throw new Exception("Movie not found");
            }

            _mapper.Map(movieDto, existingMovie);
            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDto>(existingMovie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(b => b.Id == id);
            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MovieDto>> SearchMovieByReleaseYearAsync(string directorName)
        {
            var movies = await _context.Movies
                    .Where(movie => movie.Director.Name.Contains(directorName))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);

        }

        public async Task<IEnumerable<MovieDto>> SearchMovieByDirectorAsync(string name)
        {
            var movies = await _context.Movies
                    .Where(movie => movie.Director.Name.Contains(name))
                    .ToListAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);

        }
    }
}

