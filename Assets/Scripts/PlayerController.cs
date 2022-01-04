using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour {
    [SerializeField]
    protected float Radius = 1.5f;
    [SerializeField]
    protected LayerMask SpheresLayer;
    [SerializeField]
    protected Leaderboard LeaderboardPrefab;
    [Networked(OnChanged = nameof(ScoreChanged))]
    protected int Score {get; set;}
    [Networked]
    protected GameManager GameManager {get; set;}

    public override void Spawned() {
        base.Spawned();
        GameManager = FindObjectOfType<GameManager>();
        if(GameManager) {
            GameManager.ActivePlayers.Set(Object.InputAuthority, this);
        }
    }

    public override void FixedUpdateNetwork() {
        base.FixedUpdateNetwork();
        if(!Object.HasStateAuthority) return;
        var hits = new List<LagCompensatedHit>();
        if(Runner.LagCompensation.OverlapSphere(transform.position, Radius, Object.InputAuthority, hits, SpheresLayer, HitOptions.IncludePhysX) > 0) {
            foreach (var hitSphere in hits) {
                var gs = hitSphere.GameObject.GetComponentInChildren<GameSphere>();
                if(gs && !gs.hit) {
                    gs.hit = true;
                    Score++;
                    Runner.Despawn(gs.Object);
                }
            }
        }
    }
    
    public static void ScoreChanged(Changed<PlayerController> changed) {
        changed.Behaviour.GameManager.HandlePlayersChange();
    }
    
    public int GetScore() {
        return Score;
    }
}
