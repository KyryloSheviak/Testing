using System.Collections.Generic;
using System.Linq;
using Testing.Domain.Entitys;
using Testing.Domain.Models;

namespace Testing.Domain.Interface
{
    public interface IRepositoryUser
    {
        /// <summary>
        /// Проверка заблокирован ли пользователь
        /// </summary>
        /// <param name="userName">Имя юзера в система (email)</param>
        /// <returns>true - заблокирован пользователь</returns>
        bool IsBlockUser(string userName);

        /// <summary>
        /// Метод возвращающий тесты непройденные юзером
        /// </summary>
        /// <param name="userName">Имя юзера в система (email)</param>
        /// <returns>Коллекция вопросов</returns>
        TestsViewModel GetTestForUser(string userName, string subject, string level);

        /// <summary>
        /// Метод выполняющий возврат первого вопроса и устанавливающий 
        /// отметку прохождения теста
        /// </summary>
        /// <param name="id">id вопроса</param>
        /// <param name="userName">Имя юзера в система (email)</param>
        /// <returns>Возврат: времени прохождения теста, название теста, кол-во вопросов, первый вопрос (объект)</returns>
        (int time, string subject, int countQuestions, Question question) StartTest(int? id, string userName);

        /// <summary>
        /// Возврат следующего вопроса
        /// </summary>
        /// <param name="idtest">id теста</param>
        /// <param name="numc">номер вопроса</param>
        /// <returns></returns>
        Question NextQuestion(int idtest, int numc);

        /// <summary>
        /// Возврат кол-ва вопросов по тесту
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int CountQuestions(int? id);

        /// <summary>
        /// Метод сохраняющий ответ юзера на вопрос
        /// </summary>
        /// <param name="idtest">id теста</param>
        /// <param name="idquest">id вопроса</param>
        /// <param name="idans">id ответа</param>
        /// <param name="userName">Имя юзера в система (email)</param>
        void SaveUserAnswer(int idtest, int idquest, List<int> idans, string userName);

        /// <summary>
        /// Вывод результата по тесту
        /// </summary>
        /// <param name="idtest">id теста</param>
        /// <param name="userName">Имя юзера в система (email)</param>
        /// <returns></returns>
        double Result(int? idtest, string userName);

        /// <summary>
        /// Результаты по всем тестам юзера
        /// </summary>
        /// <param name="userName">Имя юзера в система (email)</param>
        /// <returns></returns>
        IQueryable<TestResults> AllResults(string userName);

        /// <summary>
        /// Получение кол-ва доступных тестов для юзера
        /// </summary>
        /// <param name="userName">Имя юзера в система (email)</param>
        /// <returns></returns>
        int CountTests(string userName);

        TestsViewModel FirstOpenPage(string userName);


        bool Test(int? id, string userName);
    }
}
