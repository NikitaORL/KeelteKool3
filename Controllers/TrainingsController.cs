using KeelteKool.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace KeelteKool.Controllers
{
    [Authorize] // доступ только для авторизованных пользователей
    public class TrainingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainings — список всех тренингов
        public ActionResult Index()
        {
            var trainings = db.Trainings.Include(t => t.Course).ToList();
            return View(trainings);
        }

        // GET: Trainings/Details/5 — детали тренинга + участники
        public ActionResult Details(int id)
        {
            var training = db.Trainings
                            .Include(t => t.Course)  // информация о курсе
                            .Include(t => t.Registrations.Select(r => r.ApplicationUser)) // информация об учениках
                            .FirstOrDefault(t => t.Id == id);

            if (training == null) return HttpNotFound();

            return View(training); // <-- отдает Details.cshtml через контроллер
        }
    }
}