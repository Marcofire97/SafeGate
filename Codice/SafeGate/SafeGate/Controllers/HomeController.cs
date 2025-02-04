using Microsoft.AspNetCore.Mvc;
using SafeGate.Models;
using System.Diagnostics;
using System.IO;

namespace SafeGate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            ViewData["Nome"] = HttpContext.Session.GetString("Nome");
            ViewData["Cognome"] = HttpContext.Session.GetString("Cognome");
            ViewData["Ruolo"] = HttpContext.Session.GetString("Ruolo");
            ViewData["Id_Funzionario"] = HttpContext.Session.GetString("Id_Funzionario");
            return View();
        }

        // Questo metodo risponde all'ajax per il caricamento delle sezioni
        public JsonResult GetSection(string section)
        {
            string path = Path.Combine("Views", "Home", "Sections", section + ".cshtml"); //Il path del file da caricare

            Console.WriteLine(section);

            if (System.IO.File.Exists(path)) //Se il file esiste
            {
                string content = System.IO.File.ReadAllText(path); // Legge il contenuto del file

                // Restituisce il contenuto del file
                return Json(new
                {
                    success = true,
                    data = content
                });
            }
            else
            {
                // Se il file non esiste, restituisce un messaggio di errore
                return Json(new
                {
                    success = false,
                    data = $@"<div class='error-message text-center p-4'>
                                <i class='bi bi-exclamation-triangle-fill text-danger' style='font-size: 2rem;'></i>
                                <h2 class='mt-3 text-danger'>Errore nel caricamento della sezione {section}</h2>
                                <p class='text-muted'>Il conenuto richiesto non esiste. Riprova più tardi.</p>
                                </div>"
                });
            }
        }

        public JsonResult GetStatistics()
        {
            Database db = new Database();
            Statistica stats = db.GetStatistics();
            return Json(new
            {
                Numero_Controlli_Totali = stats.numero_Controlli_Totali,
                Numero_Passeggeri_Controllati = stats.numero_Passeggeri_Controllati,
                Merci_Respinte_Sequestrate = stats.merci_Respinte_Sequestrate,
                Totale_Dazi_Riscossi = stats.totale_Dazi_Riscossi,
                Merci_Dichiarate = stats.numero_Merci_Dichiarate,
                Numero_Punti_Controllo = stats.numero_Punti_Controllo
            });
        }
    }
}