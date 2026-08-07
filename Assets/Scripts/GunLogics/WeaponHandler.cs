using System;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Gun currentGun;

    public Action<Gun> onGunChanged;

    private void OnEnable()
    {
        GameManager.weaponHandler = this;
        onGunChanged += ChangeGun;
    }

    private void OnDisable()
    {
        onGunChanged -= ChangeGun;
    }
    public void ChangeGun(Gun newGun)
    {
        currentGun = newGun;
    }
}
