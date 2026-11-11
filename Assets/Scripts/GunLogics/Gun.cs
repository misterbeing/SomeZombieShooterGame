using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public int damage;
    public Transform rayPoint;
    public float fireRate;
    public float range;
    public RaycastHit hit;

    [Header("Raycast All Properties")]
    public bool useRaycastAll;
    public RaycastHit[] hits;
    private void Start()
    {
        GameManager.weaponHandler.onGunChanged?.Invoke(this);
    }
    public virtual void Shoot()
    {
        Debug.Log(gunName + " fired!");
        if(!useRaycastAll)
        {
            if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, range))
            {
                Debug.Log("Hit: " + hit.collider.name);
                Debug.DrawLine(rayPoint.position, hit.point, Color.red, 1f);
                Zombie zombie = hit.collider.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.GotHit(true, damage);
                }
                return;
            }
        }
        if (useRaycastAll)
        {
            hits = Physics.RaycastAll( rayPoint.position,rayPoint.forward,range);

            foreach (RaycastHit hit in hits)
            {
                Debug.DrawLine(rayPoint.position, hit.point, Color.red, 1f);

                if (hit.collider.TryGetComponent<Zombie>(out Zombie zombie))
                {
                    zombie.GotHit(true, damage);
                }
            }
            return;
        }
    }
}
