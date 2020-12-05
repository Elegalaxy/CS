using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        setHealth(); //Set starting health
    }

    public void takeDamage(float dmg) { //Function to take damage
        currentHealth -= dmg;
        healthBar.SetHealthBar();
        if(currentHealth <= 0) die(); //Die if health <= 0
    }

    public void setHealth() {
        currentHealth = maxHealth; //Reset health function
        healthBar.SetHealthBar();
    }

    public void setHealth(int h) {
        currentHealth = h; //Set certain health
    }

    public float getHealth() => currentHealth; //Return current health

    void die() {
        gameObject.SetActive(false); //Disable object
    }
}
