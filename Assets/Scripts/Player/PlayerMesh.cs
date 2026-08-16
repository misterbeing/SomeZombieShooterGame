using UnityEngine;
public class PlayerMesh : MonoBehaviour
{
    [SerializeField] private RaycastHit raycastResult;
    [SerializeField] private GameObject playermesh;
    [SerializeField] private bool raycastHitPlayer;
    [SerializeField] private LayerMask layertoIgnore;

    
    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, out raycastResult, Mathf.Infinity,layertoIgnore))
        {
            if(raycastResult.collider.gameObject.GetComponent<CharacterController>())
            {
                playermesh.layer = 0;
                raycastHitPlayer = true;
            }

            else
            {
                playermesh.layer = 7;
                raycastHitPlayer = false;
            }

            Debug.DrawLine(transform.position, raycastResult.point, Color.red);
            Debug.DrawRay(transform.position, transform.forward * 6, Color.green);
        }
    }
}
