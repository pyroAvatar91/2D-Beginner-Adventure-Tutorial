using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    
    private Rigidbody2D rigidbody2d;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private bool vertical;
    [SerializeField] private float timeMoving = 3.0f;
    [SerializeField] private int direction = 1;
    [SerializeField] private int removeHealth = -1;
    [SerializeField] private bool isSlowVariant = false;
    [SerializeField] private bool isSlowLeft = true;
    [SerializeField] private int currentState = 0; // 0: horizontal, 1: vertical, 2: reverse vertical, 3: reverse horizontal
    [SerializeField] private float stateDuration = 2.0f;   

    private float movingTimer;
    private float stateTimer;
    
    private static readonly Vector2[] MoveDirectionsLeft = {
        Vector2.left,   // 0: horizontal
        Vector2.down,   // 1: vertical
        Vector2.up,     // 2: reverse vertical
        Vector2.right   // 3: reverse horizontal
    };
    
    private static readonly Vector2[] MoveDirectionsRight = {
        Vector2.right,  // 0: horizontal
        Vector2.down,   // 1: vertical
        Vector2.up,     // 2: reverse vertical
        Vector2.left    // 3: reverse horizontal
    };
    
    
    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        movingTimer = timeMoving;
        stateTimer = stateDuration;
    }


    private void Update() {
        if (isSlowVariant) {
            stateTimer -= Time.deltaTime;
            if (stateTimer <= 0) {
                currentState = (currentState + 1) % 4;
                stateTimer = stateDuration;
            }
        }
        else {
            movingTimer -= Time.deltaTime;
            if (movingTimer < 0) {
                direction = -direction;
                movingTimer = timeMoving;
            }
        }

    }
    

    private void FixedUpdate() {
        Vector2 position = rigidbody2d.position;

        if (isSlowVariant) {
            var directions = isSlowLeft ? MoveDirectionsLeft : MoveDirectionsRight;
            position += directions[currentState] * (moveSpeed * Time.deltaTime);
        }
        else {
            if (vertical) {
                position.y = position.y + (direction * moveSpeed * Time.deltaTime);
            }
            else {
                position.x = position.x + (direction * moveSpeed * Time.deltaTime);
            }
        }
        rigidbody2d.MovePosition(position);

    }


    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null) {player.ChangeHealth(removeHealth);}
    }
    
}
