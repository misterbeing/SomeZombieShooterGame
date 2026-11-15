using UnityEngine;
using UnityEngine.InputSystem;
using zombieGame;

public class InputManager : MonoBehaviour
{
    public InputActionProperty moveInput;
    public InputActionProperty mousePosition;
    [SerializeField]private CharacterControllerScript characterController;
    private PlayerInputAction playerinput;

    private void OnEnable()
    {
        characterController = GameManager.characterController;
        moveInput.action.Enable();
        playerinput = new PlayerInputAction();

        playerinput.Enable();
    }
    private void OnDisable()
    {
        moveInput.action.Disable();
        playerinput.Disable();
    }
    private void Update()
    {
        MoveInput();
        //LookInput();
        if (playerinput.PlayerMovement.PrimaryFire.WasPressedThisFrame()) FireInput();
        if (Mouse.current.rightButton.wasPressedThisFrame) SecondaryFireInput();
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
       GameManager.weaponHandler.currentGun?.Shoot();
       Debug.Log("Firing gun: " + GameManager.weaponHandler.currentGun?.gunName);
    }

    public void SecondaryFireInput()
    {
       Debug.Log("Secondary fire input detected.");
       // Implement secondary fire logic here
       GameManager.grenadelauncher.LaunchGrenade();
    }
        
}
