using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public bool isActive { get; set; }
        public ICollection<Contract> Contracts { get; set;}
    }
}
