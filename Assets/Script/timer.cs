using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI countdown;

    public float roundMinutes;
    public float roundSecond;
    public bool timerAvailable;

    public uiMenu menu;

    float maxTime;
    float currentTime;

    private void Start() {
        timerAvailable = true; //Using timer
        countdown = gameObject.GetComponent<TextMeshProUGUI>();
        roundMinutes = 1f; //Temp time
        roundSecond = 30f;
        setTime(transformTime(roundMinutes) + roundSecond); //Set timer with unit of seconds
    }

    private void Update() {
        if(timerAvailable) { //If timer is available
            if(currentTime > 0) { //If in time limit
                currentTime -= Time.deltaTime; //Countdown

                string minutes, second; //Showing timer

                if((currentTime / 60) < 10) minutes = "0" + (int) (currentTime / 60); //Better minutes text showing
                else minutes = "" + (int) (currentTime / 60);

                if((currentTime % 60) < 10) second = "0" + (int) (currentTime % 60); //Better seconds text showing
                else second = "" + (int) (currentTime % 60);

                countdown.text = minutes + " : " + second; //Show timer time
            } else { //If time run off
                IsVictory();
            }
        }

        if(checkAiNumber() == 0) IsVictory(); //If Ai is empty, victory
    }

    public void setTime(float time) {
        maxTime = time; //Set maximum round time
        currentTime = maxTime; //Set current time to max time
    }

    float transformTime(float minutes) {
        return minutes * 60; //Transfor minutes to seconds
    }

    void IsVictory() {
        menu.victory(checkVictory()); //Check if victory
    }

    bool checkVictory() {
        if(checkAiNumber() == 0) return true; //If number of Ai is 0 return true
        else return false;
    }

    int checkAiNumber() {
        GameObject[] aiList = GameObject.FindGameObjectsWithTag("Ai"); //Get all game object with tag of "Ai"
        return aiList.Length; //return number of Ai
    }
}
