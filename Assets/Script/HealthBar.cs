using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Health player;

    private void Start() {
        float health = player.maxHealth;
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealthBar() {
        float health = player.currentHealth;
        slider.value = health;
    }
}
