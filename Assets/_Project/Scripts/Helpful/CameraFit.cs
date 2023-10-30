using UnityEngine;

public class CameraFit : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _fitRenderer;

	private void Awake()
	{
		var cam = Camera.main;
		var w = _fitRenderer.bounds.size.x;
		var h = _fitRenderer.bounds.size.y;
	
		cam.orthographicSize = ((w > h* cam.aspect) ? (float) w / (float) cam.pixelWidth * cam.pixelHeight : h) / 2;
	}
}
