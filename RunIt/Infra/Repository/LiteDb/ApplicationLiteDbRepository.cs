using RunIt.Entity;
using RunIt.Repository;
using RunIt.Infra.Repository.LiteDb.Base;
using LiteDB;
using System;

namespace RunIt.Infra.Repository.LiteDb
{
    public class ApplicationLiteDbRepository : LiteDbRepository<Application>, IApplicationRepository
    {
        protected override void DeleteByKey(LiteRepository db, string key)
        {
            db.Delete<Application>(app => app.Name == key);
        }

        public Application GetByAliasOrName(string aliasOrName)
        {
            using (var db = CreateNewConnection())
            {
                var app = db.Query<Application>().Where(a => a.Alias == aliasOrName).FirstOrDefault();
                if (app == null)
                    app = db.Query<Application>().Where(a => a.Name == aliasOrName).FirstOrDefault();
                return app;
            }
        }
    }
}
