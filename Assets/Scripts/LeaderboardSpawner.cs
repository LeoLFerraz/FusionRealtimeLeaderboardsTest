using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class LeaderboardSpawner : NetworkBehaviour {
    [SerializeField]
    protected Leaderboard LeaderboardPrefab;

    public override void Spawned() {
        base.Spawned();
        if(Object.HasInputAuthority) Instantiate(LeaderboardPrefab, Vector3.zero, Quaternion.identity);
    }

    public void SpawnLeaderboard(NetworkRunner runner, PlayerRef playerRef) {
        if(LeaderboardPrefab && runner.LocalPlayer == playerRef) Instantiate(LeaderboardPrefab, Vector3.zero, Quaternion.identity);
    }
}
