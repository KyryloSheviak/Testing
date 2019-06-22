using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Testing.Domain.Entitys;
using Testing.Domain.Interface;

namespace Testing.Domain.Repository
{
    public class TestingRepositoryAdmin : IRepositoryAdmin
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<ApplicationUser> GetUsers()
        {
            return context.Users.Where(u => !u.isDelete);
        }

        public void BlockUser(string id)
        {
            var t = context.Users.Find(id).isBlock;
            context.Users.Find(id).isBlock = !t;
            context.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            context.Users.Find(id).isDelete = true;
            context.SaveChanges();
        }

        public IQueryable<Test> GetTests()
        {
            return context.Tests.Where(t => !t.isDelete);
        }

        public Test GetTest(int? id)
        {
            return context.Tests.Find(id);
        }

        public void DeleteTest(int? id)
        {
            context.Tests.Find(id).isDelete = true;
            context.SaveChanges();
        }

        public IDbSet<Сomplexity> GetСomplexity()
        {
            return context.Сomplexitys;
        }

        public void AddTest(Test test)
        {
            context.Tests.Add(test);
            context.SaveChanges();
        }

        public void EditTestInformation(Test test)
        {
            context.Tests.Find(test.Id).Subject = test.Subject;
            context.Tests.Find(test.Id).TimeToGo = test.TimeToGo;
            context.Tests.Find(test.Id).СomplexityId = test.СomplexityId;
            context.SaveChanges();
        }

        public int AddQuestion(string name, string idtest, List<string> answers, List<string> ans)
        {
            int testID = Convert.ToInt32(idtest);
            var obj = new Question
            {
                TestId = Convert.ToInt32(idtest),
                UserQuestion = name
            };
            context.Questions.Add(obj);
            context.SaveChanges();

            int id = obj.Id;

            var Answers = new List<Answer>();
            for (int i = 0; i < answers.Count; i++)
            {
                Answers.Add(new Answer
                {
                    QuestionId = id,
                    UserAnswer = answers[i].ToString(),
                    IsCorrect = ans[i] == "1" ? true : false
                });
            }

            Answers.ForEach(t => context.Answers.Add(t));
            context.SaveChanges();

            var countQuestions = context.Tests.Find(testID).Questions.Count();
            context.Tests.Find(testID).CountQuestions = countQuestions;
            context.SaveChanges();

            return countQuestions;
        }
    }
}
