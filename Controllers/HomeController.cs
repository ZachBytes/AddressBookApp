using AddressBookApp.Models;
using AddressBookApp.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
namespace AddressBook.Controllers
{




    public class HomeController : Controller
    {
        private readonly AddressDbContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AddressDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactBrowser()
        {
            return View();
            //return View((IEnumerable<Contact>)_dbContext.Contacts.ToList());
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
