using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LearningClassThree : LearningManager
{
	[Header("Line")]

	[SerializeField] private LineRenderer _line;
	[SerializeField] private LineRenderer _line2;
	[SerializeField] private LineRenderer _line3;
	[SerializeField] private LineRenderer _line4;
	[Header("body")]
	[SerializeField] TMP_Text countQuestionUI;
	[SerializeField] protected TextMeshProUGUI _contentTxt;
	[SerializeField] protected UICaculation _uICaculation;
	[SerializeField] protected FormularController _formular;
	[SerializeField] protected RangdomResult rd;

	[SerializeField] protected TextMeshProUGUI _customText1;
	[SerializeField] protected TextMeshProUGUI _customText2;

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
	protected float _result = 0;
	protected float _unitValue = 0.25f;
	protected float _unitCircleValue = 0.01f;
	private void Start()
	{

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
	public void LoadQuestion()
	{
		_customText1.gameObject.SetActive(false);
		_customText2.gameObject.SetActive(false);
		float numb1 = 0;
		float numb2 = 0;
		float numb3 = 0;
		//DrawTriangle(ref numb1, ref numb2, ref numb3);
		DrawCircle(ref numb1, ref numb2);
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
	private void DrawCircle(ref float numb1, ref float numb2)
	{
		int point = 71;
		float angle = 360 / (float)point;
		float currentAngle = 0;
		_line.positionCount = point;
		numb1 = Random.Range(5, 30);
		for (int i = 0; i < point; i++)
		{
			currentAngle = angle * i;
			_line.SetPosition(i, _line2.transform.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.up * numb1 * _unitCircleValue);
		}
		_line.SetPosition(point - 1, _line.GetPosition(0));
		int type = Random.Range(0, 2);
		numb2 = (float)System.Math.Round(numb1 * numb1 * Mathf.PI, 2);

		_line2.SetPosition(0, _line2.transform.position);
		_line2.SetPosition(1, _line.GetPosition(0));


		switch (type)
		{
			case 0:
				_contentTxt.text = string.Format("Cho hình tròn có bán kính là {0} . Tìm diện tích hình tròn?", numb1);
				_formular.SpawnFormular(string.Format("R = {0} , S = ?", numb1));
				_result = numb2;
				rd.InitResult(100, numb2);
				break;
			case 1:
				_contentTxt.text = string.Format("Cho hình tròn có diện tích là {0} . Tìm bán kính hình tròn?", numb2);
				_formular.SpawnFormular(string.Format("S = {0} , R = ?", numb2));
				_result = numb1;
				rd.InitResult(100, numb1);
				break;
				//case 2:
				//	_contentTxt.text = string.Format("Cho hình tam giác diện tích là {0} và độ dài cạnh đáy là {1}. Tìm chiều cao từ cạnh đáy?", numb1, numb2);
				//	_questionTxt.text = string.Format("S = {0} , a= {1}\n h=?", numb3, numb1);
				//	rd.InitResult(100, numb2);
				//	break;
		}
	}
	private void DrawTriangle(ref float numb1, ref float numb2, ref float numb3)
	{
		_customText1.gameObject.SetActive(true);
		_customText2.gameObject.SetActive(true);
		int point1Rand = Random.Range(3, 7);
		int point2Rand = Random.Range(3, 7);
		Vector3 point1 = new Vector3(_line.transform.position.x - point1Rand * _unitValue, _line.transform.position.y, _line.transform.position.z);
		Vector3 point2 = new Vector3(_line.transform.position.x + point2Rand * _unitValue, _line.transform.position.y, _line.transform.position.z);
		numb1 = point1Rand + point2Rand;


		int point3Rand = Random.Range(3, 8);
		Vector3 point4 = _line.transform.position;
		numb2 = point3Rand;
		Vector3 point3 = point4 + Vector3.up * numb2 * _unitValue;
		_customText1.text = "a";
		_customText1.transform.position = _line.transform.position + Vector3.down * 0.2f;

		_customText2.text = "h";
		_customText2.transform.position = Vector3.Lerp(point3, point4, 0.5f) + Vector3.right * 0.2f;


		_line.positionCount = 4;
		_line.SetPosition(0, point1);
		_line.SetPosition(1, point2);
		_line.SetPosition(2, point3);
		_line.SetPosition(3, point1);


		_line2.SetPosition(0, point3);
		_line2.SetPosition(1, point4);

		numb3 = 0.5f * numb1 * numb2;


		int type = Random.Range(0, 3);
		switch (type)
		{
			case 0:
				_contentTxt.text = string.Format("Cho hình tam giác có độ dài cạnh đáy là {0} và chiều cao từ cạnh đáy là {1}. Tìm diện tích hình tam giác?", numb1, numb2);
				_formular.SpawnFormular(string.Format("a = {0} , h = {1} S = ?", numb1, numb2));
				rd.InitResult(100, numb3);
				break;
			case 1:
				_contentTxt.text = string.Format("Cho hình tam giác có diện tích là {0} và chiều cao từ cạnh đáy là {1}. Tìm độ dài cạnh đáy hình tam giác?", numb1, numb2);
				_formular.SpawnFormular(string.Format("S = {0} , h = {1}  a = ?", numb3, numb2));
				rd.InitResult(100, numb1);
				break;
			case 2:
				_contentTxt.text = string.Format("Cho hình tam giác diện tích là {0} và độ dài cạnh đáy là {1}. Tìm chiều cao từ cạnh đáy?", numb1, numb2);
				_formular.SpawnFormular(string.Format("S = {0} , a= {1}  h = ?", numb3, numb1));
				rd.InitResult(100, numb2);
				break;
		}

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
}

