using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwipeThrowMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.SwipeThrow;
    
    ISwipeThrowMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ISwipeThrowMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly SwipeThrowSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly SwipeThrowMiniGameOptions _options;
    readonly UniqueCoroutine _throwDelayCoroutine;
    readonly List<ThrowableObjectView> _objectViews = new();

    bool _hasCompleted;
    float _throwTimer;

    public SwipeThrowMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        SwipeThrowMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as SwipeThrowSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;

        _throwDelayCoroutine = new UniqueCoroutine(coroutineRunner);
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
            _randomProvider.PickRandom(_sceneView.ContainerSpawnPoints).position;
        
        _throwDelayCoroutine.Start(ThrowDelayCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        //TODO pedro: avoid double method call when the game completes
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

    void HandleSwipePerformed (Vector3 rawDirection, Vector3 forwardDirection)
    {
        if (_throwTimer > 0f)
            return;
        
        CreateAndThrowObject(rawDirection, forwardDirection);
        _throwTimer = _options.ThrowDelay;
    }

    void CreateAndThrowObject (Vector3 rawDirection, Vector3 forwardDirection)
    {
        Vector3 adjustedRawDirection = (rawDirection * _options.DirectionWeight) / Screen.dpi;
        Vector3 adjustedForwardDirection = forwardDirection * _options.ForwardWeight;
        Vector3 finalDirection = adjustedForwardDirection + adjustedRawDirection;
        
        ThrowableObjectView obj = _viewFactory.GetView<ThrowableObjectView>(_sceneView.transform);
        obj.Setup(_sceneView.ThrowableSpawnPoint.position, Quaternion.identity, finalDirection);
        _objectViews.Add(obj);
    }

    void HandleThrowableEnter ()
    {
        _hasCompleted = true;
        
        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    IEnumerator ThrowDelayCoroutine ()
    {
        while (true)
        {
            while (_throwTimer > 0f)
            {
                _throwTimer -= Time.deltaTime;
                yield return null;
            }

            _throwTimer = 0f;
            yield return null;
        }
    }

    public override void Dispose ()
    {
        RemoveViewListeners();
        _objectViews.DisposeAndClear();
        _throwDelayCoroutine.Dispose();
        base.Dispose();
    }
}