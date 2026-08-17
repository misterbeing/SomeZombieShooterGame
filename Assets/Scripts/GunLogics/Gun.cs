using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public int damage;
    public Transform rayPoint;
    public float fireRate;
    public float range;
    public RaycastHit hit;
    private void OnEnable()
    {
        GameManager.weaponHandler.onGunChanged?.Invoke(this);
    }
    public virtual void Shoot()
    {
        Debug.Log(gunName + " fired!");
        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawLine(rayPoint.position, hit.point, Color.red, 1f);
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.GotHit(true);
            }
        }
    }
}
