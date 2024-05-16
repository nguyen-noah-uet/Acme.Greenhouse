using Acme.Greenhouse.Nodes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.NodeStatus
{
    public class NodeStatusService
        : CrudAppService<NodeStatus, NodeStatusDto, int, PagedAndSortedResultRequestDto, NodeStatusCreateDto>,
        INodeStatusService
    {
        public NodeStatusService(IRepository<NodeStatus, int> repository) : base(repository)
        {
        }
    }
}
