using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LongPressBombsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.LongPressBombs;

    ILongPressBombsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ILongPressBombsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly LongPressBombsSceneView _sceneView;
    readonly PoolableViewFactory _viewFactory;
    readonly LongPressBombsMiniGameOptions _options;
    readonly UniqueCoroutine _updateCoroutine;
    readonly List<LongPressableBombView> _objectViews = new();

    public LongPressBombsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        LongPressBombsMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as LongPressBombsSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;
        _updateCoroutine = new UniqueCoroutine(coroutineRunner);
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
        
        _viewFactory.SetupPool(_sceneView.BombPrefab);
        SpawnObjects();
        
        _updateCoroutine.Start(UpdateCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return _objectViews.All(x => x.Defused);
    }

    protected override void AddListeners ()
    {
        base.AddListeners();
        MiniGameModel.OnLongPressBegan += HandleLongPressBegan;
        MiniGameModel.OnLongPressCancelled += HandleLongPressCancelled;
        MiniGameModel.OnLongPressEnded += HandleLongPressEnded;
    }

    protected override void RemoveListeners ()
    {
        if (MiniGameModel == null)
            return;
        
        base.RemoveListeners();
        MiniGameModel.OnLongPressBegan -= HandleLongPressBegan;
        MiniGameModel.OnLongPressCancelled -= HandleLongPressCancelled;
        MiniGameModel.OnLongPressEnded -= HandleLongPressEnded;
    }
    
    void SpawnObjects ()
    {
        for (int i = 0; i < MiniGameModel.BaseObjectsToSpawn; i++)
        {
            LongPressableBombView obj = _viewFactory.GetView<LongPressableBombView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-_options.SpawnRange.x, -_options.SpawnRange.y),
                new Vector2(_options.SpawnRange.x, _options.SpawnRange.y)
            );
            
            float randomDelay = _randomProvider.Range(_options.DelayRange.x, _options.DelayRange.y);
            float randomTimer = _randomProvider.Range(_options.TimerRange.x, _options.TimerRange.y);
            float randomDefuse = _randomProvider.Range(_options.DefuseRange.x, _options.DefuseRange.y);
            obj.Setup(randomDelay, randomTimer, randomDefuse, _options.TimerGrace);

            AddViewListeners(obj);
            _objectViews.Add(obj);
        }
    }
    
    void HandleLongPressBegan (ILongPressable longPressable, Vector2 pressPosition)
    {
        LongPressableBombView obj = longPressable as LongPressableBombView;
        if (obj == null || !_objectViews.Contains(obj))
            throw new InvalidOperationException($"Long press action performed on invalid scene object.");
        
        obj.SetDefusingState(true);
    }
    
    void HandleLongPressCancelled (ILongPressable longPressable, Vector2 pressPosition)
    {
        LongPressableBombView obj = longPressable as LongPressableBombView;
        if (obj == null || !_objectViews.Contains(obj))
            throw new InvalidOperationException($"Long press action performed on invalid scene object.");
        
        obj.SetDefusingState(false);
    }
    
    void HandleLongPressEnded (ILongPressable longPressable, Vector2 pressPosition, float duration)
    {
        LongPressableBombView obj = longPressable as LongPressableBombView;
        if (obj == null || !_objectViews.Contains(obj))
            throw new InvalidOperationException($"Long press action performed on invalid scene object.");
        
        obj.SetDefusingState(false);
        
        //TODO pedro: maybe pass the complete call to within CheckWinCondition
        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    void AddViewListeners (LongPressableBombView view)
    {
        if (view == null)
            return;
        view.OnDefuseTimerReached += HandleDefuseTimerReached;
        view.OnTimerEnded += HandleTimerEnded;
    }
    
    void RemoveViewListeners (LongPressableBombView view)
    {
        if (view == null)
            return;
        view.OnDefuseTimerReached -= HandleDefuseTimerReached;
        view.OnTimerEnded -= HandleTimerEnded;
    }

    void HandleTimerEnded ()
    {
        MiniGameModel.ForceFailure();
    }

    void HandleDefuseTimerReached ()
    {
        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    IEnumerator UpdateCoroutine ()
    {
        while (true)
        {
            if (!IsActive)
            {
                yield return null;
                continue;
            }

            foreach (LongPressableBombView obj in _objectViews)
                obj.UpdateBomb();
            yield return null;
        }
    }

    public override void Dispose ()
    {
        foreach (LongPressableBombView obj in _objectViews)
            RemoveViewListeners(obj);
        
        _objectViews.DisposeAndClear();
        _updateCoroutine.Dispose();
        base.Dispose();
    }
}