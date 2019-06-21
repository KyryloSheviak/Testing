using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Testing.Domain.Entitys;

namespace Testing.Web.Models
{
    public class TestsViewModel
    {
        public IEnumerable<string> Subject { get; set; }
        public IEnumerable<Test> Test { get; set; }
    }
}