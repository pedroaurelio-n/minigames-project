using System;
using System.Collections.Generic;
using UnityEngine;

public class TapObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.TapObjects;

    ITapObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ITapObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly TapObjectsSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly HashSet<TappableObjectView> _objectViews = new();

    public TapObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as TapObjectsSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
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
        for (int i = 0; i < MiniGameModel.BaseObjectsToSpawn; i++)
        {
            TappableObjectView obj = _viewFactory.GetView<TappableObjectView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-MiniGameModel.MaxSpawnDistance, -MiniGameModel.MaxSpawnDistance),
                new Vector2(MiniGameModel.MaxSpawnDistance, MiniGameModel.MaxSpawnDistance)
            );
            _objectViews.Add(obj);
        }
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        if (timerEnded)
        {
            //TODO pedro: implement ui
            string message = _objectViews.Count == 0 ? "YOU WIN THE GAME" : "YOU LOSE THE GAME";
            DebugUtils.Log(message);
        }
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

    void HandleTapPerformed (IPressable pressable, Vector2 tapPosition)
    {
        if (!_objectViews.Contains(pressable as TappableObjectView))
            throw new InvalidOperationException($"Tap performed on invalid scene object.");
        
        _objectViews.Remove(pressable as TappableObjectView);
        TappableObjectView obj = pressable as TappableObjectView;
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