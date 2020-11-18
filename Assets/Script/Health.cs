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
        currentHealth = maxHealth; //Set starting health
    }

    public void takeDamage(float dmg) { //Function to take damage
        currentHealth -= dmg;
        if(currentHealth <= 0) gameObject.SetActive(false); //Die if health <= 0
        Debug.Log(currentHealth);
    }
}
