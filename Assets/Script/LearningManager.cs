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
        Caculition(10, TypeCalculation.Sum);
        Caculition(10, TypeCalculation.Brand);
        Caculition(10, TypeCalculation.Multiplication);
        Caculition(10, TypeCalculation.Division);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Caculition(int maxValue, TypeCalculation type)
    {
        fistNumber = Random.Range(0, maxValue);
        secondNumber = Random.Range(0, maxValue - fistNumber);

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

    //char GetTypeCalculation(TypeCalculation type)
    //{
    //    char c_Calculation;
    //    switch (type)
    //    {
    //        case TypeCalculation.Sum:
    //            c_Calculation = '+';
    //            break;
    //        case TypeCalculation.Brand:
    //            c_Calculation = '-';
    //            break;
    //        case TypeCalculation.Multiplication:
    //            c_Calculation =  '*';
    //            break;
    //        case TypeCalculation.Division:
    //            c_Calculation =  '/';
    //            break;
    //    }
    //    return c_Calculation;
    //}
}
