using UnityEngine;
using Zenject;

public class StartSceneInstaller : MonoInstaller {
    [SerializeField] private StartScreenView _startScreenView;

    public override void InstallBindings() {
        Container.BindInterfacesAndSelfTo<StartScreenController>().AsSingle().WithArguments(_startScreenView);
    }
}