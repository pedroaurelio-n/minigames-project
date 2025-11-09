using System;
using UnityEngine;

public class JoystickAimSceneView : SceneView
{
    [field: SerializeField] public Transform RotatingObject { get; private set; }
    [field: SerializeField] public Transform ProjectileSpawnPoint { get; private set; }
    [field: SerializeField] public JoystickAimProjectileView ProjectilePrefab { get; private set; }
    [field: SerializeField] public JoystickAimTargetView TargetPrefab { get; private set; }

    void OnDrawGizmos ()
    {
        Vector2 spawnRange = GameGlobalOptions.Instance.MiniGameOptions.JoystickAimMiniGameOptions.SpawnRange;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnRange.x * 2, 0f, spawnRange.y * 2));
        
        Debug.DrawRay(ProjectileSpawnPoint.position, ProjectileSpawnPoint.forward * 2f, Color.blue);
    }
}