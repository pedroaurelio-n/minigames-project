public interface IMiniGameSelectorModel
{
    MiniGameType NextType { get; }
    
    void Initialize ();
}