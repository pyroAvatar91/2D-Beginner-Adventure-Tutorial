using UnityEngine;

public class Projectile : MonoBehaviour {
    
    
    private Rigidbody2D rigidbody2d;
    private readonly float maxDistance = 50.0f;

    private void Awake() {
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
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }


}
