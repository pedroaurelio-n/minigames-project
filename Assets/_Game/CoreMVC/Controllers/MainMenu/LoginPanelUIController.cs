using System;

public class LoginPanelUIController : IDisposable
{
    readonly IMainMenuModel _model;
    readonly LoginPanelUIView _view;
    
    public LoginPanelUIController (
        IMainMenuModel model,
        LoginPanelUIView view
    )
    {
        _model = model;
        _view = view;
    }

    public void Initialize ()
    {
        AddListeners();
        AddViewListeners();
    }
    
    void Enable ()
    {
        _view.SetActive(true);
    }

    void Disable ()
    {
        _view.SetActive(false);
    }

    void AddListeners ()
    {
        _model.OnMainMenuStateChanged += HandleMainMenuStateChanged;
    }
    
    void RemoveListeners ()
    {
        _model.OnMainMenuStateChanged -= HandleMainMenuStateChanged;
    }

    void AddViewListeners ()
    {
        _view.OnLoginButtonClick += HandleLoginButtonClick;
        _view.OnRegisterButtonClick += HandleRegisterButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnLoginButtonClick -= HandleLoginButtonClick;
        _view.OnRegisterButtonClick -= HandleRegisterButtonClick;
    }

    void HandleMainMenuStateChanged (MainMenuState newState)
    {
        if (newState == MainMenuState.Login)
        {
            Enable();
            return;
        }
        
        Disable();
    }

    void HandleLoginButtonClick ()
    {
        _model.Login(_view.EmailInputField.text, _view.PasswordField.text);
    }
    
    void HandleRegisterButtonClick ()
    {
        _model.Register(_view.EmailInputField.text, _view.PasswordField.text);
    }
    
    public void Dispose ()
    {
        RemoveListeners();
        RemoveViewListeners();
    }
}