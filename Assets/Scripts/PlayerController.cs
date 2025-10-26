using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    
    
    Animator animator;
    private Vector2 moveDirection = new Vector2(1, 0);

    private Rigidbody2D rigidbody2d;
    private Vector2 move;
    [SerializeField] private InputAction MoveAction;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float timeInvincible = 2.0f;
    [SerializeField] private GameObject projectilePrefab;
    
    private int minHealth = 0;
    private int currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    
    private bool isInvincible;
    private float damageCooldown;
    
    
    
    private void Start() {
        animator = GetComponent<Animator>();
        
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        currentHealth = maxHealth;
        Debug.Log(currentHealth + "/" + maxHealth);        
    }

    private void Update() {
        move = MoveAction.ReadValue<Vector2>();
        
        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f)) {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible) {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Launch();
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
            animator.SetTrigger("Hit");
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    private void Launch() {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }
    
}
