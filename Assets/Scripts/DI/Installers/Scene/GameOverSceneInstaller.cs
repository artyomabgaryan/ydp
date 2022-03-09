using UnityEngine;
using Zenject;

public class GameOverSceneInstaller : MonoInstaller {
    [SerializeField] private GameOverScreenView _gameOverScreenView;

    public override void InstallBindings() {
        Container.BindInterfacesAndSelfTo<GameOverScreenController>().AsSingle().WithArguments(_gameOverScreenView);
    }
}