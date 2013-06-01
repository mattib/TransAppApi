using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Models;

namespace TransAppApi.DataSources
{
    public interface IEventsDataSource
    {
        Event[] GetAll();

        Event GetEvent(int Id);
    }
}