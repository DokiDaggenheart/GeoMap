using Zenject;
using UnityEngine;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PathModel>().AsSingle();
        Container.Bind<MovementModel>().AsSingle();
        Container.Bind<MoodModel>().AsSingle();
        Container.Bind<EnergyModel>().AsSingle();
        Container.Bind<EnergyController>().AsSingle();
        Container.Bind<MoodController>().AsSingle();
    }
}
