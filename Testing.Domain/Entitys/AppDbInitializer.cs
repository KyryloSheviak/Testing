using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Testing.Domain.Config;
using System.Collections.Generic;

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

            // ======================================================


            var complexity = new List<Сomplexity>
            {
                new Сomplexity { Complication = "базовый" },
                new Сomplexity { Complication = "повышенный" },
                new Сomplexity { Complication = "высокий" }
            };
            complexity.ForEach(c => context.Сomplexitys.Add(c));
            context.SaveChanges();

            var test = new List<Test>
            {
                new Test {
                    Subject = "Математика",
                    TimeToGo = 10,
                    CountQuestions = 5,
                    СomplexityId = 1,
                },
                new Test {
                    Subject = "Математика",
                    TimeToGo = 15,
                    CountQuestions = 5,
                    СomplexityId = 1
                },
                new Test {
                    Subject = "Математика",
                    TimeToGo = 20,
                    CountQuestions = 25,
                    СomplexityId = 2
                },
                new Test {
                    Subject = "Физика",
                    TimeToGo = 10,
                    CountQuestions = 5,
                    СomplexityId = 2
                },
                new Test {
                    Subject = "Физика",
                    TimeToGo = 20,
                    CountQuestions = 10,
                    СomplexityId = 3
                }
            };
            test.ForEach(t => context.Tests.Add(t));
            context.SaveChanges();

            var questions = new List<Question>
            {
                new Question { TestId = 1, UserQuestion = "2+2" },
                new Question { TestId = 1, UserQuestion = "3+3" },
                new Question { TestId = 1, UserQuestion = "2-2" },
                new Question { TestId = 1, UserQuestion = "5*2" },
                new Question { TestId = 1, UserQuestion = "5+8" },
                // второй тест
                new Question { TestId = 2, UserQuestion = "2*2+2" },
                new Question { TestId = 2, UserQuestion = "3+3-0" },
                new Question { TestId = 2, UserQuestion = "2/2*2" },
                new Question { TestId = 2, UserQuestion = "5*2+1" },
                new Question { TestId = 2, UserQuestion = "5+8*2" },
            };
            questions.ForEach(t => context.Questions.Add(t));
            context.SaveChanges();

            var answers = new List<Answer>
            {
                new Answer { QuestionId = 1, UserAnswer = "4", IsCorrect = true },
                new Answer { QuestionId = 1, UserAnswer = "5", IsCorrect = false },
                new Answer { QuestionId = 2, UserAnswer = "6", IsCorrect = true },
                new Answer { QuestionId = 2, UserAnswer = "4", IsCorrect = false },
                new Answer { QuestionId = 3, UserAnswer = "0", IsCorrect = true },
                new Answer { QuestionId = 3, UserAnswer = "3", IsCorrect = false },
                new Answer { QuestionId = 4, UserAnswer = "10", IsCorrect = true },
                new Answer { QuestionId = 4, UserAnswer = "12", IsCorrect = false },
                new Answer { QuestionId = 5, UserAnswer = "13", IsCorrect = true },
                new Answer { QuestionId = 5, UserAnswer = "17", IsCorrect = false },
            };
            answers.ForEach(t => context.Answers.Add(t));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
