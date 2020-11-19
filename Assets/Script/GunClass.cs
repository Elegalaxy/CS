using UnityEngine;

[System.Serializable]
public class GunClass : WeaponClass {
    public float recoil;
    public float reloadSpeed;

    public GunClass(string bulletTy, int dmg, int mag, GameObject sprite, float rec, float reloadSpd): base(bulletTy, dmg, mag, sprite) {
        recoil = rec;
        reloadSpeed = reloadSpd;
    }
}
