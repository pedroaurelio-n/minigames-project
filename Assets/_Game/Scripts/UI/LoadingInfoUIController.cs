using UnityEngine;

public class LoadingInfoUIController
{
    readonly LoadingInfoUIView _view;
    readonly IPlayerInfoModel _playerInfoModel;

    public LoadingInfoUIController (
        LoadingInfoUIView view,
        IPlayerInfoModel playerInfoModel
    )
    {
        _view = view;
        _playerInfoModel = playerInfoModel;
    }

    public void Enable ()
    {
        _view.gameObject.SetActive(true);
        SyncView();
    }

    public void Disable ()
    {
        _view.gameObject.SetActive(false);
    }

    void SyncView ()
    {
        _view.SetLivesAmount(_playerInfoModel.CurrentLives.ToString());
        _view.SetScoreAmount(_playerInfoModel.CurrentScore.ToString());
        
        int livesChange = _playerInfoModel.GetLivesChangeType();
        int scoreChange = _playerInfoModel.GetScoreChangeType();
        _view.SetLivesColor(livesChange == 0 ? Color.white : livesChange > 0 ? Color.green : Color.red);
        _view.SetScoreColor(scoreChange == 0 ? Color.white : scoreChange > 0 ? Color.green : Color.red);
    }
}