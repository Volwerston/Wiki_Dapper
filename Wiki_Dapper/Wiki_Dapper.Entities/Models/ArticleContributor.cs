using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki_Dapper.Entities.Models
{
    public class ArticleContributor
    {
        public int Id { get; set; }
        
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public string ContributorId { get; set; }
        public ApplicationUser Contributor { get; set; }

        public DateTime ContributionTime { get; set; }
    }
}
