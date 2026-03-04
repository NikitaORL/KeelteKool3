using System.Linq;
using System.Web.Mvc;
using KeelteKool.Models;
using Microsoft.AspNet.Identity; 

public class CoursesController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // Страница для учеников
    [Authorize(Roles = "Opilane")]
    public ActionResult MyCourses()
    {
        string userId = User.Identity.GetUserId();

        var myRegistrations = db.Registrations
            .Where(r => r.ApplicationUserId == userId && r.Staatus == "Kinnitatud")
            .Select(r => r.Training) 
            .ToList();

        return View(myRegistrations);
    }
}
