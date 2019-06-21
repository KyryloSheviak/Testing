using System.ComponentModel.DataAnnotations;
using Testing.Domain.Entitys;

namespace Testing.Web.Models
{
    public class TestsUser : TestUser
    {
        [Display(Name = "Предмет")]
        public string TestName { get; set; }
    }
}