using Zenject;
using UnityEngine;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PathModel>().AsSingle();
        Container.Bind<MovementModel>().AsSingle();
        Container.Bind<MovementController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoodModel>().AsSingle();
        Container.Bind<EnergyModel>().AsSingle();
        Container.Bind<EnergyController>().AsSingle();
        Container.Bind<MoodController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TimeModel>().AsSingle();
        Container.Bind<TimeController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TemperatureModel>().AsSingle();
        Container.Bind<TemperatureController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<WeatherController>().AsSingle();
        Container.Bind<WeatherModel>().AsSingle();
        Container.Bind<LogSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InventorySystem>().AsSingle();
    }
}
