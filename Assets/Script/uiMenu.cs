﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiMenu: MonoBehaviour {
    public GameObject player;
    public GameObject endMenu;
    public Camera worldCamera;
    public TextMeshProUGUI scoreBoard;

    int score;
    Health playerHealth;

    private void Start() {
        score = 0;
        scoreBoard.text = "Score: " + score;
        playerHealth = player.GetComponent<Health>(); //Set health component
    }

    private void Update() {
        if(playerHealth.getHealth() <= 0) playerDie(); //If player die
    }

    void playerDie() {
        worldCamera.targetDisplay = 0;
        endMenu.SetActive(true); //Open revive menu
        Cursor.lockState = CursorLockMode.None; //Unlock the cursor to middle of screen
    }

    public void revive() {
        player.SetActive(true); //Enable object
        player.transform.position = new Vector3(0, 1.5f, 0); //Reset player position
        playerHealth.setHealth(); //Reset player health
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor to middle of screen
        worldCamera.targetDisplay = 1;
        endMenu.SetActive(false); //Disable menu
    }

    public void addScore(int s) {
        score += s;
        scoreUpdate();
    }

    void scoreUpdate() {
        scoreBoard.text = "Score: " + score;
    }
}
