using CountryCreator.Data.Models;

namespace CountryCreator.Services.Interfaces
{
    public interface ICountryService
    {
        string AddCountry(Country country);

        string EditCountry(Country country);

        IQueryable<Country> GetCountries();

        string GetCountryName(string id);

        Country GetCountry(Guid id);
    }
}
