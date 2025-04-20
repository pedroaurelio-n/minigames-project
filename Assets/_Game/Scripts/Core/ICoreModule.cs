using System;

public interface ICoreModule : IDisposable
{
    event Action OnInitializationComplete;
    
    void Initialize ();
}