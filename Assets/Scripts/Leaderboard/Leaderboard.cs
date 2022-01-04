using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class Leaderboard : MonoBehaviour {
    [SerializeField]
    protected PlayerScoreLeaderboardDisplay PlayerScoreDisplayPrefab;
    [SerializeField]
    protected bool RefreshActive = true;
    
    protected List<PlayerScoreLeaderboardDisplay> ScoresTracked = new();
    protected GameManager GameManager;

    protected virtual void Start() {
        GameManager = FindObjectOfType<GameManager>();
        var canvas = FindObjectOfType<Canvas>();
        if(canvas) transform.SetParent(canvas.transform, false);
    }

    public virtual void UpdateLeaderboard(List<PlayerController> players) {
        foreach (var player in players) {
            if(!player) continue;
            var playerRef = player.Object.InputAuthority;
            if(!ScoresTracked.Find(x => x.BoundPlayerRef == playerRef)) {
                HandleNewPlayer(playerRef);
            }
            
            UpdatePlayerStatus(playerRef);
        }
    }
    
    public virtual void UpdateLeaderboard(NetworkArray<PlayerController> players) {
        foreach (var player in players) {
            if(!player) continue;
            var playerRef = player.Object.InputAuthority;
            if(!ScoresTracked.Find(x => x.BoundPlayerRef == playerRef)) {
                HandleNewPlayer(playerRef);
            }
            
            UpdatePlayerStatus(playerRef);
            OrderChildrenByScore(players);
        }
    }
    
    protected virtual void HandleNewPlayer(PlayerRef newPlayerRef) {
        var psd = Instantiate(PlayerScoreDisplayPrefab, transform);
        psd.BoundPlayerRef = newPlayerRef;
        ScoresTracked.Add(psd);
    }
    
    protected virtual void UpdatePlayerStatus(PlayerRef playerRef) {
        var psd = ScoresTracked.Find(x => x.BoundPlayerRef == playerRef);
        if(psd) {
            psd.SetScoreText(GameManager.ActivePlayers[playerRef].GetScore().ToString());
        }
    }
    
    protected virtual void OrderChildrenByScore(NetworkArray<PlayerController> players) {
        var playersCopy = players.ToArray();
        var orderedList = playersCopy.OrderByDescending(e => e ? e.GetScore() : 0).ToList();
        for(var i = 0; i < orderedList.Count; i++) {
            if(!orderedList[i]) continue;
            var psd = ScoresTracked.Find(x => x.BoundPlayerRef == orderedList[i].Object.InputAuthority);
            if(psd) {
                psd.transform.SetSiblingIndex(i);
            }
        }
    }
}
