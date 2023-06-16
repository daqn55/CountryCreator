using CountryCreator.Data;
using CountryCreator.Data.Models;
using CountryCreator.Services.Interfaces;

namespace CountryCreator.Services
{
    public class CountryService : BaseService, ICountryService
    {
        private const string AddCountryErrorMSG = "Country or city with the name \"{0}\" already exist.";
        private const string AddCountrySuccessMSG = "\"{0}\" was added successfully";

        private const string EditCountryErrorMSG = "Тhe country was not edited please try again";
        private const string EditCountryErrorNameMSG = "Already exist country or city with that name \"{0}\"";
        private const string EditCountrySuccessMSG = "Country was edited successfully";

        private readonly ApplicationDbContext _db;
        public CountryService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public string AddCountry(Country country)
        {
            var countryName = country.CountryName;
            var isNameAvailable = IsNameAvailable(countryName, Guid.Empty);

            if (isNameAvailable)
            {
                try
                {
                    _db.Countries.Add(country);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

                return string.Format(AddCountrySuccessMSG, countryName);
            }

            return string.Format(AddCountryErrorMSG, countryName);
        }

        public string EditCountry(Country country)
        {
            try
            {
                var isNameAvailable = IsNameAvailable(country.CountryName, country.Id);

                if (isNameAvailable)
                {
                    _db.Countries.Update(country);
                    _db.SaveChanges();

                    return EditCountrySuccessMSG;
                }

                return string.Format(EditCountryErrorNameMSG, country.CountryName);
            }
            catch (Exception)
            {
                return EditCountryErrorMSG;
            }
        }

        public IQueryable<Country> GetCountries()
        {
            return _db.Countries;
        }

        public Country GetCountry(Guid id)
        {
            return _db.Countries.FirstOrDefault(c => c.Id == id);
        }

        public string GetCountryName(string id)
        {
            return _db.Countries.First(c => c.Id == Guid.Parse(id)).CountryName;
        }

        
    }
}
