using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjectMiniGameController : BaseMiniGameController
{
    const float CHECK_DELAY = 0.2f;
    
    protected override MiniGameType MiniGameType => MiniGameType.FindObject;
    
    IFindObjectMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IFindObjectMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly FindObjectSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly PoolableViewFactory _viewFactory;
    readonly ICameraProvider _cameraProvider;
    readonly UniqueCoroutine _checkForObjectRoutine;
    readonly List<FindableObjectView> _objectViews = new();
    readonly WaitForSeconds _waitForCheck;

    FindableObjectView _targetObject;
    bool _isObjectVisible;
    
    public FindObjectMiniGameController(
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        ICameraProvider cameraProvider,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _randomProvider = randomProvider;
        _sceneView = sceneView as FindObjectSceneView;
        _viewFactory = viewFactory;
        _cameraProvider = cameraProvider;

        _checkForObjectRoutine = new UniqueCoroutine(coroutineRunner);
        _waitForCheck = new WaitForSeconds(CHECK_DELAY);
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
        Vector3 randomDirection = _randomProvider.InsideSphere;
        float distance = _randomProvider.Range(3f, 15f);
        
        _targetObject = _viewFactory.GetView<FindableObjectView>(_sceneView.transform);
        _targetObject.Setup(true);
        _targetObject.transform.position =
            _randomProvider.PickRandom(_sceneView.PossiblePoints).position
            + randomDirection.normalized * distance;
        _objectViews.Add(_targetObject);

        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            FindableObjectView obj = _viewFactory.GetView<FindableObjectView>(_sceneView.transform);
            obj.Setup(false);
            obj.transform.position =
                _randomProvider.PickRandom(_sceneView.AllPoints).position
                + randomDirection * distance;
            _objectViews.Add(obj);
        }
        
        _checkForObjectRoutine.Start(CheckForObjectRoutine());
    }

    protected override bool CheckWinCondition(bool timerEnded)
    {
        //TODO pedro: implement zooming to fill screen with object
        if (timerEnded)
            _checkForObjectRoutine.Stop();
        return _isObjectVisible;
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