using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiHealth: MonoBehaviour {
    public float maxHealth = 100;
    public float currentHealth;
    public uiMenu menu;
    public aiScript script;

    // Start is called before the first frame update
    void Start() {
        script = GetComponent<aiScript>();
        menu = GameObject.FindGameObjectWithTag("UI").GetComponent<uiMenu>();
        setHealth(); //Set starting health
    }

    public void takeDamage(float dmg) { //Function to take damage
        currentHealth -= dmg;
        script.gotAttackedByPlayer = true;
        if(currentHealth <= 0) die(); //Die if health <= 0
        //Debug.Log(gameObject.name + " " + currentHealth);
    }

    public void setHealth() {
        currentHealth = maxHealth; //Reset health function
    }

    public void setHealth(int h) {
        currentHealth = h; //Set certain health
    }

    public float getHealth() => currentHealth; //Return current health

    void die() {
        menu.addScore(100);
        gameObject.SetActive(false); //Disable object
    }
}
