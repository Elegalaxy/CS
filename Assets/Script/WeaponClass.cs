using UnityEngine;

[System.Serializable]
public class WeaponClass {
    public string bulletType; //Name of weapon/bullet
    public int damage;
    public int maxMag;

    public WeaponClass(string bulletTy, int dmg, int mag) {
        bulletType = bulletTy;
        damage = dmg;
        maxMag = mag;
    }
}
