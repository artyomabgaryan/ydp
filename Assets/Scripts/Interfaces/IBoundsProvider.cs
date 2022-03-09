using UnityEngine;

public interface IBoundsProvider {
	public Bounds CameraBounds { get; }
	public Bounds SafeBounds { get; }
}