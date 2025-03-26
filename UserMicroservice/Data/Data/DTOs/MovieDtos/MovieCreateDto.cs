using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.MovieDtos
{
    public class MovieCreateDto
    {
        public string Title { get; set; }
        public string ReleaseYear { get; set; }

        public int DirectorId { get; set; }
    }
}


