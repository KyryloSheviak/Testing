using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Testing.Domain.Entitys;

namespace Testing.Domain.Interface
{
    public interface IRepositoryAdmin
    {
        /// <summary>
        /// Получение всех не удаленных юзеров
        /// </summary>
        /// <returns></returns>
        IQueryable<ApplicationUser> GetUsers();

        /// <summary>
        /// Блокировка / разблокировка пользователя
        /// </summary>
        /// <param name="id">id юзера</param>
        void BlockUser(string id);

        /// <summary>
        /// Удаления юзера
        /// </summary>
        /// <param name="id">id юзера</param>
        void DeleteUser(string id);

        /// <summary>
        /// Получение не удаленных тестов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Test> GetTests();

        /// <summary>
        /// Soft delete теста
        /// </summary>
        /// <param name="id">id теста</param>
        void DeleteTest(int? id);

        /// <summary>
        /// Получение уровней сложности
        /// </summary>
        /// <returns></returns>
        IDbSet<Сomplexity> GetСomplexity();

        /// <summary>
        /// Добавление нового теста
        /// </summary>
        /// <param name="test"></param>
        void AddTest(Test test);

        /// <summary>
        /// Получение объекта Тест
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Test GetTest(int? id);

        /// <summary>
        /// Редактирование информации о тесте
        /// </summary>
        /// <param name="test"></param>
        void EditTestInformation(Test test);

        /// <summary>
        /// Добавление вопроса
        /// </summary>
        /// <param name="name">Вопрос</param>
        /// <param name="idtest">id теста</param>
        /// <param name="answers">коллекция ответов</param>
        /// <param name="ans">коллекция ответов по вопросу, правильный или нет</param>
        /// <returns></returns>
        int AddQuestion(string name, string idtest, List<string> answers, List<string> ans);

        /// <summary>
        /// Получение кол-ва тестов
        /// </summary>
        /// <returns></returns>
        int CountTests();

        /// <summary>
        /// Получения кол-ва юзеров
        /// </summary>
        /// <returns></returns>
        int CountUsers();
    }
}
