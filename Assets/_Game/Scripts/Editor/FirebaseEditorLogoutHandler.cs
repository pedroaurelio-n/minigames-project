#if UNITY_EDITOR
using UnityEditor;
using Firebase.Auth;

[InitializeOnLoad]
public class FirebaseEditorLogoutHandler
{
    static FirebaseEditorLogoutHandler()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }
    
    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            var auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                auth.SignOut();
            }
        }
    }
}
#endif