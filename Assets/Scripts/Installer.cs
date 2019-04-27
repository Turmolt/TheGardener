using BackwardsCap;
using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DayManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<LifeManager>().AsSingle();
    }
}