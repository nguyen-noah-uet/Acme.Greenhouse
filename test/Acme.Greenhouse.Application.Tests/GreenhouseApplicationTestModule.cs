using Volo.Abp.Modularity;

namespace Acme.Greenhouse;

[DependsOn(
    typeof(GreenhouseApplicationModule),
    typeof(GreenhouseDomainTestModule)
)]
public class GreenhouseApplicationTestModule : AbpModule
{

}
