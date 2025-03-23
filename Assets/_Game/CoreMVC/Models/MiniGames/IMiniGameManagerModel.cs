public interface IMiniGameManagerModel
{
    IMiniGameModel ActiveMiniGame { get; }
    MiniGameType ActiveMiniGameType { get; }
    
    void Initialize ();
    void LateInitialize ();
}