﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiWeapon : MonoBehaviour
{
    public Transform weapon, firePoint;
    public GameObject bullet;
    public GameObject muzzle;
    public GunClass weaponType;

    int currentDamage, maxMag, currentMag;
    float reloadTime;

    void Start()
    {
        weapon = gameObject.transform.GetChild(0);
        weapon.GetComponent<PickDrop>().enabled = false;
        firePoint = weapon.transform.Find("Fire Point");
        firePoint.rotation = Quaternion.Euler(0, 180, 0);
        weaponType = GameObject.FindObjectOfType<WeaponManager>().findWeapon(weapon.name);

        currentDamage = weaponType.damage;
        maxMag = weaponType.maxMag;
        reloadTime = weaponType.reloadSpeed;
    }

    public void Shoot() {
        if(currentMag <= 0) {
            Invoke(nameof(reload), reloadTime);
        } else {
            GameObject muz = Instantiate(muzzle, firePoint.position, firePoint.rotation);
            muz.transform.SetParent(transform);
            Destroy(muz, 0.13f);
            Bullet currentBullet = Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet>(); //Instantiate bullet
            currentBullet.setStat(currentDamage); //Set bullet damage
            currentBullet.shootByPlayer = false;
            FindObjectOfType<SoundManager>().play(weapon.name); //Play gun sound
            currentMag--;
        }
    }

    void reload() {
        currentMag = maxMag;
    }
}
