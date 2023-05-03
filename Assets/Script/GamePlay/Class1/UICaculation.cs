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
    // Start is called before the first frame update
    public void Init(string _fistNumber, string _secondNumber, string TypeCalculation)
    {
        numberOne.text = _fistNumber;
        numberSecond.text = _secondNumber;
        caculation.text = TypeCalculation;
        numberResult.text = "";
    }

    public void SetResultUI(string result)
    {
        numberResult.text = result;
    }
    public void ClearResultUI()
    {
        numberResult.text = "";
    }

}
