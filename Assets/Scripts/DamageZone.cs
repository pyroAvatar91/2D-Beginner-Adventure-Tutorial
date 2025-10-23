using System;
using UnityEngine;

public class DamageZone : MonoBehaviour {


    [SerializeField] private int removeHealth = -1;


    private void OnTriggerStay2D(Collider2D other) {
        var controller = other.GetComponent<PlayerController>();

        if (controller != null) {
            controller.ChangeHealth(removeHealth);
        }
    }
    
    
}
