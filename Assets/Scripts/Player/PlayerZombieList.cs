using System.Collections.Generic;
using UnityEngine;

public class PlayerZombieList : MonoBehaviour
{
    [SerializeField] private List<Zombie> zombiesInView = new List<Zombie>();
    public Zombie closestZombie;
    private void OnEnable()
    {
        GameManager.onZombieEnteringPlayersView += AddZombie;
        GameManager.onZombieExitingPlayersView += RemoveZombie;
    }

    private void OnDisable()
    {
        GameManager.onZombieEnteringPlayersView -= AddZombie;
        GameManager.onZombieExitingPlayersView -= RemoveZombie;
    }
    public void AddZombie(Zombie zombie)
    {
        if (!zombiesInView.Contains(zombie))
        {
            zombiesInView.Add(zombie);
        }
    }

    public void RemoveZombie(Zombie zombie)
    {
        if (zombiesInView.Contains(zombie))
        {
            zombiesInView.Remove(zombie);
        }
    }
    private void Update()
    {
        UpdateClosestZombie(this.transform);
        //this.transform.LookAt(closestZombie.transform);
    }
    public void UpdateClosestZombie(Transform playerTransform)
    {
        float closestDistance = Mathf.Infinity;
        closestZombie = null;
        foreach (var zombie in zombiesInView)
        {
            float distance = Vector3.Distance(playerTransform.position, zombie.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestZombie = zombie;
            }
        }
    }
}