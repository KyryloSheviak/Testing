using System;
using System.Collections.Generic;
using System.Linq;
using Testing.Domain.Entitys;
using Testing.Domain.Interface;

namespace Testing.Domain.Repository
{
    public class TestingRepositoryUser : IRepositoryUser
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public bool IsBlockUser(string userName)
        {
            return context.Users.First(u => u.UserName == userName).isBlock;
        }

        public IQueryable<Test> GetTestForUser(string userName)
        {
            // получаем все тесты пройденные юзером
            var late = context
                .TestUsers
                .Where(x => x.ApplicationUserId == userName)
                .Select(s => s.TestId)
                .Distinct()
                .ToList();

            // возвращаем только те тесты которые пользователь не прощел
            var tests = context
                .Tests
                .Where(a => !a.isDelete && !late.Any(e => e == a.Id));

            return tests;
        }

        public (int time, string subject, int countQuestions, Question question) StartTest(int? id, string userName)
        {
            var test = context.Tests.Find(id);

            // заносим инфу о том что пользователь начал тест
            context.TestUsers.Add(new TestUser
            {
                TestId = (int)id,
                ApplicationUserId = userName,
                Date = DateTime.Now
            });
            context.SaveChanges();

            var t = test.TimeToGo;
            var s = test.Subject;
            var c = test.CountQuestions;
            int idquest = test.Questions.First().Id;
            // возвращаем первый вопрос к выбранному тесу
            var Question = new Question
            {
                Id = idquest, // id первого вопроса
                TestId = (int)id, // id теста
                UserQuestion = test.Questions.First().UserQuestion, // первый вопрос
                Answers = context.Answers.Where(p => p.QuestionId == idquest).ToList() // коллекция ответов по вопросу
            };

            return (time: t, subject: s, countQuestions: c, question: Question);
        }

        public int CountQuestions(int? id)
        {
            return context.Tests.Find(id).Questions.Count;
        }

        public void SaveUserAnswer(int idtest, int idquest, List<int> idans, string userName)
        {
            // сохраняем результаты в таблицу
            foreach (var item in idans)
            {
                context.AnswersUsers.Add(new AnswersUsers
                {
                    QuestionId = idquest,
                    AnswerId = item,
                    TestId = idtest,
                    ApplicationUserId = userName
                });
            }
            context.SaveChanges();

        }

        public Question NextQuestion(int idtest, int numc)
        {
            var test = context.Tests.Find(idtest);
            // получаем id след вопроса
            int idq = test.Questions.Skip(numc - 1).First().Id;

            var Question = new Question
            {
                Id = test.Questions.Skip(numc - 1).First().Id, // id вопроса
                TestId = (int)idtest, // id теста
                UserQuestion = test.Questions.Skip(numc - 1).First().UserQuestion, // вопрос
                Answers = context.Answers.Where(p => p.QuestionId == idq).ToList() // коллекция ответов по вопросу
            };

            return Question;
        }

        public double Result(int? idtest, string userName)
        {
            // получаем список id вопросов по выбранному тесту
            var ids = context
                .Questions
                .Where(q => q.TestId == idtest)
                .Select(i => i.Id)
                .ToList();
            // получаем количество правильных вопросов по тесту
            var correctAns = context
                .Answers
                .Where(a => ids.Any(r => r == a.QuestionId) && a.IsCorrect)
                .Count();

            // получили id ответов по тесту
            var y = context
                .AnswersUsers
                .Where(w => w.ApplicationUserId == userName && w.TestId == idtest)
                .Select(i => i.AnswerId);
            // получаем кол-во правильных ответов пользователем
            var h = context
                .Answers
                .Where(a => a.IsCorrect == true && y.Any(e => e == a.Id))
                .Count();

            // запись соотношения правильных ответов
            double t = (h * 100) / correctAns;
            context.TestUsers
                .First(u => u.ApplicationUserId == userName && u.TestId == idtest)
                .PercentTrueAns = t;

            context.SaveChanges();
            return t;
        }

        public IQueryable<TestResults> AllResults(string userName)
        {
            var res = context
            .TestUsers
            .Where(u => u.ApplicationUserId == userName)
            .Join(
                context.Tests,
                p => p.TestId,
                t => t.Id,
                (p, t) => new TestResults
                {
                    TestName = t.Subject,
                    PercentTrueAns = p.PercentTrueAns,
                    Date = p.Date
                }
            );

            return res;
        }
    }
}
