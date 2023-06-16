using CountryCreator.Data.Models;
using CountryCreator.Services.Interfaces;
using CountryCreator.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountryCreator.Web.Controllers
{
    [Authorize]
    public class CountryController : Controller
    {
        private const string EditCountryErrorMSG = "A country with this id does not exist!";

        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string countryName)
        {
            var country = new Country
            {
                CountryName = countryName,
            };

            var resultMSG = _countryService.AddCountry(country);

            TempData["result"] = resultMSG;

            return Redirect("../Home/Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var country = _countryService.GetCountry(Guid.Parse(id));
            if (country == null)
            {
                TempData["result"] = EditCountryErrorMSG;
                return Redirect("../../");
            }

            var countryModel = new CountryViewModel
            {
                CountryName = country.CountryName,
                Id = country.Id,
            };


            return View(countryModel);
        }

        [HttpPost]
        public IActionResult Edit(CountryViewModel model)
        {
            var country = _countryService.GetCountry(model.Id);

            country.CountryName = model.CountryName;

            var resultMsg = _countryService.EditCountry(country);

            TempData["result"] = resultMsg;

            return Redirect("../../");
        }
    }
}
