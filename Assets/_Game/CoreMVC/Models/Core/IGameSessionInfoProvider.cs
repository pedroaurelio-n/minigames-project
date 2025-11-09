public interface IGameSessionInfoProvider
{
    //TODO pedro: create MiniGameInfo struct to refactor type/category
    string CurrentScene { get; }
    string CurrentSceneViewName { get; set; }
    MiniGameType CurrentMiniGameType { get; set; }
    MiniGameType NextMiniGameType { get; set; }
    bool HasStartedGameRun { get; set; }
    
    IPlayerInfoModel PlayerInfoModel { get; }
}