using System.Collections;
using UnityEngine;

public class Titres_Scrolling : MonoBehaviour
{
	[SerializeField] private float _yDistance;
	[SerializeField] private float _speed;

	private IEnumerator Start()
	{
		var t = (RectTransform)transform;
		var pos = t.anchoredPosition;
		var curDistance = 0f;

		while (curDistance < _yDistance)
		{
			var offset = _speed * Time.deltaTime;
			if (Input.GetMouseButton(0))
				offset *= 3;

			curDistance += offset;
			pos.y += offset;
			
			t.anchoredPosition = pos;
			yield return null;
		}

		SceneTransitions.Instance.SetMainImageColor(Color.black);
		SceneTransitions.Instance.StartTransition(2, 1);
	}
}
