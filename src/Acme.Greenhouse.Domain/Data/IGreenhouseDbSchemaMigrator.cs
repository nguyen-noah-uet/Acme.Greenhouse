using System.Threading.Tasks;

namespace Acme.Greenhouse.Data;

public interface IGreenhouseDbSchemaMigrator
{
    Task MigrateAsync();
}
