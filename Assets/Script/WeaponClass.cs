using UnityEngine;

[System.Serializable]
public class WeaponClass {
    public string bulletType; //Name of weapon/bullet
    public int damage;
    public int maxMag;
    public GameObject weaponSprite; //Model of weapon

    public WeaponClass(string bulletTy, int dmg, int mag, GameObject sprite) {
        bulletType = bulletTy;
        damage = dmg;
        maxMag = mag;
        weaponSprite = sprite;
    }
}
