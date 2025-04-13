using System.Collections.Generic;
using UnityEngine;

public class DragObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.DragObjects;
    
    IDragObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IDragObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly DragObjectsSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly Dictionary<DraggableObjectColor, int> _coloredObjects = new();

    public DragObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as DragObjectsSceneView;
        _randomProvider = randomProvider;
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
        
        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            DraggableObjectView coloredObj =
                _sceneView.DraggablePrefabs[_randomProvider.Range(0, _sceneView.DraggablePrefabs.Length)];
            DraggableObjectView obj = Object.Instantiate(coloredObj, _sceneView.transform);
            obj.transform.position = _sceneView.SpawnPoint.transform.position + _randomProvider.Range(
                new Vector3(-15, 0),
                new Vector3(15, 0)
            );
            
            _coloredObjects.TryAdd(obj.Color, 0);
            _coloredObjects[obj.Color]++;
        }
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        foreach (DraggableContainerView container in _sceneView.Containers)
        {
            if (_coloredObjects.TryGetValue(container.Color, out int expectedCount))
            {
                if (container.ValidObjectsCount != expectedCount)
                {
                    if (timerEnded)
                    {
                        //TODO pedro: implement ui
                        string message = "YOU LOSE THE GAME";
                        DebugUtils.Log(message);
                    }
                    return false;
                }
            }
        }
        
        if (timerEnded)
        {
            //TODO pedro: implement ui
            string message =  "YOU WIN THE GAME";
            DebugUtils.Log(message);
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
        base.Dispose();
    }
}