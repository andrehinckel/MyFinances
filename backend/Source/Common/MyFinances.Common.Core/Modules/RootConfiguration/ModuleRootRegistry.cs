namespace MyFinances.Common.Core.Modules.RootConfiguration;

public static class ModuleRootRegistry
{
    private static readonly List<ICompositionRoot> _compositionRoots = [];

    public static IEnumerable<ICompositionRoot> CompositionRoots => _compositionRoots.AsReadOnly();
    
    public static void Add(ICompositionRoot compositionRoot)
    {
        _compositionRoots.Add(compositionRoot);
    }

    public static ICompositionRoot GetByModule(IModuleConfiguration moduleDefinition)
    {
        return _compositionRoots.Find(x => x.ModuleDefinition == moduleDefinition)!;
    }

    public static ICompositionRoot GetByModule<TModule>() where TModule : class, IModuleConfiguration
    {
        return _compositionRoots.Find(x => x.ModuleDefinition.GetType() == typeof(TModule))!;
    }
}