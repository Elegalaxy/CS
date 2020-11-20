using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon: MonoBehaviour {
    public Transform firePoint;
    public GameObject bullet;
    public Camera cam;
    public TMP_Text showBullet;

    public float reloadTime = 2f;
    public float shootingTime = 0.5f;
    public int maxMag = 10;
    public string weaponName = "Pistol";

    int currentDamage;

    int currentMag;
    float currentShootingTime;
    bool isReload;

    private void Start() {
        firePoint = gameObject.transform.Find("Fire Point");
        currentMag = maxMag; //Set starting mag
        updateBullet(); //Update mag info
        updateGun(); //Update bullet info from weapon
    }

    // Update is called once per frame
    void Update() {
        //count down
        if(currentShootingTime > 0) currentShootingTime -= Time.deltaTime; //Count shooting time

        if(Input.GetMouseButtonDown(0) && currentMag > 0 && currentShootingTime <= 0) shoot();
        //If left mouse button clicked when have mag and not cool down

        if(Input.GetKeyDown(KeyCode.R)) { //Reload
            currentShootingTime = reloadTime; //Set reload time
            isReload = true;
        }

        if(isReload && currentShootingTime <= 0) reload(); //finish reload when countdown end
    }

    void shoot() {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Set a ray from cursor
        RaycastHit hit;
        Vector3 targetPoint;
        Vector3 direction;

        if(Physics.Raycast(ray, out hit)) targetPoint = hit.point; //If hit something, return to hit
        else targetPoint = ray.GetPoint(75); //If not, set it to somewhere far away

        direction = targetPoint - firePoint.position; //Calculate direction of the bullet

        GameObject currentBullet = Instantiate(bullet, firePoint.position, firePoint.rotation); //Instantiate bullet
        currentBullet.GetComponent<Bullet>().setStat(currentDamage);
        currentBullet.transform.forward = direction; //Shoot the bullet to the direction
        FindObjectOfType<SoundManager>().play("PistolShoot");

        currentShootingTime = shootingTime; //Reset shooting time
        currentMag--; //Decrease bullet and update UI
        updateBullet();
    }

    void reload() { //Reload function
        currentMag = maxMag;
        isReload = false;
        updateBullet(); //Update UI
    }

    void updateBullet() {
        showBullet.text = currentMag + " / " + maxMag; //Update UI
    }

    public void updateGun() { //Update damage and speed after pick a new gun
        currentDamage = FindObjectOfType<WeaponManager>().findWeapon(weaponName).damage;
    }
}
