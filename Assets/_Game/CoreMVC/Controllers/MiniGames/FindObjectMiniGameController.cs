using System.Collections;
using UnityEngine;

public class FindObjectMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.FindObject;
    
    IFindObjectMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IFindObjectMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly FindObjectSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly UniqueCoroutine checkForObjectRoutine;

    FindableObjectView _objectView;
    bool _isObjectVisible;
    
    public FindObjectMiniGameController(
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        IRandomProvider randomProvider,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _randomProvider = randomProvider;
        _sceneView = sceneView as FindObjectSceneView;

        checkForObjectRoutine = new UniqueCoroutine(coroutineRunner);
    }
    
    public override void Initialize ()
    {
        //TODO pedro: remove null condition
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }
    
    protected override void SetupMiniGame()
    {
        Vector3 randomDirection = _randomProvider.InsideSphere;
        float distance = _randomProvider.Range(0f, 15f);
        
        _objectView = Object.Instantiate(_sceneView.FindableObjectPrefab);
        _objectView.SetMaterial(true);
        _objectView.transform.position =
            _sceneView.PossiblePoints[_randomProvider.Range(0, _sceneView.PossiblePoints.Length)].position
            + randomDirection * distance;

        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            FindableObjectView obj = Object.Instantiate(_sceneView.FindableObjectPrefab);
            obj.SetMaterial(false);
            obj.transform.position =
                _sceneView.AllPoints[_randomProvider.Range(0, _sceneView.AllPoints.Length)].position
                + randomDirection * distance;
        }
        
        checkForObjectRoutine.Start(CheckForObjectRoutine());
    }

    protected override bool CheckWinCondition(bool timerEnded)
    {
        //TODO pedro: implement zooming to fill screen with object
        if (timerEnded)
            checkForObjectRoutine.Stop();
        if (timerEnded || _isObjectVisible)
        {
            //TODO pedro: implement game ended UI
            string message = _isObjectVisible ? "YOU WIN THE GAME" : "YOU LOSE THE GAME";
            DebugUtils.Log(message);
        }
        return _isObjectVisible;
    }

    IEnumerator CheckForObjectRoutine()
    {
        while (true)
        {
            _isObjectVisible = _objectView.Renderer.isVisible;

            if (CheckWinCondition(false))
            {
                //TODO pedro: block timer model from ending during this wait
                yield return new WaitForSeconds(1f);
                MiniGameModel.Complete();
                yield break;
            }
            yield return null;
        }
    }

    public override void Dispose()
    {
        checkForObjectRoutine?.Dispose();
        base.Dispose();
    }
}