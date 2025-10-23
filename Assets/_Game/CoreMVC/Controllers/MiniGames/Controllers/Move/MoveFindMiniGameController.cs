using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFindMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.MoveFind;
    
    IMoveFindMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IMoveFindMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly MoveFindSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly PoolableViewFactory _viewFactory;
    readonly ICameraProvider _cameraProvider;
    readonly UniqueCoroutine _checkForObjectRoutine;
    readonly MoveFindMiniGameOptions _options;
    readonly WaitForSeconds _waitForCheck;
    readonly List<FindableObjectView> _objectViews = new();

    FindableObjectView _targetObject;
    bool _isObjectVisible;
    
    public MoveFindMiniGameController(
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        ICameraProvider cameraProvider,
        MoveFindMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _randomProvider = randomProvider;
        _sceneView = sceneView as MoveFindSceneView;
        _viewFactory = viewFactory;
        _cameraProvider = cameraProvider;
        _options = options;

        _checkForObjectRoutine = new UniqueCoroutine(coroutineRunner);
        _waitForCheck = new WaitForSeconds(_options.CheckDelay);
    }
    
    public override void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }
    
    protected override void SetupMiniGame()
    {
        base.SetupMiniGame();
        
        _viewFactory.SetupPool(_sceneView.FindableObjectPrefab);
        SpawnFindableObject();
        SpawnFillerObjects();
        _checkForObjectRoutine.Start(CheckForObjectRoutine());
    }

    protected override bool CheckWinCondition(bool timerEnded)
    {
        if (timerEnded)
            _checkForObjectRoutine.Stop();
        return _isObjectVisible;
    }

    void SpawnFindableObject ()
    {
        Vector3 randomDirection = _randomProvider.InsideSphere;
        float distance = _randomProvider.Range(_options.DistanceRange.x, _options.DistanceRange.y);
        
        _targetObject = _viewFactory.GetView<FindableObjectView>(_sceneView.transform);
        _targetObject.Setup(true);
        _targetObject.transform.position =
            _randomProvider.PickRandom(_sceneView.PossiblePoints).position
            + randomDirection.normalized * distance;
        _objectViews.Add(_targetObject);
    }

    void SpawnFillerObjects ()
    {
        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            Vector3 randomDirection = _randomProvider.InsideSphere;
            float distance = _randomProvider.Range(_options.DistanceRange.x, _options.DistanceRange.y);
            
            FindableObjectView obj = _viewFactory.GetView<FindableObjectView>(_sceneView.transform);
            obj.Setup(false);
            obj.transform.position =
                _randomProvider.PickRandom(_sceneView.AllPoints).position
                + randomDirection * distance;
            _objectViews.Add(obj);
        }
    }

    IEnumerator CheckForObjectRoutine()
    {
        while (true)
        {
            _isObjectVisible = _cameraProvider.IsContainedInCameraBounds(_targetObject.Renderer);

            if (CheckWinCondition(false))
            {
                MiniGameModel.Complete();
                yield break;
            }
            yield return _waitForCheck;
        }
    }

    public override void Dispose()
    {
        _checkForObjectRoutine?.Dispose();
        _objectViews.DisposeAndClear();
        base.Dispose();
    }
}