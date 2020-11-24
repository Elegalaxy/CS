using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponClass[] weaponList; //List for weapons

    public WeaponClass findWeapon(string name) { //Find weapon using name
        WeaponClass w = Array.Find(weaponList, weaponList => weaponList.bulletType == name);
        if(w == null) {
            Debug.LogWarning(name + " Not Found!");
            return null;
        }
        return w;
    }
}
