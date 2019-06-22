using System;
using System.ComponentModel.DataAnnotations;

namespace Testing.Domain.Entitys
{
    public class TestResults
    { 
        [Display(Name = "Предмет")]
        public string TestName { get; set; }

        [Display(Name = "Результат")]
        public double PercentTrueAns { get; set; }

        [Display(Name = "Дата прохождения")]
        public DateTime Date { get; set; }
    }
}
