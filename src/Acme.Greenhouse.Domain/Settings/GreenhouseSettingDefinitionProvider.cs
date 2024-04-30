using Volo.Abp.Settings;

namespace Acme.Greenhouse.Settings;

public class GreenhouseSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(GreenhouseSettings.MySetting1));
    }
}
