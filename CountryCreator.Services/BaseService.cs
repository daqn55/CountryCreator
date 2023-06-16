using CountryCreator.Data;

namespace CountryCreator.Services
{
    public abstract class BaseService
    {
        private readonly ApplicationDbContext _db;

        public BaseService(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool IsNameAvailable(string name, Guid id)
        {
            name = name.ToLower();

            var checkCountryName = _db.Countries.Where(c => c.Id != id).Any(c => c.CountryName.ToLower() == name);
            var checkCityName = _db.Cities.Where(c => c.Id != id).Any(c => c.CityName.ToLower() == name);

            if (!checkCityName && !checkCountryName)
            {
                return true;
            }

            return false;
        }
    }
}
