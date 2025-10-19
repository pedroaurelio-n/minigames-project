using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMovingMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.TapMoving;

    ITapMovingMinigameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ITapMovingMinigameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly TapMovingSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly TapMovingMiniGameOptions _options;
    readonly UniqueCoroutine _activationCoroutine;
    readonly HashSet<TappableMovingObjectView> _objectViews = new();

    public TapMovingMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        TapMovingMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as TapMovingSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;
        _activationCoroutine = new UniqueCoroutine(coroutineRunner);
    }

    public override void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }

    protected override void SetupMiniGame ()
    {
        base.SetupMiniGame();
        
        _viewFactory.SetupPool(_sceneView.TappableMovingObjectPrefab);
        SpawnObjects();
        _activationCoroutine.Start(ActivationCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return _objectViews.Count == 0;
    }

    protected override void AddListeners ()
    {
        base.AddListeners();
        MiniGameModel.OnTapPerformed += HandleTapPerformed;
    }

    protected override void RemoveListeners ()
    {
        if (MiniGameModel == null)
            return;
        
        base.RemoveListeners();
        MiniGameModel.OnTapPerformed -= HandleTapPerformed;
    }
    
    void SpawnObjects ()
    {
        for (int i = 0; i < MiniGameModel.BaseObjectsToSpawn; i++)
        {
            TappableMovingObjectView obj = _viewFactory.GetView<TappableMovingObjectView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-_options.XSpawnDistance, -_options.YSpawnDistance),
                new Vector2(_options.XSpawnDistance, _options.YSpawnDistance)
            );

            Vector2 randomDirection = _randomProvider.InsideCircle *
                                      _randomProvider.Range(_options.SpeedRange.x, _options.SpeedRange.y);
            float randomDelay = _randomProvider.Range(_options.DelayRange.x, _options.DelayRange.y);
            obj.Setup(randomDirection, randomDelay);
            
            _objectViews.Add(obj);
        }
    }

    void HandleTapPerformed (ITappable tappable, Vector2 tapPosition)
    {
        TappableMovingObjectView obj = tappable as TappableMovingObjectView;
        if (obj == null || !_objectViews.Contains(obj))
            throw new InvalidOperationException($"Tap performed on invalid scene object.");
        
        _objectViews.Remove(obj);
        _viewFactory.ReleaseView(obj);

        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }
    
    IEnumerator ActivationCoroutine ()
    {
        while (true)
        {
            if (!IsActive)
            {
                yield return null;
                continue;
            }
            
            foreach (TappableMovingObjectView obj in _objectViews)
                obj.EvaluateActivation();

            yield return null;
        }
    }

    public override void Dispose ()
    {
        _activationCoroutine.Dispose();
        _objectViews.DisposeAndClear();
        base.Dispose();
    }
}