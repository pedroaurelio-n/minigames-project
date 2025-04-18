using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void SetActive (this MonoBehaviour monoBehaviour, bool active)
    {
        monoBehaviour.gameObject.SetActive(active);
    }
}