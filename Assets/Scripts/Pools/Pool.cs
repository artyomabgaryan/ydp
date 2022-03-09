using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Pools {
public class Pool<T> where T : Object {
	private readonly Stack<T> _items = new();
	private readonly T _originalItem;
	[Inject] private IPrefabFactory _prefabFactory;
	public Pool(T originalItem) {
		_originalItem = originalItem;
	}

	public T GetItem() {
		return (T)(_items.TryPop(out var item) ? item : _prefabFactory.Instantiate(_originalItem));
	}

	public void AddItem(T item) {
		_items.Push(item);
	}
}
}