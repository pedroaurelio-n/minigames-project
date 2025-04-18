using VContainer;
using VContainer.Unity;

public class SceneUIViewInstaller : IInstaller
{
    readonly SceneUIView _sceneUIView;

    public SceneUIViewInstaller (
        SceneUIView sceneUIView
    )
    {
        _sceneUIView = sceneUIView;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_sceneUIView);
        
        builder.RegisterInstance(_sceneUIView.MiniGameLabelUIView);
        builder.RegisterInstance(_sceneUIView.MiniGameTimerUIView);
    }
}