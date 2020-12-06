using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickDrop : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider collider;
    Weapon weapon;
    Transform player,gunContainer, cam;

    public float pickRange;
    public float dropForce, dropUpForce;

    public bool equiped = false;
    public static int slotFull = 0;

    public int maxMag;
    public int currentMag;
    public int totalMag;
    public float reloadTime;
    public float shootingTime;
    public int currentDamage;

    GunClass currentWeapon;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = player.Find("Main Camera");
        gunContainer = cam.Find("Weapon");
        weapon = gunContainer.GetComponent<Weapon>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        initializeGun();

        pickRange = 2f; //Initialize pick range and drop force
        dropForce = 10f;
        dropUpForce = 7f;

        if(!equiped) {
            rb.isKinematic = false;
            collider.isTrigger = false;
            slotFull = 0;
        }else if(equiped) {
            rb.isKinematic = true;
            collider.isTrigger = true;
            slotFull = 1;
        }
    }
    
    private void Update() {
        Vector3 distanceToPlayer = player.position - transform.position; //Distance between gun and player
        if(!equiped && distanceToPlayer.magnitude <= pickRange && Input.GetKeyDown(KeyCode.E) && slotFull <= 2) PickUp(); //Pickup if click E

        if(equiped && Input.GetKeyDown(KeyCode.G)) Drop(); //Drop if click G
    }

    public void PickUp() {
        weapon.firePoint = gameObject.transform.Find("Fire Point");
        equiped = true; //This gun is equiped
        slotFull++; //Player slot is full

        transform.SetParent(gunContainer); //Set parent to gun container
        transform.localPosition = Vector3.zero; //Reset position
        transform.localRotation = Quaternion.Euler(Vector3.zero); //Reset rotation
        transform.localScale = Vector3.one; //Reset scale

        rb.isKinematic = true;
        collider.isTrigger = true; //Gun will not collide

        //weapon.weaponName = name;
        weapon.selectedWeapon = weapon.transform.childCount-1;
        weapon.selectWeapon();
    }

    void Drop() {
        equiped = false;
        slotFull--;

        transform.SetParent(null); //Set parent to null

        rb.isKinematic = false;
        collider.isTrigger = false;

        rb.velocity = player.GetComponent<CharacterController>().velocity; //Apply player velocity to gun

        rb.AddForce(cam.forward * dropForce, ForceMode.Impulse); //Add force while dropping
        rb.AddForce(cam.up * dropUpForce, ForceMode.Impulse); //Add upward force while dropping

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random)*10f);

        if(weapon.transform.childCount > 0) {
            weapon.selectedWeapon = weapon.transform.childCount - 1;
            weapon.weaponName = weapon.transform.GetChild(weapon.selectedWeapon).name;

        }
        weapon.selectWeapon();
    }
    public void initializeGun() { //Update damage and speed after pick a new gun
        currentWeapon = getWeapon(name); //Update weapon object
        if(currentWeapon != null) {
            currentDamage = currentWeapon.damage; //Update damage
            maxMag = currentWeapon.maxMag; //Update max mag
            totalMag = maxMag * 2; //Update total mag
            shootingTime = currentWeapon.shootingTime; //Update shooting time
            reloadTime = currentWeapon.reloadSpeed; //Update reload time
            currentMag = maxMag; //Update current mag
        } else {
            totalMag = 0; //If not found set all to 0
            shootingTime = 0;
            reloadTime = 0;
            currentMag = totalMag;
        }
    }

    GunClass getWeapon(string name) {
        return FindObjectOfType<WeaponManager>().findWeapon(name); //Find weapon object
    }
}
