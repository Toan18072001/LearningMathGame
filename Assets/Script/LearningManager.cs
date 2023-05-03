using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LearningManager : MonoBehaviour
{
    protected char cTypeCatution;
    protected int fistNumber;
    protected int secondNumber;
    protected float result;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Caculition(int maxValue, TypeCalculation type)
    {
        fistNumber = Random.Range(0, maxValue);
      //  secondNumber = Random.Range(0, maxValue - fistNumber);

        switch (type)
        {
            case TypeCalculation.Sum:
                secondNumber = Random.Range(0, maxValue - fistNumber);
                result = fistNumber + secondNumber;
                break;
            case TypeCalculation.Brand:
                secondNumber = Random.Range(0,fistNumber);
                result = fistNumber - secondNumber;
                break;
            case TypeCalculation.Multiplication:
                result = fistNumber * secondNumber;
                break;
            case TypeCalculation.Division:
                result = (float)fistNumber / (float)secondNumber;
                break;
        }
        
        Debug.Log("FistNumber " + fistNumber + "SeconNumber: " + secondNumber + "Result" + result);
    }

    protected void CaculitionDouble(int maxValue, TypeCalculation type)
    {
        fistNumber = Random.Range(0, maxValue);
        secondNumber = fistNumber;

        switch (type)
        {
            case TypeCalculation.Sum:
                result = fistNumber + secondNumber;
                break;
            case TypeCalculation.Brand:
                result = fistNumber - secondNumber;
                break;
            case TypeCalculation.Multiplication:
                result = fistNumber * secondNumber;
                break;
            case TypeCalculation.Division:
                result = (float)fistNumber / (float)secondNumber;
                break;
        }

        Debug.Log("FistNumber " + fistNumber + "SeconNumber: " + secondNumber + "Result" + result);
    }
    protected void CaculitionCompare(int maxValue)
    {
        fistNumber = Random.Range(0, maxValue);
        secondNumber = Random.Range(0, maxValue);

        Debug.Log("FistNumber " + fistNumber + "SeconNumber: " + secondNumber);
    }
    protected char GetTypeCalculation(TypeCalculation type)
    {
        //char c_Calculation;
        switch (type)
        {
            case TypeCalculation.Sum:
                cTypeCatution = '+';
                break;
            case TypeCalculation.Brand:
                cTypeCatution = '-';
                break;
            case TypeCalculation.Multiplication:
                cTypeCatution = '*';
                break;
            case TypeCalculation.Division:
                cTypeCatution = ':';
                break;
        }
        return cTypeCatution;
    }
}
