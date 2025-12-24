using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragSortMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.DragSort;
    
    IDragSortMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IDragSortMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly DragSortSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly PoolableViewFactory _viewFactory;
    readonly DragSortMiniGameOptions _options;
    readonly List<DraggableObjectView> _objectViews = new();
    readonly Dictionary<DraggableObjectColor, int> _colorCounts = new();

    public DragSortMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        DragSortMiniGameOptions options
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as DragSortSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;
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
        SetupContainers();
        SpawnObjects();
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

    void SetupContainers ()
    {
        HashSet<Transform> points = _sceneView.ContainerSpawnPoints.ToHashSet();

        foreach (DraggableContainerView container in _sceneView.Containers)
        {
            Transform randomPoint = _randomProvider.PickRandom(points);
            points.Remove(randomPoint);
            float randomX = _randomProvider.Range(
                -_options.ContainerHorizontalDistance,
                _options.ContainerHorizontalDistance
            );
            Vector3 newPosition = randomPoint.transform.position + new Vector3(randomX, 0f, 0f);
            container.transform.position = newPosition;
        }
    }

    void SpawnObjects ()
    {
        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            DraggableObjectView obj = _viewFactory.GetView<DraggableObjectView>(_sceneView.transform);
            obj.Setup(_randomProvider.RandomEnumValue<DraggableObjectColor>());
            obj.transform.position = _sceneView.BallSpawnPoint.transform.position + _randomProvider.Range(
                new Vector3(-_options.BallSpawnDistance, 0),
                new Vector3(_options.BallSpawnDistance, 0)
            );
            
            _objectViews.Add(obj);
            _colorCounts.TryAdd(obj.Color, 0);
            _colorCounts[obj.Color]++;
        }
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
        //TODO pedro: check for physics differences between editor and build
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