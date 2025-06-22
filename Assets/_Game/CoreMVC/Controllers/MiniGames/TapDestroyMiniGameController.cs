using System;
using System.Collections.Generic;
using UnityEngine;

public class TapDestroyMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.TapDestroy;

    ITapDestroyMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ITapDestroyMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly TapDestroySceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly TapDestroyMiniGameOptions _options;
    readonly HashSet<TappableObjectView> _objectViews = new();

    public TapDestroyMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        TapDestroyMiniGameOptions options
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as TapDestroySceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;
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
        
        _viewFactory.SetupPool(_sceneView.TappableObjectPrefab);
        SpawnObjects();
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
            TappableObjectView obj = _viewFactory.GetView<TappableObjectView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-_options.SpawnDistance.x, -_options.SpawnDistance.y),
                new Vector2(_options.SpawnDistance.x, _options.SpawnDistance.y)
            );
            _objectViews.Add(obj);
        }
    }

    void HandleTapPerformed (IPressable pressable, Vector2 tapPosition)
    {
        TappableObjectView obj = pressable as TappableObjectView;
        if (obj == null || !_objectViews.Contains(obj))
            throw new InvalidOperationException($"Tap performed on invalid scene object.");
        
        _objectViews.Remove(obj);
        _viewFactory.ReleaseView(obj);

        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    public override void Dispose ()
    {
        _objectViews.DisposeAndClear();
        base.Dispose();
    }
}