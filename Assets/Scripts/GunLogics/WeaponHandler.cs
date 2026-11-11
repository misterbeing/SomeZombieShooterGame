using System;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Gun currentGun;

    public Action<Gun> onGunChanged;

    private void Awake()
    {
        GameManager.weaponHandler = this;
    }
    private void OnEnable()
    { 
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
