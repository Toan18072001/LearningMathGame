using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LearningClassTwo : LearningManager
{
	[Header("body")]
	[SerializeField] TMP_Text countQuestionUI;
	[SerializeField] protected TextAsset _questionResource;
	[SerializeField] protected TextMeshProUGUI _contentTxt;
	[SerializeField] protected UICaculation _uICaculation;
	[SerializeField] protected TextMeshProUGUI _questionTxt;
	[SerializeField] protected RangdomResult rd;

	[Header("Popup")]
	[SerializeField] PopupQuitGame popupQuitGame;
	[SerializeField] PopupEndGame popupEndGame;

	[Header("icon")]
	[SerializeField] Image icon;
	[SerializeField] Sprite sucess;
	[SerializeField] Sprite wrong;

	protected int _currentQuestion = 1;
	protected int _maxCurentQuestion = 20;
	protected Question[] _questionDatas;
	protected int _result = 0;
	private void Start()
	{
		_questionDatas = JsonConvert.DeserializeObject<Question[]>(_questionResource.text);
		LoadQuestion(_questionDatas[Random.Range(0, _questionDatas.Length)]);
	}

	private void OnEnable()
	{
		rd.onResultChanged += OnResultChanged;
		popupQuitGame.Click += DesTroyOb;
		popupEndGame.Click += DesTroyOb;
	}

	private void OnDisable()
	{
		rd.onResultChanged -= OnResultChanged;
		popupQuitGame.Click -= DesTroyOb;
		popupEndGame.Click -= DesTroyOb;
	}
	public void LoadQuestion(Question question)
	{
		int maxValue = 100;
		///rd.InitResult(maxValue, result);
		Caculition(maxValue, GetEnumTypeCalculation(question.formular));
		_uICaculation.Init(fistNumber.ToString(), secondNumber.ToString(), GetTypeCalculation(question.formular).ToString());
		int numb1 = 0;
		int numb2 = 0;
		int numb3 = 0;

		GetFormular(question.formular, maxValue, ref numb1, ref numb2, ref numb3);
		int randomQuestion = Random.Range(0, question.question.Length);
		string value1 = randomQuestion == 0 ? "[X]" : numb1.ToString();
		string value2 = randomQuestion == 1 ? "[X]" : numb2.ToString();
		string value3 = randomQuestion == 2 ? "[X]" : numb3.ToString();
		_contentTxt.text = string.Format(question.content,
			value1,
			value2,
			value3,
			(question.objectRandom.Length >= 1 ? question.objectRandom[0][UnityEngine.Random.Range(0, question.objectRandom[0].Length)] : string.Empty),
			(question.objectRandom.Length >= 2 ? question.objectRandom[1][UnityEngine.Random.Range(0, question.objectRandom[1].Length)] : string.Empty)
			);

		_questionTxt.text =

			string.Format(question.question[randomQuestion],
			value1,
			value2,
			value3,
			(question.objectRandom.Length >= 1 ? question.objectRandom[0][UnityEngine.Random.Range(0, question.objectRandom[0].Length)] : string.Empty),
			(question.objectRandom.Length >= 2 ? question.objectRandom[1][UnityEngine.Random.Range(0, question.objectRandom[1].Length)] : string.Empty)
			);
		_result = randomQuestion == 0 ? numb1 : (randomQuestion == 1 ? numb2 : numb3);
		_uICaculation.Init(value1, value2, value3, GetTypeCalculation(question.formular).ToString(), randomQuestion, "[X]");
		rd.InitResult(maxValue, _result);
	}

	private void GetFormular(int type, int max, ref int numb1, ref int numb2, ref int numb3)
	{
		switch (type)
		{
			case 0:
				numb1 = UnityEngine.Random.Range(0, max);
				numb2 = UnityEngine.Random.Range(0, max);
				numb3 = numb1 * numb2;
				break;
			case 1:
				numb1 = UnityEngine.Random.Range(0, max);
				numb2 = UnityEngine.Random.Range(0, max);
				numb3 = numb1 + numb2;
				break;
			case 2:
				numb2 = UnityEngine.Random.Range(0, max);
				numb3 = UnityEngine.Random.Range(0, max);
				numb1 = numb2 + numb3;
				break;
			case 3:
				numb2 = UnityEngine.Random.Range(0, max);
				numb3 = UnityEngine.Random.Range(0, max);
				numb1 = numb3 * numb2;
				break;
		}

	}
	protected char GetTypeCalculation(int type)
	{
		//char c_Calculation;
		switch (type)
		{
			case 1:
				cTypeCatution = '+';
				break;
			case 2:
				cTypeCatution = '-';
				break;
			case 0:
				cTypeCatution = '*';
				break;
			case 3:
				cTypeCatution = ':';
				break;
		}
		return cTypeCatution;
	}


	protected TypeCalculation GetEnumTypeCalculation(int type)
	{
		//char c_Calculation;
		switch (type)
		{
			case 1:
				return TypeCalculation.Sum;
			case 2:
				return TypeCalculation.Brand;
			case 0:
				return TypeCalculation.Multiplication;
			case 3:
				return TypeCalculation.Division;
		}
		return TypeCalculation.Sum;
	}
	public void OnResultChanged(string result)
	{

		if (_uICaculation.gameObject.activeSelf)
		{
			_uICaculation.SetResultUI(result);
			if (result == _result.ToString())
			{
				ResultSucces();
				countQuestionUI.text = _currentQuestion.ToString() + "/" + _maxCurentQuestion.ToString();
			}
			else
			{
				ResultWrong();
			}
		}

	}

	public void ResultSucces()
	{
		StopAllCoroutines();
		_currentQuestion++;
		StartCoroutine(IEGenerateQuestion());
		icon.gameObject.SetActive(true);
		icon.sprite = sucess;
		icon.color = Color.green;

	}

	public void ResultWrong()
	{
		StopAllCoroutines();
		StartCoroutine(IEEnableQuestionIcon());
		icon.gameObject.SetActive(true);
		icon.sprite = wrong;
		icon.color = Color.red;
	}

	void DesTroyOb()
	{
		Destroy(transform.gameObject);
	}

	protected IEnumerator IEEnableQuestionIcon()
	{
		yield return new WaitForSeconds(1.5f);
		icon.gameObject.SetActive(false);
		_uICaculation.ClearResultUI();
	}

	protected IEnumerator IEGenerateQuestion()
	{
		yield return new WaitForSeconds(1.5f);
		icon.gameObject.SetActive(false);
		LoadQuestion(_questionDatas[Random.Range(0, _questionDatas.Length)]);
	}
}
