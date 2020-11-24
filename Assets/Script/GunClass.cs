using UnityEngine;

[System.Serializable]
public class GunClass : WeaponClass {
    public float shootingTime;
    public float reloadSpeed;

    public GunClass(string bulletTy, int dmg, int mag, float shoot, float reloadSpd): base(bulletTy, dmg, mag) {
        shootingTime = shoot;
        reloadSpeed = reloadSpd;
    }
}
