using System.Collections.Generic;
using RunIt.Entity;

namespace RunIt.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);
        void Delete(string key);
        void Update(TEntity entity);
        List<TEntity> LoadAll();        
    }
}
