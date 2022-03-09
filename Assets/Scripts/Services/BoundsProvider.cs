using UnityEngine;


public class BoundsProvider : IBoundsProvider {
	private readonly Camera _camera;
	private readonly Bounds _cameraBounds;
	private readonly Bounds _safeBounds;

	public Bounds CameraBounds => _cameraBounds;

	public Bounds SafeBounds => _safeBounds;

	public BoundsProvider(Camera camera) {
		_camera = camera;
		_cameraBounds = GetCameraBounds();
		_safeBounds = GetSafeBounds();
	}

	private Bounds GetSafeBounds() {
		var cameraBounds = GetCameraBounds();
		cameraBounds.Expand(2);
		return cameraBounds;
	}

	private Bounds GetCameraBounds() {
		var localPosition = _camera.transform.localPosition;
		var orthographicSize = _camera.orthographicSize;
		var cameraAspect = _camera.aspect;

		var bounds = new Bounds {
			min = new Vector3(localPosition.x - orthographicSize * cameraAspect, localPosition.y - orthographicSize, 0),
			max = new Vector3(localPosition.x + orthographicSize * cameraAspect, localPosition.y + orthographicSize, 20)
		};

		return bounds;
	}
}