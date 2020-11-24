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
    public static bool slotFull = false;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = player.Find("Main Camera");
        gunContainer = cam.Find("Weapon");
        weapon = gunContainer.GetComponent<Weapon>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        pickRange = 2f; //Initialize pick range and drop force
        dropForce = 10f;
        dropUpForce = 7f;

        if(!equiped) {
            rb.isKinematic = false;
            collider.isTrigger = false;
        }else if(equiped) {
            rb.isKinematic = true;
            collider.isTrigger = true;
            slotFull = true;
        }
    }
    
    private void Update() {
        Vector3 distanceToPlayer = player.position - transform.position; //Distance between gun and player
        if(!equiped && distanceToPlayer.magnitude <= pickRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp(); //Pickup if click E

        if(equiped && Input.GetKeyDown(KeyCode.G)) Drop(); //Drop if click G
    }

    void PickUp() {
        weapon.firePoint = gameObject.transform.Find("Fire Point");
        equiped = true; //This gun is equiped
        slotFull = true; //Player slot is full

        transform.SetParent(gunContainer); //Set parent to gun container
        transform.localPosition = Vector3.zero; //Reset position
        transform.localRotation = Quaternion.Euler(Vector3.zero); //Reset rotation
        transform.localScale = Vector3.one; //Reset scale

        rb.isKinematic = true;
        collider.isTrigger = true; //Gun will not collide

        weapon.weaponName = name;
        weapon.updateGun();
    }

    void Drop() {
        equiped = false;
        slotFull = false;

        transform.SetParent(null); //Set parent to null

        rb.isKinematic = false;
        collider.isTrigger = false;

        rb.velocity = player.GetComponent<CharacterController>().velocity; //Apply player velocity to gun

        rb.AddForce(cam.forward * dropForce, ForceMode.Impulse); //Add force while dropping
        rb.AddForce(cam.up * dropUpForce, ForceMode.Impulse); //Add upward force while dropping

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random)*10f);

        weapon.weaponName = "";
        weapon.updateGun();
    }

}
