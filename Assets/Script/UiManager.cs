using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; set; }
    [Header("Class Object")]
    [SerializeField] LearningClassOne classOne;

    [Header("PopupObject")]
    [SerializeField] PopupQuitGame popupQuitGame;

    [SerializeField] GameObject parent;
    [SerializeField] private GameObject currentUI;
    [SerializeField] private GameObject beforUI;
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
    
    public void BackMenuGameUI()
    {
        Debug.Log("ckick");
        popupQuitGame.gameObject.SetActive(true);
    }
   
    public void GameOneSumOneNumber()
    {
        GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
        ob.SetActive(true);
        ob.GetComponent<LearningClassOne>().type = TypeOfTopic.OneSumOneNumber;
        ob.GetComponent<LearningClassOne>().CaculationOneDigit("1 + 1 chữ số", TypeCalculation.Sum, 10);
    }
    public void GameOneBrandOneNumber()
    {
        GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
        ob.SetActive(true);
        ob.GetComponent<LearningClassOne>().type = TypeOfTopic.OneBrandOneNumber;
        ob.GetComponent<LearningClassOne>().CaculationOneDigit("1 - 1 chữ số", TypeCalculation.Brand, 10);
    }
   
    public void GameDoubleNumber()
    {
        GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
        ob.SetActive(true);
        ob.GetComponent<LearningClassOne>().type = TypeOfTopic.SumDoubleNumber;
        ob.GetComponent<LearningClassOne>().CaculationDoubleNumber("Cộng gấp đôi", TypeCalculation.Sum, 10);
    }

    public void GameCompareNumber()
    {
        GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
        ob.SetActive(true);
        ob.GetComponent<LearningClassOne>().type = TypeOfTopic.Compare;
        ob.GetComponent<LearningClassOne>().CaculationCompareNumber("Lớn, bé, Bằng (>, <, =)", 20);
    }
    public void GameSummaryCaculation()
    {
        GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
        ob.SetActive(true);
        ob.GetComponent<LearningClassOne>().SummaryCaculation();
        ob.GetComponent<LearningClassOne>().maxCurentQuestion = 20;
        ob.GetComponent<LearningClassOne>().isSummaryCaculation= true;
    }
    #endregion
}
