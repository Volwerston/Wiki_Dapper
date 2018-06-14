using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki_Dapper.Entities.Models
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ApplicationUser> ArticleContributors { get; set; }

        public Article()
        {
            ArticleContributors = new HashSet<ApplicationUser>();
        }
    }
}
