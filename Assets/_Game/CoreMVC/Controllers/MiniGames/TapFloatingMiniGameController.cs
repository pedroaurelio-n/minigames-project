using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapFloatingMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.TapFloating;

    ITapFloatingMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ITapFloatingMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly TapFloatingSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly TapFloatingMiniGameOptions _options;
    readonly UniqueCoroutine _moveCoroutine;
    readonly HashSet<FloatingObjectView> _targetObjectViews = new();
    readonly HashSet<FloatingObjectView> _fillerObjectViews = new();

    public TapFloatingMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        TapFloatingMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as TapFloatingSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;
        _moveCoroutine = new UniqueCoroutine(coroutineRunner);
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
        
        _viewFactory.SetupPool(_sceneView.FloatingObjectView);
        SpawnTargetObjects();
        SpawnFillerObjects();
        
        _moveCoroutine.Start(MoveCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return _targetObjectViews.Count == 0;
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
    
    void SpawnTargetObjects ()
    {
        for (int i = 0; i < MiniGameModel.BaseTargetsToSpawn; i++)
        {
            FloatingObjectView obj = _viewFactory.GetView<FloatingObjectView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-_options.XSpawnDistance, _options.YDistanceRange.x),
                new Vector2(_options.XSpawnDistance, _options.YDistanceRange.x)
            );

            float randomSpeed = _randomProvider.Range(_options.SpeedRange.x, _options.SpeedRange.y);
            float randomDelay = _randomProvider.Range(_options.DelayRange.x, _options.DelayRange.y);
            obj.Setup(true, randomSpeed, randomDelay);
            _targetObjectViews.Add(obj);
        }
    }
    
    void SpawnFillerObjects ()
    {
        for (int i = 0; i < MiniGameModel.BaseObjectsToSpawn; i++)
        {
            FloatingObjectView obj = _viewFactory.GetView<FloatingObjectView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-_options.XSpawnDistance, _options.YDistanceRange.x),
                new Vector2(_options.XSpawnDistance, _options.YDistanceRange.x)
            );
            
            float randomSpeed = _randomProvider.Range(_options.SpeedRange.x, _options.SpeedRange.y);
            float randomDelay = _randomProvider.Range(_options.DelayRange.x, _options.DelayRange.y);
            obj.Setup(false, randomSpeed, randomDelay);
            _fillerObjectViews.Add(obj);
        }
    }

    void HandleTapPerformed (IPressable pressable, Vector2 tapPosition)
    {
        FloatingObjectView obj = pressable as FloatingObjectView;
        if (obj == null || (!_targetObjectViews.Contains(obj) && !_fillerObjectViews.Contains(obj)))
            throw new InvalidOperationException($"Tap performed on invalid scene object.");
        
        if (!obj.IsObjective)
            MiniGameModel.ForceFailure();
        
        _targetObjectViews.Remove(obj);
        _viewFactory.ReleaseView(obj);

        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    IEnumerator MoveCoroutine ()
    {
        while (true)
        {
            if (!IsActive)
            {
                yield return null;
                continue;
            }
            
            foreach (FloatingObjectView obj in _targetObjectViews)
                obj.MoveUpwards();
            
            foreach (FloatingObjectView obj in _fillerObjectViews)
                obj.MoveUpwards();

            yield return null;
        }
    }

    public override void Dispose ()
    {
        _moveCoroutine.Dispose();
        
        _targetObjectViews.DisposeAndClear();
        _fillerObjectViews.DisposeAndClear();
        
        base.Dispose();
    }
}