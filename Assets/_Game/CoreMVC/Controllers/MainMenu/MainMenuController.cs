public class MainMenuController
{
    readonly IMainMenuModel _model;
    readonly MainMenuView _view;
    
    public MainMenuController (
        IMainMenuModel model,
        MenuView view
    )
    {
        _model = model;
        _view = view as MainMenuView;
    }
}