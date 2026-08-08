using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMesh : MonoBehaviour
{
    [SerializeField] private RaycastHit raycastResult;
    [SerializeField] private GameObject playermesh;


    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out raycastResult))
        {
            if(raycastResult.collider.gameObject.GetComponent<CharacterController>())
            {
                playermesh.layer = 0;
            }

            else
            {
                playermesh.layer = 7;
            }

            Debug.DrawLine(transform.position, raycastResult.point, Color.red);
        }
    }
}
