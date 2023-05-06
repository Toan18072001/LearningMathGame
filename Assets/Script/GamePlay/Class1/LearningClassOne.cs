using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class LearningClassOne : LearningManager
{
    [Header("Top")]
    [SerializeField] TMP_Text label1;
    [SerializeField] TMP_Text label2;

    [Header("body")]
    [SerializeField] TMP_Text countQuestionUI;
    [SerializeField] UICaculation uICaculation;
    [SerializeField] UICompareCaculation uICompareCaculation;
    [SerializeField] RangdomResult rd;

    [Header("icon")]
    [SerializeField] Image icon;
    [SerializeField] Sprite sucess;
    [SerializeField] Sprite wrong;

    [Header("Popup")]
    [SerializeField] PopupQuitGame popupQuitGame;

    int currentQuestion = 1;
    public int maxCurentQuestion;
    public bool isSummaryCaculation = false;
    public TypeOfTopic type;
    
    private void Start()
    {
        //PlusOneDigit("1 + 1 chữ số");
        rd.onResultChanged += OnResultChanged;
        popupQuitGame.Click += DesTroyOb;
        countQuestionUI.text = currentQuestion.ToString() + "/" + maxCurentQuestion.ToString();

    }

    public void CaculationOneDigit(string label, TypeCalculation _type, int maxValue)
    {
        uICaculation.gameObject.SetActive(true);
        uICompareCaculation.gameObject.SetActive(false);
        Setlabel(label);
        Caculition(maxValue, _type);
        rd.InitResult(maxValue, result);
        SetUINumber(_type);
    }
 
    public void CaculationDoubleNumber(string label, TypeCalculation _type, int maxValue)
    {
        uICaculation.gameObject.SetActive(true);
        uICompareCaculation.gameObject.SetActive(false);
        Setlabel(label);
        CaculitionDouble(maxValue,_type);
        rd.InitResult(maxValue, result);
        SetUINumber(_type);
    }
    public void CaculationCompareNumber(string label, int maxValue)
    {
        uICompareCaculation.gameObject.SetActive(true);
        uICaculation.gameObject.SetActive(false);
        Setlabel(label);
        CaculitionCompare(maxValue);
        SetUINumber(TypeCalculation.ComparisonSpells);
        uICaculation.gameObject.SetActive(false);
        rd.InitResult();

    }

    public void Setlabel(string label)
    {
        label1.text = label;
        label2.text = label;
    }
    public void SetUINumber(TypeCalculation type)
    {
        if (TypeCalculation.ComparisonSpells != type)
        {
            uICaculation.Init(fistNumber.ToString(), secondNumber.ToString(), GetTypeCalculation(type).ToString());
        }
        else
        {
            uICompareCaculation.Init(fistNumber.ToString(), secondNumber.ToString());
        }
    }

    public void OnResultChanged(string _result)
    {
        Debug.Log("Player Result: " + _result);
        if (uICaculation.gameObject.active)
        {
            uICaculation.SetResultUI(_result);
            if (_result == result.ToString())
            {
                ResultSucces();
                Debug.Log("curent qes: " + currentQuestion);
                countQuestionUI.text = currentQuestion.ToString() + "/" + maxCurentQuestion.ToString();

            }
            else
            {
                ResultWrong();
            }
        }
        else
        {
            uICompareCaculation.SetResultUI(_result);
            if(fistNumber > secondNumber && _result == ">")
            {
                ResultSucces();
                Debug.Log("curent qes: " + currentQuestion);
                countQuestionUI.text = currentQuestion.ToString() + "/" + maxCurentQuestion.ToString();


            }
            else if (fistNumber == secondNumber && _result == "=")
            {
                ResultSucces();
                Debug.Log("curent qes: " + currentQuestion);
                countQuestionUI.text = currentQuestion.ToString() + "/" + maxCurentQuestion.ToString();

            }
            else if (fistNumber < secondNumber && _result == "<")
            {
                ResultSucces();
                Debug.Log("curent qes: " + currentQuestion);
                countQuestionUI.text = currentQuestion.ToString() + "/" + maxCurentQuestion.ToString();

            }
            else
            { 
                ResultWrong();
            }
        }
    }
    public void ResultSucces()
    {
        StartCoroutine(GenerateQuestion());
        icon.gameObject.SetActive(true);
        icon.sprite = sucess;
        icon.color = Color.green;
        currentQuestion++;
    }
    public void ResultWrong()
    {
        StartCoroutine(EnableQuestionIcon());
        icon.gameObject.SetActive(true);
        icon.sprite = wrong;
        icon.color = Color.red;
    }
    public void ShowPopupQuitGame()
    {
        popupQuitGame.gameObject.SetActive(true);
    }
    public void SummaryCaculation()
    {
        isSummaryCaculation = true;
        if (currentQuestion <= 5)
        {
            GenerateType(TypeOfTopic.OneSumOneNumber);
        }
        else if (currentQuestion > 5 && currentQuestion <= 10)
        {
            GenerateType(TypeOfTopic.OneBrandOneNumber);
        }
        else if (currentQuestion > 10 && currentQuestion <= 15)
        {
            GenerateType(TypeOfTopic.SumDoubleNumber);
        }
        else
        {
            GenerateType(TypeOfTopic.Compare);
        }

    }
    
    void DesTroyOb()
    {
        Destroy(transform.gameObject);
    }
    IEnumerator GenerateQuestion()
    {
        yield return new WaitForSeconds(1.5f);
        icon.gameObject.SetActive(false);
        if (!isSummaryCaculation)
        {
            GenerateType(type);
        }
        else
        {
            SummaryCaculation();
        }
    }
    IEnumerator EnableQuestionIcon()
    {
        yield return new WaitForSeconds(1.5f);
        icon.gameObject.SetActive(false);
        uICaculation.ClearResultUI();
        uICompareCaculation.ClearResultUI();
    }

    void GenerateType(TypeOfTopic typeOfTopic)
    {
        switch (typeOfTopic)
        {
            case TypeOfTopic.OneSumOneNumber:
                CaculationOneDigit("1 + 1 chữ số",TypeCalculation.Sum,10);
                uICaculation.gameObject.SetActive(true);
                break;
            case TypeOfTopic.SumDoubleNumber:
                CaculationDoubleNumber("Cộng gấp đôi", TypeCalculation.Sum, 10);
                uICaculation.gameObject.SetActive(true);
                break;
            case TypeOfTopic.OneBrandOneNumber:
                CaculationOneDigit("1 - 1 chữ số", TypeCalculation.Brand, 10);
                uICaculation.gameObject.SetActive(true);
                break;
            case TypeOfTopic.Compare:
                CaculationCompareNumber("Lớn, bé, Bằng (>, <, =)", 20);
                break;

        }
    }
}
