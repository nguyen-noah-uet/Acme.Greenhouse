using Acme.Greenhouse.Nodes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.NodeStatus
{
    public interface INodeStatusService : ICrudAppService<NodeStatusDto, int, PagedAndSortedResultRequestDto, NodeStatusCreateDto>
    {
    }
}
