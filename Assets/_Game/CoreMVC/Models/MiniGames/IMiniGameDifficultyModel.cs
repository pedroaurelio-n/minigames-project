using System;

public interface IMiniGameDifficultyModel : IDisposable
{
    int CurrentDifficultyLevel { get; }
    float CurrentTimerDecrease { get; }
    bool IsVariantLevel { get; }
    
    void UpdateDependencies (IMiniGameManagerModel miniGameManagerModel);
    void Initialize ();
}