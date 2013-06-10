using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.SearchQueries
{
    public class UsersSearchQuery : EntitiesSearchQuery
    {
        public int? CompanyId { get; set; }
    }
}