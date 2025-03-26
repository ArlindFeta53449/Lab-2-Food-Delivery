using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.SatelliteDtos
{
    public class SatelliteCreateDto
    {
        public string Name { get; set; }
        public bool isDeleted { get; set; }

        public int PlanetId { get; set; }
    }
}
