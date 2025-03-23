using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class TapObjectsMiniGameController : BaseMiniGameController
{
    const int OBJECTS_TO_SPAWN = 5;
    const int MAX_DISTANCE = 20;

    protected override MiniGameType MiniGameType => MiniGameType.TapObjects;

    ITapObjectsMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as ITapObjectsMiniGameModel;

    readonly IPressModel _pressModel;
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly IRandomProvider _randomProvider;
    readonly SceneView _sceneView;

    HashSet<IPressable> _spawnedObjects = new();

    public TapObjectsMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IPressModel pressModel,
        IRandomProvider randomProvider
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _pressModel = pressModel;
        _sceneView = sceneView;
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

        for (int i = 0; i < OBJECTS_TO_SPAWN; i++)
        {
            TappableObjectView obj = Object.Instantiate(_sceneView.TappableObjectPrefab, _sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector2(-MAX_DISTANCE, -MAX_DISTANCE),
                new Vector2(MAX_DISTANCE, MAX_DISTANCE)
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
        _pressModel.OnTapPerformed += HandleTapPerformed;
    }

    protected override void RemoveListeners ()
    {
        base.RemoveListeners();
        _pressModel.OnTapPerformed -= HandleTapPerformed;
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
}