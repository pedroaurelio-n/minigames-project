using UnityEngine;
using UnityEngine.InputSystem;

public class DebugAccess : MonoBehaviour
{
    LoadingManager _loadingManager;
    
    void Awake ()
    {
        _loadingManager = FindObjectOfType<LoadingManager>(true);
    }
    
    void Update ()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            if (_loadingManager.ApplicationSession.GameSession.CurrentCore is not GameCore core)
                return;
            
            core.SceneModel.MiniGameManagerModel.ForceCompleteMiniGame();
        }
        
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (_loadingManager.ApplicationSession.GameSession.CurrentCore is not GameCore core)
                return;
            
            core.SceneModel.MiniGameManagerModel.ForceFailMiniGame();
        }
    }
}
