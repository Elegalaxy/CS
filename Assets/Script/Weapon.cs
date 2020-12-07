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

    public float recoil = 2f;
    public float meleeTime = 1f;

    public string weaponName;
    public int selectedWeapon;

    public GameObject meleeParticle;

    PickDrop currentWeapon;

    float currentShootingTime;
    bool isReload;

    int maxMag;
    float reloadTime;
    float shootingTime;
    int currentDamage;

    void Start() {
        if(transform.GetChild(0) != null) {
            transform.GetChild(0).GetComponent<PickDrop>().PickUp();
            selectedWeapon = 0;
        }
        selectWeapon();
        updateGun(); //Update bullet info from weapon

        meleeTime = 1f;
    }

    // Update is called once per frame
    void Update() {
        //countdown
        if(currentShootingTime > 0) currentShootingTime -= Time.deltaTime; //Count shooting time

        if(Input.GetMouseButton(0) && currentWeapon.currentMag > 0 && currentShootingTime <= 0 && currentWeapon != null) shoot();
        //If left mouse button clicked when have mag and not cool down

        if(PickDrop.slotFull > 0 && currentWeapon.totalMag > 0 && currentWeapon.currentMag != maxMag && (Input.GetKeyDown(KeyCode.R) || (currentWeapon.currentMag == 0 && Input.GetMouseButtonUp(0)))) {
            startReload(); //Reload if press R or mag is finish but wanna shoot
        }

        if(isReload && currentShootingTime <= 0) reload(); //finish reload when countdown end

        //Switch weapon

        int previousSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if(selectedWeapon >= transform.childCount - 1) selectedWeapon = 0;
            else selectedWeapon++;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if(selectedWeapon <= 0) selectedWeapon = transform.childCount - 1;
            else selectedWeapon--;
        }

        if(previousSelectedWeapon != selectedWeapon) selectWeapon();

        //Melee
        if(Input.GetKeyDown(KeyCode.C)) {
            melee();
        }
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

        Bullet currentBullet = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet>(); //Instantiate bullet
        currentBullet.setStat(currentDamage); //Set bullet damage
        currentBullet.shootByPlayer = true;
        currentBullet.transform.forward = direction; //Shoot the bullet to the direction
        FindObjectOfType<SoundManager>().play(weaponName); //Play gun sound
        AddRecoil(recoil); //Add recoil

        currentShootingTime = shootingTime; //Reset shooting time
        currentWeapon.currentMag--; //Decrease bullet
        updateBullet(); //Update bullet count
    }

    void startReload() {
        FindObjectOfType<SoundManager>().play("Reload"); //Play gun reload sound
        currentShootingTime = reloadTime; //Set reload time
        isReload = true;
    }

    void reload() { //Reload function
        if(currentWeapon.totalMag >= maxMag) { //If reminding mag is bigger than max
            currentWeapon.totalMag -= maxMag - currentWeapon.currentMag;
            currentWeapon.currentMag = maxMag;
        } else {
            if(maxMag - currentWeapon.currentMag <= currentWeapon.totalMag) { //If totalMag is bigger than require mag
                currentWeapon.totalMag -= maxMag - currentWeapon.currentMag;
                currentWeapon.currentMag = maxMag;
            } else {
                currentWeapon.currentMag += currentWeapon.totalMag; //If total mag is not enough
                currentWeapon.totalMag = 0;
            }
        }

        isReload = false;
        updateBullet(); //Update UI
    }

    void updateBullet() {
        if(currentWeapon != null) showBullet.text = currentWeapon.currentMag + " / " + currentWeapon.totalMag; //Update UI
        else showBullet.text = "0 / 0";
    }

    void AddRecoil(float force) {
        look.xRotation -= force; //Add recoil on weapon
    }

    void updateGun() { //Update damage and speed after pick a new gun
        currentWeapon = getWeapon(weaponName); //Update weapon object
        if(currentWeapon != null) {
            currentDamage = currentWeapon.currentDamage; //Update damage
            maxMag = currentWeapon.maxMag; //Update max mag
            shootingTime = currentWeapon.shootingTime; //Update shooting time
            reloadTime = currentWeapon.reloadTime; //Update reload time
        } else {
            shootingTime = 0; //If not found set all to 0
            reloadTime = 0;
        }
        updateBullet(); //Update bullet info
    }

    PickDrop getWeapon(string name) {
        return transform.Find(name).GetComponent<PickDrop>(); //Find weapon object
    }

    //Public functions
    public void selectWeapon() {
        int i = 0;
        foreach(Transform weapon in transform) {
            if(i == selectedWeapon) weapon.gameObject.SetActive(true); //Set choosen weapon active
            else weapon.gameObject.SetActive(false); //Set other weapon unactive
            i++;
        }
        if(transform.childCount > 0) weaponName = transform.GetChild(selectedWeapon).name; //Set weapon name if got weapon
        else weaponName = "";
        updateGun(); //Update damage and speed
    }

    void melee() {
        GameObject currentParticle = Instantiate(meleeParticle, firePoint.position, firePoint.rotation);
        currentParticle.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        currentParticle.GetComponent<Rigidbody>().AddForce(currentParticle.transform.up * 100f);
        currentParticle.GetComponent<Rigidbody>().AddForce(currentParticle.transform.right * 500f);
        Destroy(currentParticle, 0.5f);
    }
}