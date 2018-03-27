using RunIt.Entity;
using RunIt.Repository.Base;

namespace RunIt.Repository
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetByAliasOrName(string aliasOrName);
    }
}
