using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiki_Dapper.Models.DTO
{
    public class StatisticsDTO
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string CreatorLogin { get; set; }
        public int Comments { get; set; }
        public int Contributors { get; set; }
    }
}
