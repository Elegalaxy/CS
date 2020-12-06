using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenu;

    private void Start() {
        Resume();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPause) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void loadScene(int n) {
        SceneManager.LoadScene(n);
    }

    public void Quit() {
        Application.Quit();
    }

    void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
