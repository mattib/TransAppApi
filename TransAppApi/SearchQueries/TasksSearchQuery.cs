using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.SearchQueries
{
    public class TasksSearchQuery : EntitiesSearchQuery
    {
        public int? UserId { get; set; }
        public int? CompanyId { get; set; }
        public string DeliveryNumber { get; set; }
    }
}