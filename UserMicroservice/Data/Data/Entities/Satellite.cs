using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Satellite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isDeleted { get; set; }


        public int PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}
