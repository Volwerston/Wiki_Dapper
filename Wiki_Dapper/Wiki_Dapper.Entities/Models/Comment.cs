using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki_Dapper.Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public DateTime AddingTime { get; set; }
        public string Text { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
