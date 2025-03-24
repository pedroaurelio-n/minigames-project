using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.ThrowObjects;
    
    IThrowObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IThrowObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly ThrowObjectsSceneView _sceneView;

    public ThrowObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as ThrowObjectsSceneView;
    }

    public override void Initialize ()
    {
        //TODO pedro: remove null condition
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return true;
    }

    protected override void AddListeners ()
    {
        base.AddListeners();
        MiniGameModel.OnSwipePerformed += HandleSwipePerformed;
    }

    protected override void RemoveListeners ()
    {
        if (MiniGameModel == null)
            return;
        
        base.RemoveListeners();
        MiniGameModel.OnSwipePerformed -= HandleSwipePerformed;
    }

    void HandleSwipePerformed (Vector3 swipeDirection)
    {
        ThrowableObjectView obj = Object.Instantiate(
            _sceneView.ThrowableObjectPrefab,
            _sceneView.SpawnPoint.transform.position,
            Quaternion.identity,
            _sceneView.transform
        );
        obj.Throw(swipeDirection);
    }
}