namespace MyFinances.Common.Core.Modules;

public static class ModuleRegistry
{
    private static readonly List<IModuleConfiguration> _moduleConfigurations = [];

    public static IEnumerable<IModuleConfiguration> ModuleConfigurations => _moduleConfigurations.AsReadOnly();

    public static void Add(IModuleConfiguration moduleDefinition)
    {
        _moduleConfigurations.Add(moduleDefinition);
    }
}