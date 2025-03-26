using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthYear { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
}

