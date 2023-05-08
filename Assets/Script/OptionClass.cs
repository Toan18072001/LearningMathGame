using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OptionClass : MonoBehaviour
{
    [SerializeField] List<GameObject> obBtns;
    [SerializeField] TMP_Text label;
    public event Action<int> ChangeGame;
    int indexIcon;
    // Start is called before the first frame update
    void Start()
    {
        EnableOption();
        obBtns[0].transform.GetChild(2).gameObject.SetActive(true);
        indexIcon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnableOption()
    {
        for (int i = 0; i < obBtns.Count; i++)
        {
            obBtns[i].transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    public void ClickOptionClass(int index)
    {
        EnableOption();
        obBtns[indexIcon].transform.GetChild(2).gameObject.SetActive(false);
        obBtns[index].transform.GetChild(2).gameObject.SetActive(true);
        ChangeGame?.Invoke(index);
        label.text = obBtns[index].transform.GetChild(0).GetComponent<TMP_Text>().text;
        indexIcon = index;
        transform.GetChild(2).transform.gameObject.SetActive(false);
    }
}
