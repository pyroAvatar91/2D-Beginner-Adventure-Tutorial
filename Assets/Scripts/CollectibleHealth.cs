using UnityEngine;

public class CollectibleHealth : MonoBehaviour {
    
    [SerializeField] private int addHealth = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        var controller = other.GetComponent<PlayerController>();
        
        if (controller == null) return;
        if (controller.CurrentHealth >= controller.MaxHealth) return; 
            controller.ChangeHealth(addHealth);
            Destroy(gameObject);
        
    }
}
