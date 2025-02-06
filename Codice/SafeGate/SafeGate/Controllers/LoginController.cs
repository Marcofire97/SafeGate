using Microsoft.AspNetCore.Mvc;
using SafeGate.Models;
using System.Security.Cryptography;
using System.Text;

namespace SafeGate.Controllers
{
    public class LoginController : Controller
    {
        private readonly Database _database;

        public LoginController()
        {
            _database = new Database();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginValidation(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return Json(new { success = false, message = "Username e password sono richiesti" });
                }

                if (_database.ValidazioneFunzionario(username, password))
                {
                    Funzionario funzionario = _database.Login(username, password);

                    if (funzionario == null)
                    {
                        return Json(new { success = false, message = "Credenziali non valide" });
                    }

                    HttpContext.Session.SetString("Username", funzionario.Username);
                    HttpContext.Session.SetString("Nome", funzionario.Nome);
                    HttpContext.Session.SetString("Cognome", funzionario.Cognome);
                    HttpContext.Session.SetString("Ruolo", funzionario.Ruolo);
                    HttpContext.Session.SetString("Id_Funzionario", funzionario.Id_Funzionario.ToString());

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Username o password errati" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Si è verificato un errore: " + ex.Message });
            }
        }
    }
}
