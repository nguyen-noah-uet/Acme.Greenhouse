using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Greenhouse.Nodes
{
    [Serializable]
    public class NodeCreateUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
