using UnityEngine;

[System.Serializable]
public class GrenadeClass : WeaponClass {
    public float impactForce;

    public GrenadeClass(string bulletTy, int dmg, int mag, GameObject sprite, float impact) : base(bulletTy, dmg, mag, sprite) {
        impactForce = impact;
    }
}
