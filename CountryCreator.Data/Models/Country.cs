using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryCreator.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Cities = new List<City>();
        }

        public Guid Id { get; set; }

        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
