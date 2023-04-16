using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; set; }
    [SerializeField] List<GameObject> objectsUI;


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
    public void ShowUI(int index)
    {
        currentUI.SetActive(false);
        objectsUI[index].SetActive(true);
        currentUI = objectsUI[index];
    }

}
