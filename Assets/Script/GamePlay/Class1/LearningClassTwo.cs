using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;

public class LearningClassTwo : LearningManager
{
	[Header("body")]
	[SerializeField] TMP_Text countQuestionUI;
	[SerializeField] protected TextAsset _questionResource;
	[SerializeField] protected TextMeshProUGUI _contentTxt;
	[SerializeField] protected UICaculation _uICaculation;
	[SerializeField] protected TextMeshProUGUI _questionTxt;
	[SerializeField] protected RangdomResult rd;
	[SerializeField] protected FormularController _formular;

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
	protected int _showType = 0;
	private void Start()
	{
		_questionDatas = JsonConvert.DeserializeObject<Question[]>(_questionResource.text);
		//LoadQuestionTypeFindNumber(4, 8);
		//LoadQuestion(_questionDatas[Random.Range(0, _questionDatas.Length)]);
		LoadQuestion();
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


	public void OnShowType(int type)
	{
		_showType = type;
		LoadQuestion();
	}
	public void LoadQuestionTypeFindNumber(int max, int min)
	{
		List<TypeCalculation> types = new List<TypeCalculation>();
		List<int> values = new List<int>();
		int randValue = Random.Range(min, max);
		GetRandFomular(randValue, ref types, ref values);
		bool isRight = Random.Range(0, 100) >= 50;
		if (!isRight)
		{
			int randomIndex = Random.Range(0, values.Count);
			_result = values[randomIndex];
			values[randomIndex] = -1;
		}
		string left = string.Format("{0} {1} {2} {3} {4}",
			values[0] < 0 ? "?" : values[0],
			GetTypeCalculation(types[0]),
			values[1] < 0 ? "?" : values[1],
			types.Count >= 2 ? GetTypeCalculation(types[1]) : string.Empty,
			values.Count >= 3 ? (values[2] < 0 ? "?" : values[2]) : string.Empty);

		values.Clear();
		types.Clear();
		GetRandFomular(randValue, ref types, ref values);




		if (isRight)
		{
			int randomIndex = Random.Range(0, values.Count);
			_result = values[randomIndex];
			values[randomIndex] = -1;
		}
		rd.InitResult(randValue, _result);
		string right = string.Format("{0} {1} {2} {3} {4}",
			values[0] < 0 ? "?" : values[0],
			GetTypeCalculation(types[0]),
			values[1] < 0 ? "?" : values[1],
			types.Count >= 2 ? GetTypeCalculation(types[1]) : string.Empty,
			values.Count >= 3 ? (values[2] < 0 ? "?" : values[2]) : string.Empty);

		_formular.SpawnFormular(left.Trim() + " = " + right.Trim());

	}

	public void GetRandFomular(int value, ref List<TypeCalculation> types, ref List<int> values)
	{
		int random = 0;
		switch (Random.Range(0, 4))
		{
			case 0:
				random = Random.Range(2, value);
				int devide = value / random;
				int modulo = value % random;
				values.Add(random);
				types.Add(TypeCalculation.Multiplication);
				values.Add(devide);
				if (modulo != 0)
				{
					types.Add(TypeCalculation.Sum);
					values.Add(modulo);
				}
				break;
			case 1:
				random = Random.Range(2, value);
				int multiply = value * random;
				values.Add(multiply);
				types.Add(TypeCalculation.Division);
				values.Add(random);
				break;
			case 2:
				random = Random.Range(2, value);

				int substract = value - random;
				values.Add(random);
				types.Add(TypeCalculation.Sum);
				values.Add(substract);
				break;
			case 3:
				random = Random.Range(2, value);
				int sum = value + random;
				values.Add(sum);
				types.Add(TypeCalculation.Brand);
				values.Add(random);
				break;
		}
	}
	public void LoadQuestion()
	{
		switch (_showType)
		{
			case 0:
				LoadQuestionContent(_questionDatas[Random.Range(0, _questionDatas.Length)]);
				break;
			case 1:
				LoadQuestionTypeFindNumber(10, 20);
				break;
		}
		LoadQuestionTypeFindNumber(10, 50);
	}

	public void LoadQuestionContent(Question question)
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
			case 2:
				return TypeCalculation.Sum;
			case 3:
				return TypeCalculation.Brand;
			case 0:
				return TypeCalculation.Multiplication;
			case 1:
				return TypeCalculation.Division;
		}
		return TypeCalculation.Sum;
	}
	public void OnResultChanged(string result)
	{

		if (_uICaculation.gameObject.activeSelf)
		{
			_formular.SetResult(result);
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
		//_uICaculation.ClearResultUI();
		_formular.SetResult("?");
	}

	protected IEnumerator IEGenerateQuestion()
	{
		yield return new WaitForSeconds(1.5f);
		icon.gameObject.SetActive(false);
		LoadQuestion();
	}
}
