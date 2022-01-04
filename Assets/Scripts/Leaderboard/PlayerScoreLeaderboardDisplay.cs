using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerScoreLeaderboardDisplay : MonoBehaviour {
    public PlayerRef BoundPlayerRef;
    [SerializeField]
    protected TextMeshProUGUI ScoreText;
    
    protected Dictionary<int, Color> PlayerRefToColorMap = new Dictionary<int, Color>() {
        {0, Color.black},
        {1, Color.red},
        {2, Color.blue},
        {3, Color.green},
        {4, Color.white}
    };

    private void Start() {
        ScoreText.color = PlayerRefToColorMap[BoundPlayerRef];
    }

    public void SetScoreText(string newScore) {
        ScoreText.text = newScore;
    }
}
