using System;
using UnityEngine;

public class GameManager : MonoBehaviour {


    private int score;


    private void Start() {
        if (Projectile.Instance != null) {
            Projectile.Instance.OnEnemyFix += Projectile_OnEnemyFix;
        }
        else {Debug.LogWarning("Projectile instance not found. Event not subscribed.");}

    }

    private void Projectile_OnEnemyFix(object sender, System.EventArgs e) {
        AddScore(1);
    }
    
    public void AddScore(int addScoreAmount) {
        score += addScoreAmount;
        Debug.Log("Current Score is " + score);
    }
    
}
