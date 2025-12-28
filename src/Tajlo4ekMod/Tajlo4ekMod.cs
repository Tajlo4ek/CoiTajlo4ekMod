using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Tajlo4ekMod.fastStacker;
using Tajlo4ekMod.InfStorage;
using Tajlo4ekMod.train;


namespace Tajlo4ekMod;

public sealed class Tajlo4ekMod : IMod
{
    public bool IsUiOnly = false;

    public string Name => "TAJLO4EK mod";

    public int Version => 1;

    bool IMod.IsUiOnly => false;

    Option<IConfig> IMod.ModConfig => Option<IConfig>.None;

    public Tajlo4ekMod(CoreMod coreMod, BaseMod baseMod)
    {
    }

    public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
    {
    }

    public void EarlyInit(DependencyResolver resolver)
    {
    }

    public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
    {
    }

    public void RegisterPrototypes(ProtoRegistrator registrator)
    {
        Logger.Log("RegisterPrototypes");

        registrator.RegisterData<InfStorageRegistrator>();
        registrator.RegisterData<TrainRegistrator>();
        registrator.RegisterData<FastStacker>();

    }

}