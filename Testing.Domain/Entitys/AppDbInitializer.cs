using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Testing.Domain.Config;

namespace Testing.Domain.Entitys
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
            }



            /*
            var t = new List<TestEntity>
                {
                    new TestEntity { Name = "Английский", Complexity = "hard"},
                    new TestEntity { Name = "математика", Complexity = "easy"},
                    new TestEntity { Name = "География", Complexity = "soso"},
                };

            t.ForEach(std => context.Test.Add(std));
            context.SaveChanges();
            */

            base.Seed(context);
        }
    }
}
