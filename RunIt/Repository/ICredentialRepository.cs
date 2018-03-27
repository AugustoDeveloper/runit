using RunIt.Entity;
using RunIt.Repository.Base;

namespace RunIt.Repository
{
    public interface ICredentialRepository : IRepository<Credential>
    {
        Credential GetByName(string credentialName);
    }
}
