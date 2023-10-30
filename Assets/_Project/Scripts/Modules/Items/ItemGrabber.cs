using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
	[SerializeField] private float _damping = 10.0f;
	[SerializeField] private float _frequency = 5.0f;
	[SerializeField] private float _breakForce = 3000.0f;

	private TargetJoint2D _targetJoint;

	void Update()
	{
		var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(0))
		{
			var colliders = Physics2D.OverlapPointAll(worldPos);
			foreach (var collider in colliders)
			{
				var body = collider.attachedRigidbody;
				if (!body) return;

				if (!collider.TryGetComponent<ItemPresenter>(out var presenter)) return;

				_targetJoint = body.gameObject.AddComponent<TargetJoint2D>();
				_targetJoint.dampingRatio = _damping;
				_targetJoint.frequency = _frequency;
				_targetJoint.breakForce = _breakForce;
				_targetJoint.anchor = _targetJoint.transform.InverseTransformPoint(worldPos);
				presenter.Grab();
				break;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Destroy(_targetJoint);
			_targetJoint = null;
			return;
		}

		if (_targetJoint)
		{
			_targetJoint.target = worldPos;
		}
	}
}
