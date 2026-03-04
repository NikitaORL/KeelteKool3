namespace KeelteKool.Migrations
{
    using KeelteKool.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KeelteKool.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KeelteKool.Models.ApplicationDbContext context)
        {
            // =========================
            // 1. ROLE & USER MANAGERS
            // =========================
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // =========================
            // 2. ROLES (без Opilane)
            // =========================
            string[] roles = { "Admin", "Opetaja" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            // =========================
            // 3. ADMIN
            // =========================
            CreateUserIfNotExists(userManager, "admin@keelkool.ee", "Admin123!", "Admin");

            // =========================
            // 4. TEACHERS (USERS)
            // =========================
            CreateUserIfNotExists(userManager, "alina@gmail.com", "Alina123!", "Opetaja");
            CreateUserIfNotExists(userManager, "kristina@gmail.com", "Kristina123!", "Opetaja");
            CreateUserIfNotExists(userManager, "oleg@gmail.com", "Oleg123!"); 
            CreateUserIfNotExists(userManager, "test@gmail.com", "Test123!"); 

            context.SaveChanges(); 

            // =========================
            // 5. TEACHERS (TABLE)
            // =========================
            AddTeacherIfNotExists(context, "alina@gmail.com", "Alina Saal", "Keeleõpetaja", "Images/alina.png");
            AddTeacherIfNotExists(context, "kristina@gmail.com", "Kristina Kris", "Keeleõpetaja", "Images/kristina.png");
            AddTeacherIfNotExists(context, "oleg@gmail.com", "Oleg Bullet", "Keeleõpetaja", "Images/oleg.png");
            AddTeacherIfNotExists(context, "test@gmail.com", "Test Õpetaja", "Keeleõpetaja", "Images/test.png");

            context.SaveChanges(); // 🔥 ВАЖНО

            // =========================
            // 6. COURSES
            // =========================
            context.Courses.AddOrUpdate(
                c => c.Nimetus,
                new Course { Nimetus = "Eesti keel A1", Keel = "Eesti", Tase = "A1", Kirjeldus = "Algajatele" },
                new Course { Nimetus = "Eesti keel A2", Keel = "Eesti", Tase = "A2", Kirjeldus = "Põhitase" },
                new Course { Nimetus = "Inglise keel B1", Keel = "English", Tase = "B1", Kirjeldus = "Kesktase" },
                new Course { Nimetus = "Vene keel A2", Keel = "Русский", Tase = "A2", Kirjeldus = "Algajatele+" }
            );

            context.SaveChanges(); // 🔥 ВАЖНО

            // =========================
            // 7. TRAININGS
            // =========================
            var teacher1 = context.Teachers.First(t => t.Nimi == "Alina Saal");
            var teacher2 = context.Teachers.First(t => t.Nimi == "Kristina Kris");

            var course1 = context.Courses.First(c => c.Nimetus == "Eesti keel A1");
            var course2 = context.Courses.First(c => c.Nimetus == "Inglise keel B1");

            context.Trainings.AddOrUpdate(
                t => new { t.CourseId, t.TeacherId },
                new Training
                {
                    CourseId = course1.Id,
                    TeacherId = teacher1.Id,
                    AlgusKuupaev = DateTime.Today.AddDays(3),
                    LoppKuupaev = DateTime.Today.AddDays(30),
                    Hind = 100,
                    MaxOsalejaid = 12
                },
                new Training
                {
                    CourseId = course2.Id,
                    TeacherId = teacher2.Id,
                    AlgusKuupaev = DateTime.Today.AddDays(5),
                    LoppKuupaev = DateTime.Today.AddDays(35),
                    Hind = 120,
                    MaxOsalejaid = 12
                }
            );

            context.SaveChanges();
        }

        // =========================
        // HELPERS
        // =========================

        private void CreateUserIfNotExists(UserManager<ApplicationUser> userManager, string email, string password, string role = null)
        {
            var user = userManager.FindByEmail(email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = email, Email = email };
                userManager.Create(user, password);
            }

            if (!string.IsNullOrEmpty(role) && !userManager.IsInRole(user.Id, role))
            {
                userManager.AddToRole(user.Id, role);
            }
        }

        private void AddTeacherIfNotExists(ApplicationDbContext context, string email, string nimi, string kval, string foto)
        {
            var user = context.Users.First(u => u.Email == email);

            if (!context.Teachers.Any(t => t.ApplicationUserId == user.Id))
            {
                context.Teachers.Add(new Teacher
                {
                    Nimi = nimi,
                    Kvalifikatsioon = kval,
                    FotoPath = foto,
                    ApplicationUserId = user.Id
                });
            }
        }
    }
}
