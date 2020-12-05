using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public void Quit() {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
