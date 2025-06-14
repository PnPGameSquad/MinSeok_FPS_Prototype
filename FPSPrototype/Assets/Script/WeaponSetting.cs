using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum WeaponName { AssultRifle = 0 }
 [System.Serializable]
public struct WeaponSetting
{
    public WeaponName   weaponName;
    public int          damage;
    public int          currentMagazine;
    public int          maxMagazine;
    public int          currentAmmo;
    public int          maxAmmo;
    public float        attackRate;
    public float        attackDistance;
    public bool         isAutomaticAttack;
}
