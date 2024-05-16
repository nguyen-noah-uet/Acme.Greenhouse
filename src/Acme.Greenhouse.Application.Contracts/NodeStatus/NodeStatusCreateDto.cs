using System;

namespace Acme.Greenhouse.NodeStatus
{
    [Serializable]
    public class NodeStatusCreateDto
    {
        public int NodeId { get; set; } = default!;
        public bool IsOnline { get; set; }
    }
}
