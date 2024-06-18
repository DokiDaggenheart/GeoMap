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
        Container.Bind<TimeModel>().AsSingle();
        Container.Bind<TimeController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TemperatureModel>().AsSingle();
        Container.Bind<TemperatureController>().AsSingle();
        Container.Bind<WeatherController>().AsSingle();
        Container.Bind<WeatherModel>().AsSingle();
    }
}
