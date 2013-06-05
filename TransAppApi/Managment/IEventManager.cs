using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.SearchQueries;

namespace TransAppApi.Managment
{
    public interface IEventManager
    {
        Event[] GetEvents(EventSearchQuery eventSearchQuery);

        Event GetEvent(int id);

        Event[] GetUserEvents(int userId, DateTime? lastModified = null);

        void SaveEvents(Event[] events);

        void DeleteEvent(int id);
    }
}