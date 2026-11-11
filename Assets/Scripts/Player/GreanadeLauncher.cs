using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreanadeLauncher : MonoBehaviour
{
    [SerializeField] private Transform grenadeOrigin;
    [SerializeField] private Rigidbody grenadePrefab; // assign a Rigidbody prefab
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float blastRadius = 5f;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private ParticleSystem blastEffect;
    [SerializeField] private float damage;
    [SerializeField] private float blastDuration;
    [SerializeField] private float launchDuration;

    [SerializeField] private bool showBlastGizmo = false;
    [SerializeField] private bool canLaunch;

    public float relaodTime;

    private void OnEnable()
    {
        GameManager.grenadelauncher = this;
    }

    [ContextMenu("Launch Grenade")]
    public void LaunchGrenade()
    {
        if (!canLaunch) return;
        grenadePrefab.transform.parent = null; // Detach from the launcher
        grenadePrefab.gameObject.transform.position = grenadeOrigin.position;
        grenadePrefab.gameObject.SetActive(true);
        grenadePrefab.AddForceAtPosition(grenadeOrigin.forward * launchForce, grenadeOrigin.position, ForceMode.Impulse);
        StartCoroutine(Explode(launchDuration));
        StartCoroutine(GetGrenadeReadyTimer());
    }

    [ContextMenu("Reset Grenade")]
    public void ResetGrenade()
    {
        grenadePrefab.gameObject.transform.position = Vector3.zero;
        grenadePrefab.gameObject.SetActive(false);
        grenadePrefab.transform.parent = grenadeOrigin; // Reattach to the launcher
        grenadePrefab.linearVelocity = Vector3.zero; // Reset velocity
        grenadePrefab.angularVelocity = Vector3.zero; // Reset angular velocity
    }

    public IEnumerator Explode(float delay)
    {
        canLaunch = false;
        yield return new WaitForSeconds(delay);
        // Play blast effect
        if (blastEffect != null)
        {
            blastEffect.Play();
        }
        targets.Clear();
        // Detect targets within the blast radius
        Collider[] hitColliders = Physics.OverlapSphere(grenadePrefab.position, blastRadius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.TryGetComponent<Zombie>(out Zombie _zombie))
            {
                Debug.Log($"Target {hitCollider.name} hit by grenade!");
                // Apply damage or effects to the target here
                _zombie.GotHit(true, damage);
                targets.Add(_zombie.gameObject);
            }
        }

        yield return new WaitForSeconds(blastDuration);
        // Reset the grenade after explosion
        ResetGrenade();
        canLaunch = true;
    }

    public IEnumerator GetGrenadeReadyTimer()
    {
        float totalReloadTime = blastDuration + launchDuration;
        relaodTime = blastDuration + launchDuration;
        while (relaodTime > 0)
        {
            yield return null;
            relaodTime -= Time.deltaTime;

            float valueInZeroToOne = Mathf.InverseLerp(0f, totalReloadTime, relaodTime);
            GameManager.grenadeRelaodTime?.Invoke(valueInZeroToOne);
        }
        relaodTime = 0;
        GameManager.grenadeRelaodTime?.Invoke(0f);
        yield break;
    }

    private void OnDrawGizmosSelected()
    {
        if (!showBlastGizmo) return;
        if (grenadePrefab == null)return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grenadePrefab.position,blastRadius);
    }
}
