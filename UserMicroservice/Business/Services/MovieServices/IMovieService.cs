using Data.DTOs.MovieDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.MovieServices
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto> GetMovieAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieCreateDto);
        Task<MovieDto> UpdateMovieAsync(int id, MovieDto movieDto);
        Task<bool> DeleteMovieAsync(int id);


        Task<IEnumerable<MovieDto>> SearchMovieByReleaseYearAsync(string releaseYear);
        Task<IEnumerable<MovieDto>> SearchMovieByDirectorAsync(string directorName);
    }
}

