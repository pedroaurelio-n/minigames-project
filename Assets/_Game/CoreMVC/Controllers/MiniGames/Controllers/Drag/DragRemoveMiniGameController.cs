using System.Collections.Generic;
using UnityEngine;

public class DragRemoveMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.DragRemove;
    
    IDragRemoveMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IDragRemoveMiniGameModel;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly DragRemoveSceneView _sceneView;
    readonly IRandomProvider _randomProvider;
    readonly PoolableViewFactory _viewFactory;
    readonly DragRemoveMiniGameOptions _options;
    readonly List<DraggableClutterObjectView> _clutterViews = new();
    
    DraggableObjectiveView _objectiveView;

    public DragRemoveMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        IRandomProvider randomProvider,
        PoolableViewFactory viewFactory,
        DragRemoveMiniGameOptions options
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _sceneView = sceneView as DragRemoveSceneView;
        _randomProvider = randomProvider;
        _viewFactory = viewFactory;
        _options = options;
    }
    
    public override void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }

    protected override void SetupMiniGame ()
    {
        base.SetupMiniGame();
        
        _viewFactory.SetupPool(_sceneView.ClutterPrefabs);
        _viewFactory.SetupPool(_sceneView.ObjectivePrefab);
        SpawnObjects();
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return true;
    }

    void SpawnObjects ()
    {
        _objectiveView = _viewFactory.GetView<DraggableObjectiveView>(_sceneView.transform);
        _objectiveView.Setup(new Vector3(0, 0, 1), Quaternion.identity);
        _objectiveView.OnObjectiveDragBegan += HandleObjectiveDragBegan;
        
        for (int i = 0; i < MiniGameModel.BaseStartObjects; i++)
        {
            DraggableClutterObjectView obj = _viewFactory.GetView<DraggableClutterObjectView>(_sceneView.transform);
            Vector3 clutterPos = new(
                _randomProvider.Range(-_options.SpawnRange.x, _options.SpawnRange.x),
                _randomProvider.Range(-_options.SpawnRange.y, _options.SpawnRange.y)
            );
            Quaternion clutterRot = Quaternion.Euler(0, 0, _randomProvider.Range(0, 360f));
            obj.Setup(i, clutterPos, clutterRot);
            
            _clutterViews.Add(obj);
        }
    }

    void HandleObjectiveDragBegan ()
    {
        MiniGameModel.Complete();
    }

    public override void Dispose ()
    {
        if (_objectiveView != null)
        {
            _objectiveView.OnObjectiveDragBegan -= HandleObjectiveDragBegan;
            _objectiveView.Despawn();
            _objectiveView = null;
        }
        
        _clutterViews.DisposeAndClear();
        base.Dispose();
    }
}