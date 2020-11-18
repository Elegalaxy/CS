using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 10f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ground") Destroy(gameObject);

        if(other.gameObject.GetComponent<Health>() != null) {
            Health health = other.gameObject.GetComponent<Health>();
            health.takeDamage(damage);
        }
    }
}
