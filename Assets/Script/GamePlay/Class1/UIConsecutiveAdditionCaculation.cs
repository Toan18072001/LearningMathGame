using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConsecutiveAdditionCaculation : MonoBehaviour
{
    [SerializeField] ScrollRect listCaculation;
    [SerializeField] UICaculation item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Init(string _fistNumber, string _secondNumber, string TypeCalculation, int cout)
    {
       for(int i =0;i<cout;i++)
        {
            Debug.Log("UIConsecutiveAdditionCaculation index: " + i);
            UICaculation ob = Instantiate(item, listCaculation.content.transform.position, Quaternion.identity, listCaculation.content.transform);
            ob.Init(_fistNumber, _secondNumber, TypeCalculation);
        }
    }
}
