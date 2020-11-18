using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour {
    public Transform firePoint;
    public GameObject bullet;
    public Camera cam;

    public float reloadTime = 2f;
    public float shootingTime = 0.5f;
    public int maxMag = 10;

    int weaponIndex = 0;
    int currentMag;
    float currentShootingTime;
    bool isReload;

    private void Start() {
        currentMag = maxMag; //Set starting mag
    }

    // Update is called once per frame
    void Update() {
        //count down
        if(currentShootingTime > 0) currentShootingTime -= Time.deltaTime; //Count shooting time

        if(Input.GetMouseButtonDown(0) && currentMag > 0 && currentShootingTime <= 0) shoot();
        //If left mouse button clicked when have mag and not cool down

        if(Input.GetKeyDown(KeyCode.R)) {
            currentShootingTime = reloadTime;
            isReload = true;
        }

        if(isReload && currentShootingTime <= 0) reload();
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
        currentBullet.transform.forward = direction; //Shoot the bullet to the direction

        currentShootingTime = shootingTime; //Reset shooting time
        currentMag--;
    }

    void reload() {
        currentMag = maxMag;
        isReload = false;
    }
}
