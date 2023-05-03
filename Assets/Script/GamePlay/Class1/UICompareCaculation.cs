using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICompareCaculation : MonoBehaviour
{
    [SerializeField] TMP_Text numberOne;
    [SerializeField] TMP_Text caculation;
    [SerializeField] TMP_Text numberSecond;
    // Start is called before the first frame update
    public void Init(string _fistNumber, string _secondNumber)
    {
        numberOne.text = _fistNumber;
        numberSecond.text = _secondNumber;
        caculation.text = "";
    }

    public void SetResultUI(string result)
    {
        caculation.text = result;
    }
    public void ClearResultUI()
    {
        caculation.text = "";
    }
}
