using System.Collections.Generic;
using Testing.Domain.Entitys;

namespace Testing.Domain.Models
{
    public class TestsViewModel
    {
        public IEnumerable<string> Subject { get; set; } = new List<string>();
        public IEnumerable<string> Level { get; set; } = new List<string>();
        public IEnumerable<Test> Test { get; set; } = new List<Test>();
    }
}