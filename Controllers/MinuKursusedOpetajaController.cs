using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using KeelteKool.Models;

namespace KeelteKool.Controllers
{
    [Authorize(Roles = "Opetaja")]
    public class MinuKursusedOpetajaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult KursusedOpetaja()
        {
            string userId = User.Identity.GetUserId();

            var trainings = db.Trainings
                .Include("Course")
                .Include("Registrations")
                .Include("Teacher")
                .Where(t => t.Teacher.ApplicationUserId == userId)
                .ToList();

            return View(trainings);
        }
    }
}