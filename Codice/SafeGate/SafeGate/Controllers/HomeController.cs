using Microsoft.AspNetCore.Mvc;
using SafeGate.Models;
using System.Diagnostics;
using System.Drawing.Printing;
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

        public JsonResult GetCheckPoints()
        {
            Database db = new Database();
            return Json(new { success = true, data = db.GetCheckPoints() });
        }

        public JsonResult InsertCheckPoint(string nome, string descrizione, string tipo)
        {
            Database db = new Database();
            int id = db.InsertCheckPoint(nome, descrizione, tipo);
            return Json(new { success = true, id = id});
        }

        [HttpPost]
        public JsonResult UpdateCheckPoint(int id, string campo, string nuovovalore)
        {
            Database db = new Database();
            bool success = db.UpdateCheckPoint(id, campo, nuovovalore);
            return Json(new { success = success });
        }

        public JsonResult DeleteCheckPoint(int id)
        {
            Database db = new Database();
            bool success = db.DeleteCheckPoint(id);
            return Json(new { success = success });
        }

        public JsonResult GetOfficers()
        {
            Database db = new Database();
            return Json(new { success = true, data = db.GetOfficers() });
        }

        public JsonResult InsertOfficer(string nome, string cognome, string username, string password, string ruolo)
        {
            Database db = new Database();
            int id = db.InsertOfficer(nome, cognome, username, password, ruolo);
            return Json(new { success = true, id = id });
        }

        [HttpPost]
        public JsonResult UpdateOfficer(int id, string campo, string nuovovalore)
        {
            Database db = new Database();
            bool success = db.UpdateOfficer(id, campo, nuovovalore);
            return Json(new { success = success });
        }

        public JsonResult DeleteOfficer(int id)
        {
            Database db = new Database();
            bool success = db.DeleteOfficer(id);
            return Json(new { success = success });
        }

        public JsonResult GetAttendants()
        {
            Database db = new Database();
            var attendants = db.GetAttendants();
            return Json(new { success = true, data = attendants });
        }


        public JsonResult InsertAttendant(string nome, string cognome, string username, string password, string ruolo, int idFunzionario)
        {
            Database db = new Database();
            int id = db.InsertAttendant(nome, cognome, username, password, ruolo, idFunzionario);
            return Json(new { success = true, id = id });
        }

        [HttpPost]
        public JsonResult UpdateAttendant(int id, string campo, string nuovovalore)
        {
            Database db = new Database();
            bool success = db.UpdateAttendant(id, campo, nuovovalore);
            return Json(new { success = success });
        }


        public JsonResult DeleteAttendant(int id)
        {
            Database db = new Database();
            bool success = db.DeleteAttendant(id);
            return Json(new { success = success });
        }

        public JsonResult GetCategories()
        {
            Database db = new Database();
            var categories = db.GetCategories();
            return Json(new { success = true, data = categories });
        }

        public JsonResult InsertCategory(string nome, string descrizione)
        {
            Database db = new Database();
            int id = db.InsertCategory(nome, descrizione);
            return Json(new { success = true, id = id });
        }

        [HttpPost]
        public JsonResult UpdateCategory(int id, string campo, string nuovovalore)
        {
            Database db = new Database();
            bool success = db.UpdateCategory(id, campo, nuovovalore);
            return Json(new { success = success });
        }

    }
}