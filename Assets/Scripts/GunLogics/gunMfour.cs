using UnityEngine;

public class gunMfour : Gun
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem particleSystem;

    public override void Shoot()
    {
        base.Shoot();
        lineRenderer.SetPosition(0, rayPoint.position);
        if(hit.collider == null) lineRenderer.SetPosition(1, rayPoint.position + rayPoint.forward * range);
        else lineRenderer.SetPosition(1, hit.point);
        particleSystem.Play();
    }
}
