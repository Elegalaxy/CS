﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GunClass[] weaponList; //List for weapons

    public GunClass findWeapon(string name) { //Find weapon using name
        GunClass w = Array.Find(weaponList, weaponList => weaponList.bulletType == name);
        if(w == null) {
            //Debug.LogWarning(name + " Not Found!");
            return null;
        }
        return w;
    }
    public int getWeaponId(string name) { //Find weapon using name
        for(int i = 0; i < weaponList.Length; i++) {
            if(weaponList[i].bulletType == name) return i;
        }
        return -1;
    }
}
