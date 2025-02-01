using Microsoft.AspNetCore.Mvc;
using SafeGate.Models;
using System.Diagnostics;

namespace SafeGate.Controllers
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
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");
            ViewData["Cognome"] = HttpContext.Session.GetString("Cognome");
            ViewData["Ruolo"] = HttpContext.Session.GetString("Ruolo");
            ViewData["Id_Funzionario"] = HttpContext.Session.GetString("Id_Funzionario");
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