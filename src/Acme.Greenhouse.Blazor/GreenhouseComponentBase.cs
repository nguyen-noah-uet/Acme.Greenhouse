using Acme.Greenhouse.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Acme.Greenhouse.Blazor;

public abstract class GreenhouseComponentBase : AbpComponentBase
{
    protected GreenhouseComponentBase()
    {
        LocalizationResource = typeof(GreenhouseResource);
    }
}
