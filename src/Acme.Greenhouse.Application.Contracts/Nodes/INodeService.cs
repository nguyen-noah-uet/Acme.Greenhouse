using Acme.Greenhouse.Devices;
using Acme.Greenhouse.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.Nodes
{
    public interface INodeService
        : ICrudAppService<NodeDto, int, PagedAndSortedResultRequestDto, NodeCreateUpdateDto, NodeCreateUpdateDto>
    {
        //Task<List<NodeDto>> GetListDetailedAsync();
    }
}
