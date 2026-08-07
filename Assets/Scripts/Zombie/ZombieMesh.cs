using UnityEngine;

public class ZombieMesh : MonoBehaviour
{
    [SerializeField] private Zombie zombie;

    private void OnBecameVisible()
    {
      GameManager.onZombieEnteringPlayersView?.Invoke(zombie);
    }

    private void OnBecameInvisible()
    {
        GameManager.onZombieExitingPlayersView?.Invoke(zombie);
    }
}
