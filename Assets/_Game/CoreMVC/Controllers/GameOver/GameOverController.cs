public class GameOverController
{
    readonly IGameOverModel _model;
    readonly GameOverView _view;
    
    public GameOverController (
        IGameOverModel model,
        MenuView view
    )
    {
        _model = model;
        _view = view as GameOverView;
    }
}