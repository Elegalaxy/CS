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
        public Texture image;
        public string name;

        public Map(string n, Texture i) {
            image = i;
            name = n;
        }
    };

    public Map[] mapList;
    public RawImage currentImage;
    public TextMeshProUGUI currentText;
    int currentMapIndex;

    private void Start() {
        Debug.Log(mapList.Length);
        currentMapIndex = 0;
        currentText.text = mapList[currentMapIndex].name;
        currentImage.texture = mapList[currentMapIndex].image;
    }

    public void ChangeMap(bool next) {
        if(next) currentMapIndex++;
        else currentMapIndex--;

        if(currentMapIndex >= mapList.Length) currentMapIndex = 0;
        else if(currentMapIndex < 0) currentMapIndex = mapList.Length - 1;

        currentText.text = mapList[currentMapIndex].name;
        currentImage.texture = mapList[currentMapIndex].image;
    }

    public void SelectMap() {
        SceneManager.LoadScene(currentMapIndex+1);
    }
}
