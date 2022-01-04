using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class GameManager : NetworkBehaviour {
    [Networked(OnChanged = nameof(ActivePlayersChanged))] // Check out Leaderboard_EventMethod to see how this OnChanged is used
    [Capacity(4)]
    public NetworkArray<PlayerController> ActivePlayers {get;} = MakeInitializer(new PlayerController[4]);

    #region Leaderboard_EventMethod Stuff

    public delegate void PlayersChangedDelegate(NetworkArray<PlayerController> players);
    public PlayersChangedDelegate OnPlayersChanged;

    public override void Spawned() {
        base.Spawned();
    }

    public static void ActivePlayersChanged(Changed<GameManager> changed) {
        changed.Behaviour.HandlePlayersChange(changed.Behaviour.ActivePlayers);
    }
    
    public void HandlePlayersChange() {
        OnPlayersChanged?.Invoke(ActivePlayers);
    }

    public void HandlePlayersChange(NetworkArray<PlayerController> players) {
        OnPlayersChanged?.Invoke(players);
    }
    
    #endregion
}
