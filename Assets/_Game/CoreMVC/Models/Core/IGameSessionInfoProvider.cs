public interface IGameSessionInfoProvider
{
    string CurrentScene { get; }
    MiniGameType CurrentMiniGameType { get; set; }
    MiniGameType NextMiniGameType { get; set; }
    bool HasStartedGameRun { get; set; }
    
    IPlayerInfoModel PlayerInfoModel { get; }
}