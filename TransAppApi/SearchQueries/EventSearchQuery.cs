using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.SearchQueries
{
    public class EventSearchQuery : EntitiesSearchQuery
    {
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
    }
}