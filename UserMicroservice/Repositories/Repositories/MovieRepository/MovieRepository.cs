using Data.Entities;
using Repositories.Repositories.ChildRepositories;
using Repositories.Repositories.GenericRepository;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.MovieRepository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
         public MovieRepository(AppDbContext context) : base(context)
         {

         }
    }
}
