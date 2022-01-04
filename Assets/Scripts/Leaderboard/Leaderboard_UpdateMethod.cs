using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class Leaderboard_UpdateMethod : Leaderboard {
    [SerializeField]
    protected float RefreshCooldown = 0.25f;
    
    protected Coroutine ActiveRefreshCoroutine;

    protected override void Start() {
        base.Start();
        ActiveRefreshCoroutine = StartCoroutine(LeaderboardRefreshCoroutine());
    }

    protected IEnumerator LeaderboardRefreshCoroutine() {
        while(RefreshActive) {
            if(GameManager.Object.IsValid) UpdateLeaderboard(GameManager.ActivePlayers);
            yield return new WaitForSeconds(RefreshCooldown);
        }
    }
}
