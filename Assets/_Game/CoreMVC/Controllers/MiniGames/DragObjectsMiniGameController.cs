using System.Collections.Generic;
using UnityEngine;

public class DragObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.DragObjects;
    
    IDragObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IDragObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly DragObjectsSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly PoolableViewFactory _viewFactory;
    readonly List<DraggableObjectView> _objectViews = new();
    readonly Dictionary<DraggableObjectColor, int> _colorCounts = new();

    public DragObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as DragObjectsSceneView;
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

    protected override void SetupMiniGame ()
    {
        base.SetupMiniGame();
        
        _viewFactory.SetupPool(_sceneView.DraggablePrefab);
        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            // DraggableObjectView coloredObj =
            //     _sceneView.DraggablePrefabs[_randomProvider.Range(0, _sceneView.DraggablePrefabs.Length)];
            DraggableObjectView obj = _viewFactory.GetView<DraggableObjectView>(_sceneView.transform);
            obj.Setup(_randomProvider.RandomEnumValue<DraggableObjectColor>());
            obj.transform.position = _sceneView.SpawnPoint.transform.position + _randomProvider.Range(
                new Vector3(-15, 0),
                new Vector3(15, 0)
            );
            
            _objectViews.Add(obj);
            _colorCounts.TryAdd(obj.Color, 0);
            _colorCounts[obj.Color]++;
        }
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        foreach (DraggableContainerView container in _sceneView.Containers)
        {
            if (!_colorCounts.TryGetValue(container.Color, out int expectedCount))
                continue;
            if (container.ValidObjectsCount == expectedCount)
                continue;
            return false;
        }
        
        return true;
    }

    void AddViewListeners ()
    {
        foreach (DraggableContainerView container in _sceneView.Containers)
        {
            container.OnDraggableEnter += HandleDraggableEnter;
            container.OnDraggableExit += HandleDraggableExit;
        }
    }

    void RemoveViewListeners ()
    {
        if (_sceneView == null)
            return;
        
        foreach (DraggableContainerView container in _sceneView.Containers)
        {
            container.OnDraggableEnter -= HandleDraggableEnter;
            container.OnDraggableExit -= HandleDraggableExit;
        }
    }

    void HandleDraggableEnter (IDraggable draggable, DraggableContainerView container)
    {
        DraggableObjectView obj = draggable as DraggableObjectView;
        if (obj.Color != container.Color)
            return;
        container.ValidObjectsCount++;

        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    void HandleDraggableExit (IDraggable draggable, DraggableContainerView container)
    {
        DraggableObjectView obj = draggable as DraggableObjectView;
        if (obj.Color != container.Color)
            return;
        container.ValidObjectsCount--;
    }

    public override void Dispose ()
    {
        RemoveViewListeners();
        _objectViews.DisposeAndClear();
        base.Dispose();
    }
}