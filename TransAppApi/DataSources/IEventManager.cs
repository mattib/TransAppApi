using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Models;

namespace TransAppApi.DataSources
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