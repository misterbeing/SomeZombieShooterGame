using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionProperty moveInput;
    public InputActionProperty mousePosition;
    [SerializeField]private CharacterControllerScript characterController;

    private void OnEnable()
    {
        characterController = GameManager.characterController;
        moveInput.action.Enable();
    }
    private void OnDisable() => moveInput.action.Disable();

    private void Update()
    {
        MoveInput();
        //LookInput();
        FireInput();
    }

    public void MoveInput()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(input.x, 0, input.y);
        characterController._Move(moveDir.normalized);
    }

    public void LookInput()
    {
        Vector2 screenPos = mousePosition.action.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // Raycast against a virtual ground plane at player's Y
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            //characterController.LookTowards(worldPoint);
        }
    }

    public void FireInput()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameManager.weaponHandler.currentGun?.Shoot();
            Debug.Log("Firing gun: " + GameManager.weaponHandler.currentGun?.gunName);
        }
    }
        
}
