using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.DbLog
{
    public class DbLogService : CrudAppService<DbLog, DbLogDto, int, PagedAndSortedResultRequestDto, DbLogCreateDto>,
        IDbLogService
    {
        public DbLogService(IRepository<DbLog, int> repository) : base(repository)
        {
        }
    }
}
