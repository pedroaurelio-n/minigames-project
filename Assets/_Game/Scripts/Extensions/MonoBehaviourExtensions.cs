using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void SetActive (this MonoBehaviour monoBehaviour, bool active)
    {
        monoBehaviour.gameObject.SetActive(active);
    }

    public static bool ActiveInHierarchy (this MonoBehaviour monoBehaviour)
    {
        return monoBehaviour.gameObject.activeInHierarchy;
    }
}