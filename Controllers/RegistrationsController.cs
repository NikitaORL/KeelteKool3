using KeelteKool.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace KeelteKool.Controllers
{
    [Authorize] // любой пользователь
    public class RegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Register(int id) // id = TrainingId
        {
            string userId = User.Identity.GetUserId();

            // Проверка: уже зарегистрирован
            var alreadyRegistered = db.Registrations
                .Any(r => r.TrainingId == id && r.ApplicationUserId == userId);

            if (alreadyRegistered)
            {
                TempData["Message"] = "Sa oled juba regestreeritud!";
                return RedirectToAction("Index", "MinuKursused"); // на свои курсы
            }

            // Проверка лимита участников
            var training = db.Trainings.Find(id);
            if (training.Registrations.Count() >= training.MaxOsalejaid)
            {
                TempData["Message"] = "Nii palju õpilased!";
                return RedirectToAction("Index", "MinuKursused"); // на свои курсы
            }

            // Создаём новую регистрацию
            var registration = new Registration
            {
                TrainingId = id,
                ApplicationUserId = userId,
                Staatus = "Ootel" // статус "В ожидании"
            };

            db.Registrations.Add(registration);
            db.SaveChanges();
            return RedirectToAction("Index", "MinuKursused"); // на свои курсы
        }


        /*Мину курс*/
        public ActionResult Unregister(int id) // id = TrainingId
        {
            string userId = User.Identity.GetUserId();

            var registration = db.Registrations
                .FirstOrDefault(r => r.TrainingId == id && r.ApplicationUserId == userId);

            if (registration != null)
            {
                db.Registrations.Remove(registration);
                db.SaveChanges();
            }
            else
            {
                TempData["Message"] = "Viga";
            }

            return RedirectToAction("Index", "MinuKursused");
        }


    }
}
