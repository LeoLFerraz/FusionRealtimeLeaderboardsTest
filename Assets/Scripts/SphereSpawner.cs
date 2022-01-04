using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class SphereSpawner : NetworkBehaviour {
    [SerializeField]
    protected BoxCollider SpawningArea;
    [SerializeField]
    protected float SpawningCooldown = 1.5f;
    [SerializeField]
    protected NetworkObject SpherePrefab;
    [Networked]
    protected TickTimer NextSpawnTimer {get; set;}

    public override void FixedUpdateNetwork() {
        base.FixedUpdateNetwork();
        if(Object.HasStateAuthority && SpherePrefab && SpawningArea && NextSpawnTimer.ExpiredOrNotRunning(Runner)) {
            var boundsMin = SpawningArea.bounds.min;
            var boundsMax = SpawningArea.bounds.max;
            var randomPoint = new Vector3(Random.Range(boundsMin.x, boundsMax.x), Random.Range(boundsMin.y, boundsMax.y), Random.Range(boundsMin.z, boundsMax.z));
            Runner.Spawn(SpherePrefab, randomPoint, Quaternion.identity);
            NextSpawnTimer = TickTimer.CreateFromSeconds(Runner, SpawningCooldown);
        }
    }
}
