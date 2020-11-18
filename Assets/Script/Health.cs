using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float dmg) {
        currentHealth -= dmg;
        if(currentHealth <= 0) gameObject.SetActive(false);
        Debug.Log(currentHealth);
    }
}
