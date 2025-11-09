using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO pedro: maybe put some of this logic on the model?
public class JoystickAimMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.JoystickAim;
    
    IJoystickAimMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IJoystickAimMiniGameModel;
    JoystickAimMiniGameUIController MiniGameUIController => UIController as JoystickAimMiniGameUIController;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly JoystickAimSceneView _sceneView;
    readonly JoystickAimMiniGameOptions _options;
    readonly IRandomProvider _randomProvider;
    readonly PoolableViewFactory _viewFactory;
    readonly UniqueCoroutine _updateCoroutine;
    readonly HashSet<JoystickAimTargetView> _targetViews = new();
    readonly HashSet<JoystickAimProjectileView> _projectileViews = new();

    float _shootTimer;

    public JoystickAimMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        JoystickAimMiniGameOptions options,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as JoystickAimSceneView;
        _options = options;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
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
        UIController = new JoystickAimMiniGameUIController();
        base.SetupMiniGame();
        
        AddUIListeners();

        _viewFactory.SetupPool(_sceneView.TargetPrefab);
        _viewFactory.SetupPool(_sceneView.ProjectilePrefab);
        SpawnTargets();
        
        _updateCoroutine.Start(UpdateCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return _targetViews.Count == 0;
    }

    void SpawnTargets ()
    {
        for (int i = 0; i < MiniGameModel.BaseObjectsToSpawn; i++)
        {
            JoystickAimTargetView obj = _viewFactory.GetView<JoystickAimTargetView>(_sceneView.transform);
            obj.transform.position = _randomProvider.Range(
                new Vector3(-_options.SpawnRange.x, 0f, -_options.SpawnRange.y),
                new Vector3(_options.SpawnRange.x, 0f, _options.SpawnRange.y)
            );
            obj.OnTargetHit += HandleTargetHit;
            _targetViews.Add(obj);
        }
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
    }

    void HandleTargetHit (JoystickAimTargetView target, JoystickAimProjectileView projectile)
    {
        target.OnTargetHit -= HandleTargetHit;
        
        _targetViews.Remove(target);
        _viewFactory.ReleaseView(target);
        
        _projectileViews.Remove(projectile);
        _viewFactory.ReleaseView(projectile);
        
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
            
            _shootTimer += Time.deltaTime;
            if (_shootTimer >= _options.ShootDelay)
            {
                Shoot();
                _shootTimer = 0f;
            }
            yield return null;
        }
    }

    void Shoot ()
    {
        JoystickAimProjectileView projectile = _viewFactory.GetView<JoystickAimProjectileView>(_sceneView.transform);
        
        projectile.transform.position = _sceneView.ProjectileSpawnPoint.position;
        Vector3 forwardDirection = _sceneView.ProjectileSpawnPoint.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();
        
        _projectileViews.Add(projectile);
        projectile.Setup(new Vector3(forwardDirection.x, 0f, forwardDirection.z) * _options.ShootSpeed);
    }

    public override void Dispose ()
    {
        foreach (JoystickAimTargetView target in _targetViews)
            target.OnTargetHit -= HandleTargetHit;
        
        _targetViews.DisposeAndClear();
        _projectileViews.DisposeAndClear();
        
        _updateCoroutine.Dispose();
        
        if (UIController != null)
            RemoveUIListeners();
        
        base.Dispose();
    }
}