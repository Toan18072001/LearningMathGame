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
        //classOne.PlusOneDigit("1 + 1 chữ số");
        //currentUI.SetActive(false);
        //beforUI = currentUI;
        //currentUI = classOne.gameObject;
        //classOne.gameObject.SetActive(true);
        GameObject ob = Instantiate(classOne, parent.transform.position, Quaternion.identity, parent.transform).gameObject;
        ob.SetActive(true);
        ob.GetComponent<LearningClassOne>().PlusOneDigit("1 + 1 chữ số");
    }
    #endregion
}
