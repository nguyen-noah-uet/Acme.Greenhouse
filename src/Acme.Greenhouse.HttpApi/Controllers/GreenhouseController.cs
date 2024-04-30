using Acme.Greenhouse.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.Greenhouse.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class GreenhouseController : AbpControllerBase
{
    protected GreenhouseController()
    {
        LocalizationResource = typeof(GreenhouseResource);
    }
}
