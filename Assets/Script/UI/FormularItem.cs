
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormularItem : MonoBehaviour
{

	[SerializeField] private TMPro.TextMeshProUGUI _text;
	public void SetText(string text)
	{
		_text.text = text;
		_text.color = text == "?" ? Color.blue : Color.red;
	}
}
