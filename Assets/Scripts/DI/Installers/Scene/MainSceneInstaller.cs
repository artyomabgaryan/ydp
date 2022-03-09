using Components.Weapon;
using Controllers;
using DI.Factories;
using DI.Signals;
using Interfaces;
using Pools;
using UnityEngine;
using Views;
using Zenject;

public class MainSceneInstaller : MonoInstaller {
	[Header("Cameras")] [SerializeField] private Camera _mainCamera;
	[Header("Prefabs")] [SerializeField] private GameObject _enemy;
	[SerializeField] private GameObject _bullet;

	[Header("Containers")] [SerializeField]
	private Transform _enemyContainer;

	[SerializeField] private Transform _bulletContainer;
	[Header("Views")] [SerializeField] private WeaponView _weaponView;
	[SerializeField] private PlayerHealthView _playerHealthView;
	[SerializeField] private PlayerScoreView _scoreView;
	[SerializeField] private PlayerGunView _playerGunView;
	[Header("Configs")] [SerializeField] private WeaponConfig _singleShootWeapon;
	[SerializeField] private WeaponConfig _queueShootWeapon;
	[SerializeField] private WeaponConfig _arcShootWeapon;

	public override void InstallBindings() {
		InstallConfigBindings();
		InstallModuleBindings();
		InstallSignalBindings();
		InstallPoolBindings();
		InstallWeaponBindings();
		InstallFactoryBindings();
	}

	private void InstallFactoryBindings() {
		Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
	}

	private void InstallConfigBindings() {
		Container.Bind<GameConfig>().FromScriptableObjectResource("Configuration/Game/Game Config").AsSingle();
	}

	private void InstallModuleBindings() {
		Container.BindInterfacesAndSelfTo<WeaponUIController>().AsSingle().WithArguments(_weaponView);
		Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle().WithArguments(_scoreView);
		Container.BindInterfacesAndSelfTo<HealthController>().AsSingle().WithArguments(_playerHealthView);
		Container.BindInterfacesAndSelfTo<WeaponController>().AsSingle().WithArguments(_playerGunView);
		Container.BindInterfacesAndSelfTo<EnemyController>().AsSingle().WithArguments(_enemyContainer);
		Container.Bind<IBoundsProvider>().To<BoundsProvider>().AsSingle().WithArguments(_mainCamera);
		Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
	}

	private void InstallSignalBindings() {
		Container.DeclareSignal<UserTouchInputSignal>();
		Container.DeclareSignal<PlayZoneGotHitSignal>();
		Container.DeclareSignal<EnemyShipDestroyedSignal>();
		Container.DeclareSignal<ActivateWeaponSignal>();
		Container.DeclareSignal<WeaponChangedSignal>();
		Container.DeclareSignal<DeactivateBulletSignal>();
	}

	private void InstallWeaponBindings() {
		Container.Bind<Weapon>().To<SingleShootWeapon>().AsSingle()
			.WithArguments(_singleShootWeapon, _bulletContainer);
		Container.Bind<Weapon>().To<QueueShootWeapon>().AsSingle().WithArguments(_queueShootWeapon, _bulletContainer);
		Container.Bind<Weapon>().To<ArcShootWeapon>().AsSingle().WithArguments(_arcShootWeapon, _bulletContainer);
		Container.BindInterfacesAndSelfTo<WeaponActivator>().AsSingle();
	}

	private void InstallPoolBindings() {
		Container.Bind<Pool<GameObject>>().WithArguments(_enemy).WhenInjectedInto<EnemyController>();
		Container.Bind<Pool<GameObject>>().WithArguments(_bullet).WhenInjectedInto<WeaponController>();
	}
}