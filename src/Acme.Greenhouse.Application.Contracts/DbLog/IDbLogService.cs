using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.DbLog
{
    public interface IDbLogService : ICrudAppService<DbLogDto, int, PagedAndSortedResultRequestDto, DbLogCreateDto>
    {
    }
}
