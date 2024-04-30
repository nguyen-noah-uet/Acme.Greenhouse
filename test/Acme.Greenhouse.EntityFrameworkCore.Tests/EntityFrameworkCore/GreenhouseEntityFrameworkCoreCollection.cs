using Xunit;

namespace Acme.Greenhouse.EntityFrameworkCore;

[CollectionDefinition(GreenhouseTestConsts.CollectionDefinitionName)]
public class GreenhouseEntityFrameworkCoreCollection : ICollectionFixture<GreenhouseEntityFrameworkCoreFixture>
{

}
