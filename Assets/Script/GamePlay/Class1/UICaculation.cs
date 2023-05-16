using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICaculation : MonoBehaviour
{
	[SerializeField] TMP_Text numberOne;
	[SerializeField] TMP_Text caculation;
	[SerializeField] TMP_Text numberSecond;
	[SerializeField] TMP_Text numberResult;

	private TMP_Text _currentSlot;

	private string _oldTextValue;
	// Start is called before the first frame update
	public void Init(string _fistNumber, string _secondNumber, string TypeCalculation)
	{
		numberOne.text = _fistNumber;
		numberSecond.text = _secondNumber;
		caculation.text = TypeCalculation;
		numberResult.text = "";
	}

	public void Init(string _fistNumber, string _secondNumber, string result, string TypeCalculation, int slotIndex = 2, string oldValue = "")
	{
		_oldTextValue = oldValue;
		numberOne.text = _fistNumber;
		numberSecond.text = _secondNumber;
		caculation.text = TypeCalculation;
		numberResult.text = result;
		_currentSlot = slotIndex == 2 ? numberResult : (slotIndex == 1 ? numberSecond : numberOne);
	}




	public void SetResultUI(string result)
	{
		_currentSlot.text = result;
	}
	public void ClearResultUI()
	{
		_currentSlot.text = _oldTextValue;
	}

}
