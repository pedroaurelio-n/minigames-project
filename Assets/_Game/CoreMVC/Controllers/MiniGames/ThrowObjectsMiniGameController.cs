using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.ThrowObjects;
    
    IThrowObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IThrowObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly ThrowObjectsSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly List<ThrowableObjectView> _objectViews = new();

    bool _hasCompleted;

    public ThrowObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as ThrowObjectsSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
    }

    public override void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
        AddViewListeners();
    }

    protected override void SetupMiniGame()
    {
        base.SetupMiniGame();
        
        _viewFactory.SetupPool(_sceneView.ThrowableObjectPrefab);
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
        ThrowableObjectView obj = _viewFactory.GetView<ThrowableObjectView>(_sceneView.transform);
        obj.Setup(_sceneView.ThrowableSpawnPoint.position, Quaternion.identity, swipeDirection);
        _objectViews.Add(obj);
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
        _objectViews.DisposeAndClear();
        base.Dispose();
    }
}