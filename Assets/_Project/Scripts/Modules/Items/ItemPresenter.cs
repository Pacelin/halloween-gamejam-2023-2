using System;
using UnityEngine;

public class ItemPresenter : MonoBehaviour
{
	public event Action OnGrab;
	public ItemData Data => _data;
	public SpriteRenderer SpriteRenderer => _selfSpriteRenderer;

	[SerializeField] private ItemData _data;
	[Space]
	[SerializeField] private Collider2D _selfCollider;
	[SerializeField] private Rigidbody2D _selfRigidbody;
	[SerializeField] private SpriteRenderer _selfSpriteRenderer;

	public void SetTrigger()
	{
		_selfCollider.isTrigger = true;
		_selfRigidbody.isKinematic = true;
	}
	
	public void Grab()
	{ 
		_selfCollider.isTrigger = false;
		_selfRigidbody.isKinematic = false;
		OnGrab?.Invoke();
	}
}