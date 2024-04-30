using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme.Greenhouse.Blazor;

[Dependency(ReplaceServices = true)]
public class GreenhouseBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "";
    public override string? LogoUrl => default;
}
