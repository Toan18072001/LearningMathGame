using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RangdomResult : MonoBehaviour
{
    [SerializeField] List<GameObject> randomResult;

    [SerializeField] GameObject numberBtns;
    [SerializeField] GameObject compareBtns;

    public event Action<string> onResultChanged;
    public event Action<string> onResultCompareChange;
    // Start is called before the first frame update
    public void InitResult(int _maxValue, float _resultSuccess)
    {
        numberBtns.SetActive(true);
        compareBtns.SetActive(false);
        int rd = Random.Range(0, randomResult.Count);
        Debug.Log("ranđomPos: " + rd);
        for(int i = 0; i < randomResult.Count; i++)
        {
            if (i == rd)
            {
                randomResult[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = _resultSuccess.ToString();
                randomResult[i].GetComponent<Button>().onClick.AddListener(delegate
                {
                    ChangeNumberResult(_resultSuccess.ToString());
                });
            }
            else
            {
                int resultWrong = Random.Range(0, _maxValue);
                Debug.Log("i: " + i + " resultWrong: " + resultWrong);
                if (CheckLoopNumber(i, resultWrong.ToString()))
                {
                    resultWrong = Random.Range(0, _maxValue);
                }
                else
                {
                    if (resultWrong == _resultSuccess)
                    {
                        resultWrong = Random.Range(0, _maxValue);
                    }
                    randomResult[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = resultWrong.ToString();
                    randomResult[i].GetComponent<Button>().onClick.AddListener(delegate
                    {
                        ChangeNumberResult(resultWrong.ToString());
                    });
                }
                
            }
        }
    }
    public void InitResult()
    {
        compareBtns.SetActive(true);
        numberBtns.SetActive(false);
    }
    public void ChangeNumberResult(string result)
    {
        onResultChanged?.Invoke(result);
    }
    

    public bool CheckLoopNumber(int maxIndex, string number)
    {
        bool isLoop = false;
        for(int i = 0; i< maxIndex; i++)
        {
            if(randomResult[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text == number)
            {
                isLoop= true;
                break;
            }
        }
        return isLoop;
    }
}
