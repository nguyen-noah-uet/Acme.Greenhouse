using Acme.Greenhouse.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Greenhouse.Nodes
{
    //public class NodeRepository : EfCoreRepository<IGreenhouseDbContext, Node>, INodeRepository
    //{
    //    public NodeRepository(IDbContextProvider<IGreenhouseDbContext> dbContextProvider) : base(dbContextProvider)
    //    {
    //    }

    //    public Task DeleteAsync(int id, bool autoSave = false, CancellationToken cancellationToken = default)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task DeleteManyAsync(IEnumerable<int> ids, bool autoSave = false, CancellationToken cancellationToken = default)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Node?> FindAsync(int id, bool includeDetails = true, CancellationToken cancellationToken = default)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Node> GetAsync(int id, bool includeDetails = true, CancellationToken cancellationToken = default)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
