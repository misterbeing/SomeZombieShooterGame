using System.Collections;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;


    public float rotationSpeed = 0.1f;

    private CharacterController cc;

    [SerializeField] private CharacterStateMachine characterStateMachine;
    [SerializeField] private PlayerZombieList playerZombieList;

    public bool MovingForward;

    private void OnEnable()
    {
        GameManager.characterController = this;
    }
    private void Start()
    {
        cc = GetComponent<CharacterController>();

    }
    public void _Move(Vector3 direction)
    {
        cc.Move(direction * speed * Time.deltaTime);
        characterStateMachine.ChangeState(IsMovingTowardFacingDirection(direction));
        if(playerZombieList.closestZombie != null) LookTowards(playerZombieList.closestZombie.transform.position); 
    }


    public void LookTowards(Vector3 lookTowards)
    {
        if (lookTowards == null) return;

        Vector3 direction = (lookTowards - transform.position).normalized;
        direction.y = 0f; // Keep the character upright

        if(direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }


    CharacterStates IsMovingTowardFacingDirection(Vector3 moveDirection, float threshold = 0.5f)
    {
        if (moveDirection.sqrMagnitude < 0.001f)
            return CharacterStates.idle;

        Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        float dot = Vector3.Dot(moveDirection.normalized, flatForward);

        if (dot > threshold)
        {
            return CharacterStates.walkForward;
            Debug.Log("Moving Forward");
        }
        else if (dot < -threshold)
        {
            return CharacterStates.walkBackward;
            Debug.Log("Moving Backward");
        }
        else
        {
            Vector3 flatRight = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
            float dotRight = Vector3.Dot(moveDirection.normalized, flatRight);

            if (dotRight > 0f)
            {
                return CharacterStates.walkRight;
                Debug.Log("Moving Right");
            }
            else
            {
                return CharacterStates.walkLeft;
                Debug.Log("Moving Left");
            }
        }

        return CharacterStates.idle;
    }
}
