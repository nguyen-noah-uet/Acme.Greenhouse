using System;
using System.Collections.Generic;
using System.Text;
using Acme.Greenhouse.Localization;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse;

/* Inherit your application services from this class.
 */
public abstract class GreenhouseAppService : ApplicationService
{
    protected GreenhouseAppService()
    {
        LocalizationResource = typeof(GreenhouseResource);
    }
}
