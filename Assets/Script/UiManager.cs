using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
	public static UiManager instance { get; set; }
	[Header("Class Object")]
	[SerializeField] LearningClassOne classOne;
	[SerializeField] LearningClassTwo classTwo;
	[SerializeField] LearningClassThree classThree;

	[SerializeField] GameObject parent;
	[SerializeField] private GameObject currentUI;
	[SerializeField] private GameObject beforUI;
	[SerializeField] List<GameObject> obClass;
	private void Awake()
	{
		instance = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		//currentUI= objectsUI[0];
		//currentUI.SetActive(true);
		//for(int i=1;i<objectsUI.Count;i++)
		//{
		//    objectsUI[i].SetActive(false);
		//}
	}
	#region code logic no ref UI
	public void BackUI()
	{
		//GameObject tmp = currentUI;
		//beforUI.SetActive(true);
		//currentUI.SetActive(false);
		//currentUI = beforUI;
		//beforUI = tmp;
		Destroy(parent.transform.GetChild(1));

	}

	public void ShowMenuGame()
	{
		SceneManager.LoadScene("MenuGameScene");
	}
	#endregion

	#region Ref UI

	public void GameOneSumOneNumber(int value)
	{
		GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
		ob.SetActive(true);
		ob.GetComponent<LearningClassOne>().type = TypeOfTopic.OneSumOneNumber;
		ob.GetComponent<LearningClassOne>().maxCurentQuestion = 10;
		ob.GetComponent<LearningClassOne>().CaculationOneDigit("1 + 1 chữ số", TypeCalculation.Sum, value);
	}
	public void GameOneBrandOneNumber(int value)
	{
		GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
		ob.SetActive(true);
		ob.GetComponent<LearningClassOne>().type = TypeOfTopic.OneBrandOneNumber;
		ob.GetComponent<LearningClassOne>().maxCurentQuestion = 10;
		ob.GetComponent<LearningClassOne>().CaculationOneDigit("1 - 1 chữ số", TypeCalculation.Brand, value);
	}

	public void GameDoubleNumber(int value)
	{
		GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
		ob.SetActive(true);
		ob.GetComponent<LearningClassOne>().type = TypeOfTopic.SumDoubleNumber;
		ob.GetComponent<LearningClassOne>().maxCurentQuestion = 10;
		ob.GetComponent<LearningClassOne>().CaculationDoubleNumber("Cộng gấp đôi", TypeCalculation.Sum, value);
	}

	public void GameCompareNumber(int value)
	{
		GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
		ob.SetActive(true);
		ob.GetComponent<LearningClassOne>().type = TypeOfTopic.Compare;
		ob.GetComponent<LearningClassOne>().maxCurentQuestion = 10;
		ob.GetComponent<LearningClassOne>().CaculationCompareNumber("Lớn, bé, Bằng (>, <, =)", value);
	}
	public void GameSummaryCaculation()
	{
		GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
		ob.SetActive(true);
		ob.GetComponent<LearningClassOne>().SummaryCaculation();
		ob.GetComponent<LearningClassOne>().maxCurentQuestion = 20;
		ob.GetComponent<LearningClassOne>().isSummaryCaculation = true;
	}

	public void GameWithQuestionContent()
	{
		var ob = Instantiate(classTwo, parent.transform.position, Quaternion.identity, parent.transform);
		classTwo.OnShowType(0);
		ob.gameObject.SetActive(true);
	}

	public void GameWithQuestionFindNumber()
	{
		var ob = Instantiate(classTwo, parent.transform.position, Quaternion.identity, parent.transform);
		classTwo.OnShowType(1);
		ob.gameObject.SetActive(true);
	}

	public void GameWithShape()
	{
		var ob = Instantiate(classThree, parent.transform.position, Quaternion.identity, parent.transform);
		ob.gameObject.SetActive(true);
	}
	#endregion
}
