using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float damage = 10f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); //Add force when the bullet is shot
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ground") destroy(); //Destroy when hit ground

        if(other.gameObject.GetComponent<Health>() != null) { //If hit player
            Health health = other.gameObject.GetComponent<Health>();
            health.takeDamage(damage); //Damage player
            destroy();
        }

        if(other.gameObject.GetComponent<aiHealth>() != null) { //If hit AI
            aiHealth health = other.gameObject.GetComponent<aiHealth>();
            health.takeDamage(damage); //Damage AI
            destroy();
        }
    }

    public void destroy() { //Self-destroy function
        Destroy(gameObject);
    }

    public void setStat(int dmg) { //Set damage from weapon
        damage = dmg;
    }
}
