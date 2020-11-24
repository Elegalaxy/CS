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
    public Look look;

    public float reloadTime;
    public float shootingTime;
    public float recoil = 2f;

    public int maxMag;
    public string weaponName;

    int currentDamage;

    int totalMag;
    int currentMag;
    float currentShootingTime;
    bool isReload;
    GunClass currentWeapon;

    void Start() {
        updateGun(); //Update bullet info from weapon
    }

    // Update is called once per frame
    void Update() {
        //countdown
        if(currentShootingTime > 0) currentShootingTime -= Time.deltaTime; //Count shooting time

        if(Input.GetMouseButton(0) && currentMag > 0 && currentShootingTime <= 0 && currentWeapon != null) shoot();
        //If left mouse button clicked when have mag and not cool down

        if(PickDrop.slotFull && totalMag > 0 && currentMag != maxMag && (Input.GetKeyDown(KeyCode.R) || (currentMag == 0 && Input.GetMouseButtonUp(0)))) {
            startReload(); //Reload if press R or mag is finish but wanna shoot
        }

        if(isReload && currentShootingTime <= 0) reload(); //finish reload when countdown end
    }


    //Private functions
    void shoot() {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Set a ray from cursor
        RaycastHit hit;
        Vector3 targetPoint;
        Vector3 direction;

        if(Physics.Raycast(ray, out hit)) targetPoint = hit.point; //If hit something, return to hit
        else targetPoint = ray.GetPoint(75); //If not, set it to somewhere far away

        direction = targetPoint - firePoint.position; //Calculate direction of the bullet

        GameObject currentBullet = Instantiate(bullet, firePoint.position, firePoint.rotation); //Instantiate bullet
        currentBullet.GetComponent<Bullet>().setStat(currentDamage); //Set bullet damage
        currentBullet.transform.forward = direction; //Shoot the bullet to the direction
        FindObjectOfType<SoundManager>().play(weaponName); //Play gun sound
        AddRecoil(recoil); //Add recoil

        currentShootingTime = shootingTime; //Reset shooting time
        currentMag--; //Decrease bullet
        updateBullet(); //Update bullet count
    }

    void startReload() {
        FindObjectOfType<SoundManager>().play("Reload"); //Play gun reload sound
        currentShootingTime = reloadTime; //Set reload time
        isReload = true;
    }

    void reload() { //Reload function
        if(totalMag >= maxMag) {
            totalMag -= maxMag - currentMag;
            currentMag = maxMag;
        } else {
            if(maxMag - currentMag <= totalMag) {
                totalMag -= maxMag - currentMag;
                currentMag = maxMag;
            } else {
                currentMag += totalMag;
                totalMag = 0;
            }
        }

        isReload = false;
        updateBullet(); //Update UI
    }

    void updateBullet() {
        showBullet.text = currentMag + " / " + totalMag; //Update UI
    }

    void AddRecoil(float force) {
        look.xRotation -= force; //Add recoil on weapon
    }

    //Public functions
    public void updateGun() { //Update damage and speed after pick a new gun
        currentWeapon = getWeapon(weaponName);
        if(currentWeapon != null) {
            currentDamage = currentWeapon.damage;
            maxMag = currentWeapon.maxMag;
            totalMag = maxMag * 2;
            shootingTime = currentWeapon.shootingTime;
            reloadTime = currentWeapon.reloadSpeed;
            currentMag = maxMag;
        } else {
            totalMag = 0;
            shootingTime = 0;
            reloadTime = 0;
            currentMag = totalMag;
        }
        updateBullet();
    }

    GunClass getWeapon(string name) {
        return FindObjectOfType<WeaponManager>().findWeapon(weaponName);
    }
}