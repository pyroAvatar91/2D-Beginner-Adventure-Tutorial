using System;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Projectile : MonoBehaviour {
    
    
    public static Projectile Instance { get; private set; }
    public event EventHandler OnEnemyFix;
    
    private Rigidbody2D rigidbody2d;
    private readonly float maxDistance = 50.0f;

    private void Awake() {
        Instance = this;
        
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (transform.position.magnitude > maxDistance) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force) {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null) {
            enemy.Fix();
            OnEnemyFix?.Invoke(this, EventArgs.Empty);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }


}
