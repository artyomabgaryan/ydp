using Interfaces;
using UnityEngine;
using Zenject;

namespace DI.Factories {
public class PrefabFactory : IPrefabFactory {
	private readonly DiContainer _container;

	public PrefabFactory(DiContainer container) {
		_container = container;
	}
	public GameObject Instantiate(GameObject prefab){
		return _container.InstantiatePrefab(prefab);
	}

	public Object Instantiate(Object prefab) {
		return _container.InstantiatePrefab(prefab);
	}
}
}