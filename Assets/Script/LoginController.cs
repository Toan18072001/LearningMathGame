using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginController : PlayFabManager
{
    [SerializeField] TMP_InputField m_TMP_email;
    [SerializeField] TMP_InputField m_TMP_password;
    [SerializeField] TMP_Text m_TMP_Error;
    // Start is called before the first frame update
    public void LoginGame()
    {
        Login(m_TMP_email.text,m_TMP_password.text);
    }
    private void OnEnable()
    {
        OnLoginEvent += OnhanldeLoginEvent;
        m_TMP_Error.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        OnLoginEvent -= OnhanldeLoginEvent;
    }
    void OnhanldeLoginEvent(string _status)
    {
        if (status == "LoginSuccess")
        {
           // UiManager.instance.ShowUI(4);
        }
        else
        {
            m_TMP_Error.text = status;
            m_TMP_Error.gameObject.SetActive(true);
        }
    }
}
