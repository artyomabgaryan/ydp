using System;
using DI.Signals;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoadManager : IInitializable, IDisposable {
	private readonly SignalBus _signalBus;

	public SceneLoadManager(SignalBus signalBus) {
		_signalBus = signalBus;
	}

	public void Initialize() {
		InitializeEvents();
	}

	private void InitializeEvents() {
		_signalBus.Subscribe<LoadSceneSignal>(OnLoadScene);
	}

	public void Dispose() {
		_signalBus.Unsubscribe<LoadSceneSignal>(OnLoadScene);
	}

	private void OnLoadScene(LoadSceneSignal signal) {
		SceneManager.LoadScene((int) signal.SceneId);
	}
}