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


	protected string[] _unitWeight = new string[] { "Gam", "Đềcagam", "Hectôgam", "Kilogam", "Yến", "Tạ", "Tấn", };
	protected string[] _unitHeight = new string[] { "Milimet", "Centimet", "Decimet", "Met", "Đềcamet", "Hectômet", "Kilomet" };
	protected int _currentQuestion = 1;
	protected int _maxCurentQuestion = 20;
	protected Question[] _questionDatas;
	protected float _result = 0;
	protected float _unitValue = 0.25f;
	protected float _unitCircleValue = 0.01f;

	protected int _type;
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

	public void OnShowType(int type)
	{
		_type = type;
	}
	public void LoadQuestion()
	{
		switch (_type)
		{
			case 0:
				LoadQuestionShape();
				break;
			case 1:
				LoadQuestionUnit();
				break;
		}
		//DrawTriangle(ref numb1, ref numb2, ref numb3);

	}

	private void LoadQuestionUnit()
	{
		_contentTxt.gameObject.SetActive(false);
		int typeUnit = Random.Range(0, 2);
		int randomValue = Random.Range(0, 10);


		int randomIndex = 0;
		int randomIndex2 = 0;
		int numb1 = 0;
		int numb2 = 0;
		switch (typeUnit)
		{
			case 0:
				randomIndex = Random.Range(0, _unitHeight.Length);
				randomIndex2 = Random.Range(0, _unitHeight.Length);
				while (randomIndex2 == randomIndex)
				{
					randomIndex2 = Random.Range(0, _unitHeight.Length);
				}
				if (randomIndex > randomIndex2)
				{
					numb1 = Mathf.FloorToInt(randomValue * Mathf.Pow(10, randomIndex - randomIndex2));
					numb2 = randomValue;

				}
				else
				{
					numb2 = Mathf.FloorToInt(randomValue * Mathf.Pow(10, randomIndex2 - randomIndex));
					numb1 = randomValue;
				}
				_result = Random.Range(0, 100) >= 50 ? numb1 : numb2;
				_formular.SpawnFormular(string.Format("{0} {1} = {2} {3}", numb2 == _result ? "?" : numb2, _unitHeight[randomIndex], numb1 == _result ? "?" : numb1, _unitHeight[randomIndex2]));
				rd.InitResult((int)_result, _result);
				break;
			case 1:
				randomIndex = Random.Range(0, _unitWeight.Length);
				randomIndex2 = Random.Range(0, _unitWeight.Length);
				while (randomIndex2 == randomIndex)
				{
					randomIndex2 = Random.Range(0, _unitHeight.Length);
				}
				if (randomIndex > randomIndex2)
				{
					numb1 = Mathf.FloorToInt(randomValue * Mathf.Pow(10, randomIndex - randomIndex2));
					numb2 = randomValue;

				}
				else
				{
					numb2 = Mathf.FloorToInt(randomValue * Mathf.Pow(10, randomIndex2 - randomIndex));
					numb1 = randomValue;
				}
				_result = Random.Range(0, 100) >= 50 ? numb1 : numb2;
				_formular.SpawnFormular(string.Format("{0} {1} = {2} {3}", numb2 == _result ? "?" : numb2, _unitWeight[randomIndex], numb1 == _result ? "?" : numb1, _unitWeight[randomIndex2]));
				rd.InitResult((int)_result, _result);
				break;
		}

	}

	private void LoadQuestionShape()
	{
		_contentTxt.gameObject.SetActive(true);
		_customText1.gameObject.SetActive(false);
		_customText2.gameObject.SetActive(false);
		float numb1 = 0;
		float numb2 = 0;
		float numb3 = 0;
		int shape = Random.Range(0, 4);
		//shape = 3;
		switch (shape)
		{
			case 0:
				DrawTriangle(ref numb1, ref numb2, ref numb3);
				break;
			case 1:
				DrawCircle(ref numb1, ref numb2);
				break;
			case 2:
				DrawSquare(ref numb1, ref numb2);
				break;
			case 3:
				DrawRectancle(ref numb1, ref numb2, ref numb3);
				break;
		}
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
	#region Shape Math

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

			case 2:
				numb2 = 2 * Mathf.PI * numb1;
				_contentTxt.text = string.Format("Cho hình tròn có bán kính là {0} . Tìm  chu vi tròn?", numb1);
				_formular.SpawnFormular(string.Format("R = {0} , P = ?", numb1));
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

	private void DrawSquare(ref float numb1, ref float numb2)
	{

		numb1 = Random.Range(5, 30);
		numb2 = numb1 / 2;
		_line.positionCount = 5;
		_line.SetPosition(0, _line2.transform.position + (Vector3.left * _unitValue * numb2) + (Vector3.up * _unitValue * numb2));
		_line.SetPosition(1, _line2.transform.position + (Vector3.right * _unitValue * numb2) + (Vector3.up * _unitValue * numb2));
		_line.SetPosition(2, _line2.transform.position + (Vector3.right * _unitValue * numb2) + (Vector3.down * _unitValue * numb2));
		_line.SetPosition(3, _line2.transform.position + (Vector3.left * _unitValue * numb2) + (Vector3.down * _unitValue * numb2));
		_line.SetPosition(4, _line.GetPosition(0));
		int type = Random.Range(0, 3);
		switch (type)
		{
			case 0:
				numb2 = numb1 * numb1;
				_contentTxt.text = string.Format("cho hình vuông có chiều dài cạnh là {0}. Tìm diện tich?", numb1);
				_formular.SpawnFormular(string.Format("a = {0} , S = ?", numb1));
				_result = numb2;
				rd.InitResult(100, numb2);
				break;
			case 1:
				numb2 = numb1 * numb1;
				_contentTxt.text = string.Format("cho hình vuông có diện tích là {0}. Tìm độ dài cạnh?", numb2);
				_formular.SpawnFormular(string.Format("S = {0} , a = ?", numb2));
				_result = numb1;
				rd.InitResult(100, numb1);
				break;

			case 2:
				numb2 = 2 * Mathf.PI * numb1;
				_contentTxt.text = string.Format("cho hình vuông có chiều dài cạnh là {0}. Tìm chu vi?", numb1);
				_formular.SpawnFormular(string.Format("a = {0} , P = ?", numb1));
				_result = numb2;
				rd.InitResult(100, numb1);
				break;

				//case 2:
				//	_contentTxt.text = string.Format("Cho hình tam giác diện tích là {0} và độ dài cạnh đáy là {1}. Tìm chiều cao từ cạnh đáy?", numb1, numb2);
				//	_questionTxt.text = string.Format("S = {0} , a= {1}\n h=?", numb3, numb1);
				//	rd.InitResult(100, numb2);
				//	break;
		}
	}

	private void DrawRectancle(ref float numb1, ref float numb2, ref float numb3)
	{

		numb1 = Random.Range(5, 30);
		numb2 = Random.Range(5, 30);
		_line.positionCount = 5;
		_line.SetPosition(0, _line2.transform.position + (Vector3.left * _unitValue * numb1 / 2) + (Vector3.up * _unitValue * numb2 / 2));
		_line.SetPosition(1, _line2.transform.position + (Vector3.right * _unitValue * numb1 / 2) + (Vector3.up * _unitValue * numb2 / 2));
		_line.SetPosition(2, _line2.transform.position + (Vector3.right * _unitValue * numb1 / 2) + (Vector3.down * _unitValue * numb2 / 2));
		_line.SetPosition(3, _line2.transform.position + (Vector3.left * _unitValue * numb1 / 2) + (Vector3.down * _unitValue * numb2 / 2));
		_line.SetPosition(4, _line.GetPosition(0));
		int type = Random.Range(0, 4);
		switch (type)
		{
			case 0:
				numb3 = numb1 * numb2;
				_contentTxt.text = string.Format("cho hình chữ nhật có chiều dài cạnh lần lượt là a = {0} b = {1}. Tìm diện tich?", numb1, numb2);
				_formular.SpawnFormular(string.Format("a = {0} b = {1} S = ?", numb1, numb2));
				_result = numb3;
				rd.InitResult(100, numb2);
				break;
			case 1:
				numb3 = numb1 * numb2;
				_contentTxt.text = string.Format("cho hình chữ nhật có chiều dài cạnh là a = {0} và diện tích là {1} .Tìm độ dài cạnh còn lại b = ?", numb1, numb3);
				_formular.SpawnFormular(string.Format("a = {0}  S = {1} b = ?", numb1, numb3));
				_result = numb2;
				rd.InitResult(100, numb1);
				break;

			case 2:
				numb3 = (numb1 + numb2) * 2;
				_contentTxt.text = string.Format("cho hình chữ nhật có chiều dài cạnh lần lượt là a = {0} b = {1}. Tìm Chu vi?", numb1, numb2);
				_formular.SpawnFormular(string.Format("a = {0} b = {1} P = ?", numb1, numb2));
				_result = numb2;
				rd.InitResult(100, numb3);
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



		float numb4 = 0;
		int type = Random.Range(0, 4);
		switch (type)
		{
			case 0:
				numb3 = 0.5f * numb1 * numb2;
				_contentTxt.text = string.Format("Cho hình tam giác có độ dài cạnh đáy là {0} và chiều cao từ cạnh đáy là {1}. Tìm diện tích hình tam giác?", numb1, numb2);
				_formular.SpawnFormular(string.Format("a = {0} , h = {1} S = ?", numb1, numb2));
				rd.InitResult(100, numb3);
				break;
			case 1:
				numb3 = 0.5f * numb1 * numb2;
				_contentTxt.text = string.Format("Cho hình tam giác có diện tích là {0} và chiều cao từ cạnh đáy là {1}. Tìm độ dài cạnh đáy hình tam giác?", numb1, numb2);
				_formular.SpawnFormular(string.Format("S = {0} , h = {1}  a = ?", numb3, numb2));
				rd.InitResult(100, numb1);
				break;
			case 2:
				numb3 = 0.5f * numb1 * numb2;
				_contentTxt.text = string.Format("Cho hình tam giác diện tích là {0} và độ dài cạnh đáy là {1}. Tìm chiều cao từ cạnh đáy?", numb1, numb2);
				_formular.SpawnFormular(string.Format("S = {0} , a= {1}  h = ?", numb3, numb1));
				rd.InitResult(100, numb2);
				break;

			case 3:
				numb1 = Mathf.Floor(Vector3.Distance(point1, point2));
				numb2 = Mathf.Floor(Vector3.Distance(point2, point3));
				numb3 = Mathf.Floor(Vector3.Distance(point3, point1));
				numb4 = numb1 + numb2 + numb3;
				_contentTxt.text = string.Format("Cho hình tam giác có độ dài cạnh đáy lần lượt là a = {0} b= {1} c = {2}. tính chu vi P=?", numb1, numb2, numb3);
				_formular.SpawnFormular(string.Format("a = {0}  b= {1}  c = {2} P = ?", numb1, numb2, numb3));
				rd.InitResult(100, numb4);
				break;
		}

	}
	#endregion
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

