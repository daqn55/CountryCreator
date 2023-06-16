using CountryCreator.Data.Models;
using CountryCreator.Services.Interfaces;
using CountryCreator.Web.Paging;
using CountryCreator.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryCreator.Web.Controllers
{
    public class CityController : Controller
    {
        private const string EditCityErrorMSG = "A city with this id does not exist!";

        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CityController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            var countries = _countryService.GetCountries();

            return View(countries);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCityViewModel model)
        {
            var city = new City
            {
                CityName = model.CityName,
                CountryId = Guid.Parse(model.CountryId),
            };

            var resultMSG = _cityService.AddCity(city);

            TempData["result"] = resultMSG;

            return Redirect("../Home/Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var city = _cityService.GetCity(id);
            if (city == null)
            {
                TempData["result"] = EditCityErrorMSG;
                return Redirect("../../");
            }

            var countryId = city.CountryId;

            var cityViewModel = new CityEditViewModel
            {
                Id = id,
                Name = city.CityName,
                CountryId = countryId.ToString(),
                CountryName = _countryService.GetCountryName(countryId.ToString()),
                Countries = _countryService.GetCountries()
                                           .Where(c => c.Id != countryId)
                                           .Select(c => new CountryViewModel 
                                           { 
                                               CountryName = c.CountryName,
                                               Id = c.Id}
                                           ),
            };

            return View(cityViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CityEditToDbViewModel model)
        {
            var city = _cityService.GetCity(model.Id);
            city.CityName = model.CityName;
            city.CountryId = Guid.Parse(model.CountryId);

            var resultMsg = _cityService.EditCity(city);
            TempData["result"] = resultMsg;

            return Redirect("../../");
        }


        public async Task<IActionResult> ViewCities(string id, int? pageNumber)
        {
            if (pageNumber == null)
            {
                pageNumber = 1;
            }

            var countryName = _countryService.GetCountryName(id);
            ViewData["countryName"] = countryName;

            var cities = _cityService.GetCities(id).Select(c => new CityViewModel
            {
                CityName = c.CityName,
                Id = c.Id,
            });

            int pageSize = 3;
            return View(await PaginatedList<CityViewModel>.CreateAsync(cities.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}
