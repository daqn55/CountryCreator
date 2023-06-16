using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryCreator.Data.Models
{
    public class City
    {
        public Guid Id { get; set; }

        public string CityName { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
