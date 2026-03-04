using KeelteKool.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace KeelteKool.Controllers
{
    [Authorize] // Только авторизованные пользователи
    public class MinuKursusedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var trainings = db.Registrations
                .Where(r => r.ApplicationUserId == userId)
                .Select(r => r.Training)
                .Include(t => t.Course)   
                .Include(t => t.Teacher)  
                .ToList();

            return View("MyCourses", trainings);



        }
    }
}
