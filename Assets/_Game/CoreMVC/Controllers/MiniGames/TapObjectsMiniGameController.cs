using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class TapObjectsMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.TapObjects;

    ITapObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ITapObjectsMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly TapObjectsSceneView _sceneView;

    HashSet<IPressable> _spawnedObjects = new();

    public TapObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as TapObjectsSceneView;
        _randomProvider = randomProvider;
    }

    public override void Initialize ()
    {
        //TODO pedro: remove null condition
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();

        for (int i = 0; i < MiniGameModel.BaseObjectsToSpawn; i++)
        {
            TappableObjectView obj = Object.Instantiate(_sceneView.TappableObjectPrefab, _sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-MiniGameModel.MaxSpawnDistance, -MiniGameModel.MaxSpawnDistance),
                new Vector2(MiniGameModel.MaxSpawnDistance, MiniGameModel.MaxSpawnDistance)
            );
            _spawnedObjects.Add(obj);
        }
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        if (timerEnded)
        {
            //TODO pedro: implement ui
            string message = _spawnedObjects.Count == 0 ? "YOU WIN THE GAME" : "YOU LOSE THE GAME";
            DebugUtils.Log(message);
        }
        return _spawnedObjects.Count == 0;
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
        if (!_spawnedObjects.Contains(pressable))
            throw new InvalidOperationException($"Tap performed on invalid scene object.");
        
        _spawnedObjects.Remove(pressable);
        TappableObjectView obj = pressable as TappableObjectView;
        Object.Destroy(obj.gameObject);

        if (CheckWinCondition(false))
            MiniGameModel.Complete();
    }

    public override void Dispose ()
    {
        base.Dispose();
        _spawnedObjects.Clear();
    }
}