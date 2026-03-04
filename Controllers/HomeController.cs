using KeelteKool.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity; // Include


namespace KeelteKool.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous] // чтобы любой мог смотреть
        public ActionResult Index()
        {
            ViewBag.Message = "Avaleht";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Kursused";

            var trainings = db.Trainings
                .Include(t => t.Course)
                .Include(t => t.Teacher)
                .Include(t => t.Registrations)
                .ToList();
            return View(trainings);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Õpetajad";
            var teacher = db.Teachers.ToList();
            return View(teacher);
        }
    }
}
