using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Testing.Domain.Entitys
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Test : BaseEntity
    {
        [Display(Name = "Предмет")]
        public string Subject { get; set; } // предмет

        [Display(Name = "Время на прохождение")]
        public int TimeToGo { get; set; } // время на прохождение

        [Display(Name = "Кол-во вопросов")]
        public int CountQuestions { get; set; } // количество вопросов
        public bool isDelete { get; set; }
        public int СomplexityId { get; set; }
        public virtual Сomplexity Сomplexity { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }

    // уровень сложности
    public class Сomplexity : BaseEntity 
    {
        [Display(Name = "Сложность")]
        public string Complication { get; set; } // сложность
        public virtual ICollection<Test> Tests { get; set; }
    }

    // вопросы
    public class Question : BaseEntity
    {
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        public string UserQuestion { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }

    // ответы на вопросы
    public class Answer : BaseEntity
    {
        public string UserAnswer { get; set; } // ответ на вопрос
        public bool IsCorrect { get; set; } // правильный ответ или нет
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }

    public class AnswersUsers : BaseEntity
    {
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Test Test { get; set; }

        public virtual Question Question { get; set; }

        public virtual Answer Answer { get; set; }

        public string AnswerUser { get; set; }
    }


}
