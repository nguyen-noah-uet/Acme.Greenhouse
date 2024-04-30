using Acme.Greenhouse.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Acme.Greenhouse.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(GreenhouseEntityFrameworkCoreModule),
    typeof(GreenhouseApplicationContractsModule)
    )]
public class GreenhouseDbMigratorModule : AbpModule
{
}
