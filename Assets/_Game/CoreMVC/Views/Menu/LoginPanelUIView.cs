using System;
using TMPro;
using UnityEngine;

public class LoginPanelUIView : MonoBehaviour
{
    public event Action OnLoginButtonClick;
    public event Action OnRegisterButtonClick;
    
    [field: SerializeField] public TMP_InputField EmailInputField { get; private set; }
    [field: SerializeField] public TMP_InputField PasswordField { get; private set; }
    
    [SerializeField] ButtonUIComponent loginButton;
    [SerializeField] ButtonUIComponent registerButton;

    void Awake ()
    {
        loginButton.OnClick += () => OnLoginButtonClick?.Invoke();
        registerButton.OnClick += () => OnRegisterButtonClick?.Invoke();
    }
}