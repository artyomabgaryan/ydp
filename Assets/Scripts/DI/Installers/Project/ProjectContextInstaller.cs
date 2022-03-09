using DI.Signals;
using Zenject;

public class ProjectContextInstaller : MonoInstaller {
	public override void InstallBindings() {
		InstallManagerBindings();
		InstallSignalBindings();
	}

	private void InstallManagerBindings() {
		Container.Bind<IStorageService>().To<StorageService>().AsSingle();
		Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
		Container.BindInterfacesAndSelfTo<SceneLoadManager>().AsSingle();
	}

	private void InstallSignalBindings() {
		SignalBusInstaller.Install(Container);
		Container.DeclareSignal<LoadSceneSignal>();
	}
}