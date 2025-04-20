public interface IGameSessionInfoProvider
{
    string CurrentScene { get; }
    int CurrentSceneIndex { get; }
    MiniGameType CurrentMiniGameType { get; set; }
    bool HasStartedGameRun { get; set; }
    
    IPlayerInfoModel PlayerInfoModel { get; }
}