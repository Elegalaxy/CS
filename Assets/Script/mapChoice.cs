using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class mapChoice : MonoBehaviour
{
    [System.Serializable]
    public struct Map {
        public Texture image; //Image of map
        public string name; //Name of map

        public Map(string n, Texture i) { //Constructor
            image = i;
            name = n;
        }
    };

    public Map[] mapList;
    public RawImage currentImage; //Current map
    public TextMeshProUGUI currentText;
    int currentMapIndex; //Current map index

    private void Start() {
        currentMapIndex = 0; //Initialize
        currentText.text = mapList[currentMapIndex].name;
        currentImage.texture = mapList[currentMapIndex].image;
    }

    public void ChangeMap(bool next) {
        if(next) currentMapIndex++; //Go to next map
        else currentMapIndex--; //Go to previous map

        if(currentMapIndex >= mapList.Length) currentMapIndex = 0; //Loop of map list
        else if(currentMapIndex < 0) currentMapIndex = mapList.Length - 1;

        currentText.text = mapList[currentMapIndex].name; //Set new name and image
        currentImage.texture = mapList[currentMapIndex].image;
    }

    public void SelectMap() {
        SceneManager.LoadScene(currentMapIndex+1); //Load map scene
    }
}
