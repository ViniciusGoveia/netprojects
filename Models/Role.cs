using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public List<User> Users { get; set; }
    }
}
