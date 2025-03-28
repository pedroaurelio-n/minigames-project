public interface IGameSessionInfoProvider
{
    string CurrentScene { get; }
    int CurrentSceneIndex { get; }
    
    IPlayerInfoModel PlayerInfoModel { get; }
}