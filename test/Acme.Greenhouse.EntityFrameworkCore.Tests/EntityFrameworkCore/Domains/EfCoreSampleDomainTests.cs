using Acme.Greenhouse.Samples;
using Xunit;

namespace Acme.Greenhouse.EntityFrameworkCore.Domains;

[Collection(GreenhouseTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<GreenhouseEntityFrameworkCoreTestModule>
{

}
