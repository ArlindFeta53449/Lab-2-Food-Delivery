using Data.DTOs.DirectorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.MovieDtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public int DirectorId { get; set; }
        public DirectorDto Director { get; set; }
    }
}
