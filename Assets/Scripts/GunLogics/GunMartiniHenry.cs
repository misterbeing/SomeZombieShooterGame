using System.Collections;
using UnityEngine;

public class GunMartiniHenry : Gun
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject lineRendererGameObject;
    [SerializeField] private Transform lineRendererTransform;
    public override void Shoot()
    {
        
        base.Shoot(); 
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.DrawLine(rayPoint.position, hits[i].point, Color.red, 1f);
            lineRenderer.SetPosition(0, lineRendererTransform.position);
            lineRenderer.SetPosition(1, hits[i].point);
        }
        StartCoroutine(AppearDissappearLine());
    }

    public IEnumerator AppearDissappearLine()
    {
        lineRendererGameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lineRendererGameObject.SetActive(false);
        yield return null;
    }
}
