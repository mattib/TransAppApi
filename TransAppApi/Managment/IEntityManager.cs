using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.SearchQueries;

namespace TransAppApi.Managment
{
    public interface IEntityManager<TEntity>
    {
        TEntity[] GetEntities(EntitiesSearchQuery searchQuery);

        TEntity GetEntity(int id);

        void SaveEntity(TEntity[] events);

        void DeleteEntity(int id);
    }
}