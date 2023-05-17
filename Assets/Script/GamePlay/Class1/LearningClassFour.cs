using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LearningClassFour : LearningManager
{
	[Header("body")]
	[SerializeField] TMP_Text countQuestionUI;
	[SerializeField] protected TextMeshProUGUI _contentTxt;
	[SerializeField] protected UICaculation _uICaculation;
	[SerializeField] protected FormularController _formular;
	[SerializeField] protected RangdomResult rd;


	[Header("Popup")]
	[SerializeField] PopupQuitGame popupQuitGame;
	[SerializeField] PopupEndGame popupEndGame;

	[Header("icon")]
	[SerializeField] Image icon;
	[SerializeField] Sprite sucess;
	[SerializeField] Sprite wrong;

	private int _currentQuestion;
	private int _type;
	private int _result;
	private int _maxCurentQuestion = 20;
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
	private void Start()
	{
		LoadQuestion();
	}
	protected void LoadQuestion()
	{
		LoadQuestionDivide(10, 5);
	}

	public void OnShowType(int type)
	{
		_type = type;
	}


	protected void LoadQuestionDivide(int min, int max)
	{
		_contentTxt.gameObject.SetActive(false);
		int numb1 = 0;
		int numb2 = 0;
		int numb3 = 0;
		int numb4 = 0;
		int numb5 = 0;
		int numb6 = 0;
		int randomType = Random.Range(0, 4);
		switch (randomType)
		{
			case 0:
				numb1 = Random.Range(min, max);
				numb2 = Random.Range(min, max);
				numb3 = Random.Range(min, max);
				numb4 = Random.Range(min, max);
				numb5 = numb1 * numb3;
				numb6 = numb2 * numb4;
				GenerateFormularDevide(numb1, numb2, numb3, numb4, numb5, numb6, TypeCalculation.Multiplication);
				break;
			case 1:
				numb1 = Random.Range(min, max);
				numb2 = Random.Range(min, max);
				numb3 = Random.Range(min, max);
				numb4 = Random.Range(min, max);
				numb6 = numb2 * numb3;
				numb5 = numb1 * numb4;
				GenerateFormularDevide(numb1, numb2, numb3, numb4, numb5, numb6, TypeCalculation.Division);
				break;
			case 2:
				numb1 = Random.Range(min, max);
				numb2 = Random.Range(min, max);
				numb3 = Random.Range(min, max);
				numb4 = Random.Range(min, max);
				numb6 = numb2 * numb4;
				numb5 = (numb1 * numb4) + (numb3 * numb2);
				GenerateFormularDevide(numb1, numb2, numb3, numb4, numb5, numb6, TypeCalculation.Sum);
				break;
			case 3:
				numb1 = Random.Range(min, max);
				numb2 = Random.Range(min, max);
				numb3 = Random.Range(min, max);
				numb4 = Random.Range(min, max);
				numb6 = numb2 * numb4;
				numb5 = (numb1 * numb4) - (numb3 * numb2);
				GenerateFormularDevide(numb1, numb2, numb3, numb4, numb5, numb6, TypeCalculation.Brand);
				break;

		}
	}

	protected void GenerateFormularDevide(int numb1, int numb2, int numb3, int numb4, int numb5, int numb6, TypeCalculation calculation)
	{
		int randomIndex = Random.Range(0, 6);
		_formular.SpawnFormular(string.Format("{0}/{1} {6} {2}/{3} = {4}/{5}",
						randomIndex == 0 ? "?" : numb1,
						randomIndex == 1 ? "?" : numb2,
						randomIndex == 2 ? "?" : numb3,
						randomIndex == 3 ? "?" : numb4,
						randomIndex == 4 ? "?" : numb5,
						randomIndex == 5 ? "?" : numb6,
						GetTypeCalculation(calculation)
						));
		if (randomIndex == 0)
		{
			_result = numb1;
		}
		else if (randomIndex == 1)
		{
			_result = numb2;
		}
		else if (randomIndex == 2)
		{
			_result = numb3;
		}
		else if (randomIndex == 3)
		{
			_result = numb4;
		}
		else if (randomIndex == 4)
		{
			_result = numb5;
		}
		else if (randomIndex == 5)
		{
			_result = numb6;
		}
		rd.InitResult(_result, _result);
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
	}

	protected IEnumerator IEGenerateQuestion()
	{
		yield return new WaitForSeconds(1.5f);
		icon.gameObject.SetActive(false);
		LoadQuestion();
	}
	public void OnResultChanged(string result)
	{

		//if (_uICaculation.gameObject.activeSelf)
		//{
		//_uICaculation.SetResultUI(result);
		if (result == _result.ToString())
		{
			ResultSucces();
			countQuestionUI.text = _currentQuestion.ToString() + "/" + _maxCurentQuestion.ToString();
		}
		else
		{
			ResultWrong();
		}
		//}

	}
}

