using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Filters;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebQuery.Models;

namespace WebQuery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dataString = CustomQueryStringHelper.EncryptString("","Profile","Home",new{profileId="1",ApplicationId="MAX-1"});
            // return LocalRedirect(dataString);
            return View();
        }
        [DecryptQueryStringParameter]
        public IActionResult Profile(string profileId, string applicationId)
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
