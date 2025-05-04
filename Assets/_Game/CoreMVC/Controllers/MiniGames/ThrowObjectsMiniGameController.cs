using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.ThrowObjects;
    
    IThrowObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IThrowObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly ThrowObjectsSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly ICameraProvider _cameraProvider;
    readonly ThrowObjectsMiniGameOptions _options;
    readonly UniqueCoroutine _throwDelayCoroutine;
    readonly List<ThrowableObjectView> _objectViews = new();

    bool _hasCompleted;
    float _throwTimer;

    public ThrowObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        ICameraProvider cameraProvider,
        ThrowObjectsMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as ThrowObjectsSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _cameraProvider = cameraProvider;
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

    void HandleSwipePerformed (Vector3 rawDirection)
    {
        if (_throwTimer > 0f)
            return;
        
        CreateAndThrowObject(rawDirection);
        _throwTimer = _options.ThrowDelay;
    }

    void CreateAndThrowObject (Vector3 rawDirection)
    {
        Vector3 adjustedDirection = (rawDirection * _options.DirectionWeight) / Screen.dpi;
        Vector3 forwardDirection = _cameraProvider.MainCamera.transform.forward * _options.ForwardWeight;
        Vector3 finalDirection = adjustedDirection + forwardDirection;
        
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