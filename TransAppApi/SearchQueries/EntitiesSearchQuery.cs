using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.SearchQueries
{
    public class EntitiesSearchQuery
    {
        public int? StartIndex { get; set; }
        public int? PageSize { get; set; }
        public DateTime? LastModified { get; set; }
        public int? RowStatus { get; set; }
    }
}