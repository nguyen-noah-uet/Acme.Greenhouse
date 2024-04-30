using Volo.Abp.Modularity;

namespace Acme.Greenhouse;

/* Inherit from this class for your domain layer tests. */
public abstract class GreenhouseDomainTestBase<TStartupModule> : GreenhouseTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
