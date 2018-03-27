using System.Collections.Generic;
using RunIt.Entity;
using RunIt.Repository.Base;
using LiteDB;
using System.Configuration;

namespace RunIt.Infra.Repository.LiteDb.Base
{
    public abstract class LiteDbRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private string _connectionString;
        protected LiteDbRepository(string connectionStringName = "LiteDbConnectionSetting")
        {
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        protected LiteRepository CreateNewConnection()
        {
            return new LiteRepository(_connectionString);
        }

        public void Add(TEntity entity)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                db.Insert(entity);
            }
        }

        public void Delete(string key)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                DeleteByKey(db, key);
            }
        }

        protected abstract void DeleteByKey(LiteRepository db, string key);

        public List<TEntity> LoadAll()
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return db.Query<TEntity>().ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                db.Update(entity);
            }
        }        
    }
}
