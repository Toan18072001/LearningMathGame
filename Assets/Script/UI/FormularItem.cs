
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormularItem : MonoBehaviour
{

	[SerializeField] private TMPro.TextMeshProUGUI _text;
	public void SetText(string text)
	{
		if (text.Contains('/'))
		{
			string[] numbers = text.Split('/');
			_text.text = string.Format("{0}\n_\n{1}", numbers[0], numbers[1]);

		}
		else
		{
			_text.text = text;
			_text.color = text == "?" ? Color.blue : Color.red;
		}
	}
}
