using CountryCreator.Data;
using CountryCreator.Data.Models;
using CountryCreator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace CountryCreator.Services
{
    public class CityService : BaseService, ICityService
    {
        private const string AddCityErrorMSG = "Country or city with the name \"{0}\" already exist.";
        private const string AddCitySuccessMSG = "\"{0}\" was added successfully";

        private const string EditCityErrorMSG = "Тhe city was not edited please try again";
        private const string EditCityErrorNameMSG = "Already exist country or city with that name \"{0}\"";
        private const string EditCitySuccessMSG = "City was edited successfully";

        private readonly ApplicationDbContext _db;

        public CityService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public string AddCity(City city)
        {
            var cityName = city.CityName;

            var isNameAvailable = IsNameAvailable(cityName, Guid.Empty);
            var resultMSG = string.Empty;

            if (isNameAvailable)
            {
                try
                {
                    _db.Cities.Add(city);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

                return string.Format(AddCitySuccessMSG, cityName);
            }

            return string.Format(AddCityErrorMSG, cityName);
        }

        public string EditCity(City city)
        {
            try
            {
                var isNameAvailable = IsNameAvailable(city.CityName, city.Id);

                if (isNameAvailable)
                {
                    _db.Cities.Update(city);
                    _db.SaveChanges();

                    return EditCitySuccessMSG;
                }

                return string.Format(EditCityErrorNameMSG, city.CityName);
            }
            catch (Exception)
            {
                return EditCityErrorMSG;
            }
        }

        public IQueryable<City> GetCities(string countryId)
        {
            var cities = _db.Cities.Where(c => c.CountryId == Guid.Parse(countryId));
            
            return cities;
        }

        public City GetCity(string id)
        {
            return _db.Cities.Include(c => c.Country).FirstOrDefault(c => c.Id == Guid.Parse(id));
        }
    }
}
