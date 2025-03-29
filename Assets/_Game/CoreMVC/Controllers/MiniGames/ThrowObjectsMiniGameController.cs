using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.ThrowObjects;
    
    IThrowObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IThrowObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly ThrowObjectsSceneView _sceneView;

    bool _hasCompleted;

    public ThrowObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as ThrowObjectsSceneView;
        _randomProvider = randomProvider;
    }

    public override void Initialize ()
    {
        //TODO pedro: remove null condition
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
        AddViewListeners();
    }

    protected override void SetupMiniGame()
    {
        _sceneView.Container.transform.position =
            _sceneView.ContainerSpawnPoints[_randomProvider.Range(0, _sceneView.ContainerSpawnPoints.Length)].position;
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        //TODO pedro: avoid double method call when the game completes
        if (timerEnded)
        {
            //TODO pedro: implement ui
            string message = _hasCompleted ? "YOU WIN THE GAME" : "YOU LOSE THE GAME";
            DebugUtils.Log(message);
        }
        return _hasCompleted;
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

    void AddViewListeners ()
    {
        _sceneView.Container.OnThrowableEnter += HandleThrowableEnter;
    }

    void RemoveViewListeners ()
    {
        if (_sceneView == null)
            return;
        _sceneView.Container.OnThrowableEnter -= HandleThrowableEnter;
    }

    void HandleSwipePerformed (Vector3 swipeDirection)
    {
        //TODO pedro: use object pooling to reuse all these dynamically created objects
        ThrowableObjectView obj = Object.Instantiate(
            _sceneView.ThrowableObjectPrefab,
            _sceneView.ThrowableSpawnPoint.transform.position,
            Quaternion.identity,
            _sceneView.transform
        );
        obj.Throw(swipeDirection);
    }

    void HandleThrowableEnter ()
    {
        _hasCompleted = true;
        
        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    public override void Dispose ()
    {
        RemoveViewListeners();
        base.Dispose();
    }
}