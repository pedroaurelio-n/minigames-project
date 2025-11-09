using System.Collections;
using UnityEngine;

//TODO pedro: maybe put some of this logic on the model?
public class JoystickRotateMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.JoystickRotate;
    
    IJoystickRotateMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IJoystickRotateMiniGameModel;
    JoystickRotateMiniGameUIController MiniGameUIController => UIController as JoystickRotateMiniGameUIController;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly JoystickRotateSceneView _sceneView;
    readonly JoystickRotateMiniGameOptions _options;
    readonly IRandomProvider _randomProvider;
    readonly UniqueCoroutine _updateCoroutine;

    float _targetAngleY;

    public JoystickRotateMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        JoystickRotateMiniGameOptions options,
        IRandomProvider randomProvider,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as JoystickRotateSceneView;
        _options = options;
        _randomProvider = randomProvider;
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
        UIController = new JoystickRotateMiniGameUIController();
        base.SetupMiniGame();
        
        AddUIListeners();

        _targetAngleY = _randomProvider.Range(30f, 330f);
        _sceneView.Target.rotation = Quaternion.Euler(0, _targetAngleY, 0);
        
        _updateCoroutine.Start(UpdateCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        float currentY = _sceneView.RotatingObject.eulerAngles.y;
        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currentY, _targetAngleY));
        return angleDiff <= _options.WinAngleTolerance;
    }

    void AddUIListeners ()
    {
        MiniGameUIController.OnJoystickDirectionUpdated += HandleJoystickDirectionUpdated;
    }
    
    void RemoveUIListeners ()
    {
        MiniGameUIController.OnJoystickDirectionUpdated -= HandleJoystickDirectionUpdated;
    }

    void HandleJoystickDirectionUpdated (Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
            return;
        
        float targetInputAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        float currentY = _sceneView.RotatingObject.eulerAngles.y;
        
        float angleDiff = Mathf.DeltaAngle(currentY, targetInputAngle);
        float step = _options.RotationSpeed * Time.deltaTime;

        float newY = currentY + Mathf.Clamp(angleDiff, -step, step);

        Quaternion newRotation = Quaternion.Euler(0, newY, 0);
        _sceneView.RotatingObject.rotation = newRotation;
        
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
            
            MiniGameUIController.UIView.UpdateJoystick();
            yield return null;
        }
    }

    public override void Dispose ()
    {
        _updateCoroutine.Dispose();
        
        if (UIController != null)
            RemoveUIListeners();
        
        base.Dispose();
    }
}