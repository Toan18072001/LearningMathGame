using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FormularController : MonoBehaviour
{
	[SerializeField] private FormularItem _prefab;

	private List<FormularItem> _items = new List<FormularItem>();
	private int _indexFinding;

	public void SpawnFormular(string formular)
	{
		for (int i = 0; i < _items.Count; i++)
		{
			_items[i].gameObject.SetActive(false);
		}
		string[] datas = formular.Split(' ');
		for (int i = 0; i < datas.Length; i++)
		{
			if (i >= _items.Count)
			{
				_items.Add(GameObject.Instantiate(_prefab, transform));
			}

			StartCoroutine(IEDelayEnableItem(i * 0.1f, _items[i].gameObject));
			if (datas[i] == "?")
			{
				_indexFinding = i;
			}
			_items[i].SetText(datas[i]);
		}
	}

	private IEnumerator IEDelayEnableItem(float delay, GameObject item)
	{
		yield return new WaitForSeconds(delay);
		item.SetActive(true);
	}

	public void SetResult(string result)
	{
		_items[_indexFinding].SetText(result);
	}
}
