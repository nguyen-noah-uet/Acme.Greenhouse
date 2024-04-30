using Volo.Abp.Modularity;

namespace Acme.Greenhouse;

public abstract class GreenhouseApplicationTestBase<TStartupModule> : GreenhouseTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
