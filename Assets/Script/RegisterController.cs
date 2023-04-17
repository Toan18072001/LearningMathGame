using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterController : PlayFabManager
{
    [SerializeField] TMP_InputField m_TMP_Email;
    [SerializeField] TMP_InputField m_TMP_Password;
    [SerializeField] TMP_InputField m_TMP_UserName;
    [SerializeField] TMP_Text m_TMP_Error;

    public void RegisterUser()
    {
        Register(m_TMP_UserName.text, m_TMP_Email.text, m_TMP_Password.text);
    }
    public void ShowOrHilePassword()
    {
        if(m_TMP_Password.contentType == TMP_InputField.ContentType.Password)
        {
            m_TMP_Password.contentType = TMP_InputField.ContentType.Standard;
            m_TMP_Password.Select();
        }
        else
        {
            m_TMP_Password.contentType = TMP_InputField.ContentType.Password;
            m_TMP_Password.Select();
        }
    }
    private void OnEnable()
    {
        OnRegisterEvent += OnhandleRegisterEvent;
        m_TMP_Error.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        OnRegisterEvent -= OnhandleRegisterEvent;
    }
    void OnhandleRegisterEvent(string _status)
    {
        if (status == "RegisterSuccess")
        {
            //UiManager.instance.ShowUI(1);
        }
        else
        {
            m_TMP_Error.text = status;
            m_TMP_Error.gameObject.SetActive(true);
        }
    }
}
