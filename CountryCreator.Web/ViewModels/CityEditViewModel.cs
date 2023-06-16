namespace CountryCreator.Web.ViewModels
{
    public class CityEditViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public IQueryable<CountryViewModel> Countries { get; set; }
    }
}
