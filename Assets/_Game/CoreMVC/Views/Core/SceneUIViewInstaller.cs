using VContainer;
using VContainer.Unity;

public class SceneUIViewInstaller : IInstaller
{
    readonly SceneUIView _sceneUIView;
    readonly FadeToBlackManager _fadeToBlackManager;

    public SceneUIViewInstaller (
        SceneUIView sceneUIView,
        FadeToBlackManager fadeToBlackManager
    )
    {
        _sceneUIView = sceneUIView;
        _fadeToBlackManager = fadeToBlackManager;
    }
    
    public void Install (IContainerBuilder builder)
    {
        builder.RegisterInstance(_sceneUIView);
        builder.RegisterInstance(_fadeToBlackManager);
        
        builder.RegisterInstance(_sceneUIView.MiniGameLabelUIView);
        builder.RegisterInstance(_sceneUIView.MiniGameTimerUIView);
    }
}