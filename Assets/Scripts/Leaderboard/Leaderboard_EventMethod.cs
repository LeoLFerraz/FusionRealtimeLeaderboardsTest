using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Leaderboard_EventMethod : Leaderboard {
    protected override void Start() {
        base.Start();
        if(GameManager) GameManager.OnPlayersChanged += UpdateLeaderboard;
        UpdateLeaderboard(GameManager.ActivePlayers);
    }
}
