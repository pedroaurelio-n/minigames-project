using VContainer;
using VContainer.Unity;

public class SceneViewInstaller : IInstaller
{
    readonly SceneView _sceneView;
    readonly PoolableViewFactory _poolableViewFactory;
    
    public SceneViewInstaller (
        SceneView sceneView,
        PoolableViewFactory poolableViewFactory
    )
    {
        _sceneView = sceneView;
        _poolableViewFactory = poolableViewFactory;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_sceneView);
        //TODO pedro: delete mouse input classes
        // builder.RegisterInstance(_sceneView.MouseInput);
        builder.RegisterInstance(_sceneView.TapInputView);
        builder.RegisterInstance(_sceneView.SwipeInputView);
        builder.RegisterInstance(_sceneView.LongPressInputView);
        builder.RegisterInstance(_sceneView.TwoPointMoveInputView);
        builder.RegisterInstance(_sceneView.TwoPointZoomInputView);
        builder.RegisterInstance(_sceneView.TouchDragInputView);
        builder.RegisterInstance(_poolableViewFactory);
    }
}