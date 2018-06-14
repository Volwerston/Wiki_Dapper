using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki_Dapper.Entities.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Article> Articles { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public Category()
        {
            Articles = new HashSet<Article>();
        }
    }
}
