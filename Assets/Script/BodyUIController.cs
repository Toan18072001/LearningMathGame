using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyUIController : MonoBehaviour
{
    [SerializeField] OptionClass optionClass;
    [SerializeField] List<GameObject> UIListGameClass;
    int classGame;
    // Start is called before the first frame update
    void Start()
    {
        classGame = 0;
        optionClass.ChangeGame += GetClassGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showOptionClass()
    {
        optionClass.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void GetClassGame(int index)
    {
        UIListGameClass[classGame].gameObject.SetActive(false);
        UIListGameClass[index].gameObject.SetActive(true);
        classGame = index;
    }
}
