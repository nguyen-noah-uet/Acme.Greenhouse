using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.Nodes
{
    [Serializable]
    public class NodeStatusDto : CreationAuditedEntityDto<int>
    {
        public int NodeId { get; set; } = default!;
        public bool IsOnline { get; set; }
    }
}
