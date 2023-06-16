using CountryCreator.Services.Interfaces;
using CountryCreator.Web.Paging;
using CountryCreator.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CountryCreator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICountryService _countryService;

        public HomeController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            if (pageNumber == null)
            {
                pageNumber = 1;
            }

            var countries = _countryService.GetCountries()
                                           .Select(c => new CountryViewModel
                                           {
                                               Id = c.Id,
                                               CountryName = c.CountryName,
                                           });

            int pageSize = 3;
            return View(await PaginatedList<CountryViewModel>.CreateAsync(countries.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}