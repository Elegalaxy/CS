using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aiHealth: MonoBehaviour {
    public float maxHealth = 100;
    public float currentHealth;
    public uiMenu menu;
    public aiScript script;

    // Start is called before the first frame update
    void Start() {
        script = GetComponent<aiScript>(); //Get AI script
        menu = GameObject.FindGameObjectWithTag("UI").GetComponent<uiMenu>(); //Get UI
        setHealth(); //Set starting health
    }

    public void takeDamage(float dmg) { //Function to take damage
        currentHealth -= dmg; //Damage
        RawImage dmgCursor = GameObject.Find("DamageCursor").GetComponent<RawImage>(); //Damage cursor feedback
        dmgCursor.CrossFadeAlpha(1, 0f, false); //Set cursor visible
        dmgCursor.CrossFadeAlpha(0, 0.5f,false); //Fade out
        script.gotAttackedByPlayer = true;
        if(currentHealth <= 0) die(); //Die if health <= 0
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
