using Acme.Greenhouse.Samples;
using Xunit;

namespace Acme.Greenhouse.EntityFrameworkCore.Applications;

[Collection(GreenhouseTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<GreenhouseEntityFrameworkCoreTestModule>
{

}
