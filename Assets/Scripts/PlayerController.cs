using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    
    
    [SerializeField] private InputAction MoveAction;
    private Rigidbody2D rigidbody2d;
    private Vector2 move;
    [SerializeField] private float moveSpeed = 3.0f;

    [SerializeField] private int maxHealth = 5;
    private int minHealth = 0;
    private int currentHealth;
    
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    [SerializeField] private float timeInvincible = 2.0f;
    private bool isInvincible;
    private float damageCooldown;
    
    
    
    private void Start() {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        currentHealth = maxHealth;
        Debug.Log(currentHealth + "/" + maxHealth);        
    }

    private void Update() {
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);

        if (isInvincible) {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) {
                isInvincible = false;
            }
        }
        
    }

    private void FixedUpdate() {
        Vector2 position = (Vector2)rigidbody2d.position + move * (moveSpeed * Time.deltaTime);
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }
}
