using CountryCreator.Data.Models;

namespace CountryCreator.Services.Interfaces
{
    public interface ICityService
    {
        string AddCity(City city);

        string EditCity(City city);

        IQueryable<City> GetCities(string countryId);

        City GetCity(string id);
    }
}
