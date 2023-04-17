using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using System;
using UnityEngine.UI;

public class LearningClassOne : LearningManager
{
    [Header("Top")]
    [SerializeField] TMP_Text label1;
    [SerializeField] TMP_Text label2;

    [Header("body")]
    [SerializeField] TMP_Text numberOne;
    [SerializeField] TMP_Text caculation;
    [SerializeField] TMP_Text numberSecond;
    [SerializeField] TMP_Text numberResult;
    [SerializeField] TMP_Text countQuestion;
    [SerializeField] RangdomResult rd;

    [Header("icon")]
    [SerializeField] Image icon;
    [SerializeField] Sprite sucess;
    [SerializeField] Sprite wrong;

    [Header("Popup")]
    [SerializeField] PopupQuitGame popupQuitGame;

    int currentQuestion = 1;
    int maxCurentQuestion = 10;
    private void Start()
    {
        //PlusOneDigit("1 + 1 chữ số");
        rd.onResultChanged += OnResultChanged;
        popupQuitGame.Click += DesTroyOb;

    }

    public void PlusOneDigit(string label)
    {
        currentQuestion = 1;
        Setlabel(label);
        Caculition(10, TypeCalculation.Sum);
        SetUINumber(TypeCalculation.Sum);
        rd.InitResult(10, result);
    }
    public void Setlabel(string label)
    {
        label1.text = label;
        label2.text = label;
    }
    public void SetUINumber(TypeCalculation type)
    {
        numberOne.text = fistNumber.ToString();
        numberSecond.text = secondNumber.ToString();
        caculation.text = GetTypeCalculation(TypeCalculation.Sum).ToString();
        numberResult.text = "";
    }

    public void OnResultChanged(string _result)
    {
        numberResult.text = _result;
        if(_result == result.ToString())
        {
            icon.gameObject.SetActive(true);
            currentQuestion++;
            icon.sprite = sucess;
            icon.color = Color.green;
            StartCoroutine(GenerateQuestion());
            StartCoroutine(EnableQuestionIcon());
        }
        else
        {
            icon.gameObject.SetActive(true);
            icon.sprite = wrong;
            icon.color = Color.red;
            StartCoroutine(EnableQuestionIcon());
        }
    }

    public void ShowPopupQuitGame()
    {
        popupQuitGame.gameObject.SetActive(true);
    }

    void DesTroyOb()
    {
        Destroy(transform.gameObject);
    }
    IEnumerator GenerateQuestion()
    {
        yield return new WaitForSeconds(2);
        countQuestion.text = currentQuestion.ToString() + "/" + maxCurentQuestion.ToString();
        PlusOneDigit("1 + 1 chữ số");
    }
    IEnumerator EnableQuestionIcon()
    {
        yield return new WaitForSeconds(2);
        icon.gameObject.SetActive(false);
        numberResult.text = "";
    }
}
