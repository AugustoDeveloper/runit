using RunIt.Entity;
using RunIt.Repository;
using RunIt.Infra.Repository.LiteDb.Base;
using LiteDB;
using System;

namespace RunIt.Infra.Repository.LiteDb
{
    public class CredentialLiteDbRepository : LiteDbRepository<Credential>, ICredentialRepository
    {
        protected override void DeleteByKey(LiteRepository db, string key)
        {
            db.Delete<Credential>(c => c.Name == key);
        }

        public Credential GetByName(string credentialName)
        {
            using (var db = CreateNewConnection())
            {
                return db.Query<Credential>().Where(c => c.Name == credentialName).FirstOrDefault();
            }
        }
    }
}
