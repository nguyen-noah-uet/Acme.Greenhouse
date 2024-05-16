using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.Nodes
{
    public class NodeService
        : CrudAppService<Node, NodeDto, int, PagedAndSortedResultRequestDto, NodeCreateUpdateDto, NodeCreateUpdateDto>,
        INodeService
    {
        private readonly IRepository<Node, int> repository;

        public NodeService(IRepository<Node, int> repository) : base(repository)
        {
            this.repository = repository;
        }
        //public async Task<List<NodeDto>> GetListDetailedAsync()
        //{
        //    var query = await repository.GetQueryableAsync();
        //    query.
        //}
    }
}
