using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class uiMenu: MonoBehaviour {
    public GameObject player;
    public GameObject endMenu;
    public Camera worldCamera;
    public TextMeshProUGUI scoreBoard;

    public GameObject victoryMenu;

    int score;
    Health playerHealth;

    private void Start() {
        GameObject.Find("DamageCursor").GetComponent<RawImage>().canvasRenderer.SetAlpha(0f);
        score = 0; //Initialize score
        scoreBoard.text = "Score: " + score;
        playerHealth = player.GetComponent<Health>(); //Set health component
        victoryMenu.SetActive(false);
        endMenu.SetActive(false);
    }

    private void Update() {
        if(playerHealth.getHealth() <= 0) playerDie(); //If player die
    }

    void playerDie() {
        worldCamera.targetDisplay = 0; //Change camera view
        endMenu.SetActive(true); //Open revive menu
        Cursor.lockState = CursorLockMode.None; //Unlock the cursor to middle of screen
    }

    public void revive() {
        player.SetActive(true); //Enable object
        player.transform.position = new Vector3(0, 1.5f, 0); //Reset player position
        playerHealth.setHealth(); //Reset player health
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor to middle of screen
        worldCamera.targetDisplay = 1; //Change back camera
        endMenu.SetActive(false); //Disable menu
    }

    public void addScore(int s) {
        score += s; //Add score
        scoreUpdate(); //Update text
    }

    void scoreUpdate() {
        scoreBoard.text = "Score: " + score; //Update score text
    }

    void ShowResult() {
        victoryMenu.SetActive(true); //Open result menu
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Time.timeScale = 0f; //Stop time
    }

    public void victory(bool isWin) {
        string result;
        if(isWin) {
            result = "You Win!"; //Set result text
        } else {
            result = "You Lose!";
        }
        victoryMenu.transform.Find("Victory").GetComponent<TextMeshProUGUI>().text = result;
        victoryMenu.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = scoreBoard.text; //Show score
        ShowResult();
    }

    public void LoadScenes(bool isRestart) {
        if(isRestart) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Restart
        else SceneManager.LoadScene(0); //Load Menu
    }
}
