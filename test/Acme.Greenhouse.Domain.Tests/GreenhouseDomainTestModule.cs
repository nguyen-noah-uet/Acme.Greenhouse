using Volo.Abp.Modularity;

namespace Acme.Greenhouse;

[DependsOn(
    typeof(GreenhouseDomainModule),
    typeof(GreenhouseTestBaseModule)
)]
public class GreenhouseDomainTestModule : AbpModule
{

}
