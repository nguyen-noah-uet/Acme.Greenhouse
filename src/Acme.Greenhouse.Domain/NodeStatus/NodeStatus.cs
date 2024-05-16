using Acme.Greenhouse.Nodes;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Greenhouse.NodeStatus
{
    public class NodeStatus : CreationAuditedEntity<int>
    {
        public int NodeId { get; set; }
        public Node Node { get; set; } = default!;
        public bool IsOnline { get; set; }
    }
}
