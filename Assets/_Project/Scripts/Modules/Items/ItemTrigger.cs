using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
	public event Action<ItemPresenter> OnEnter;
	public ItemPresenter ItemIn => _items.Count > 0 ? _items[0] : null;
	private List<ItemPresenter> _items = new();

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<ItemPresenter>(out var item))
		{
			_items.Add(item);
			OnEnter?.Invoke(item);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<ItemPresenter>(out var item))
		{
			_items.Remove(item);
		}
	}
}