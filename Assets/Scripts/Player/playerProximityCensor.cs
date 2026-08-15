using System.Collections.Generic;
using UnityEngine;

public class playerProximityCensor : MonoBehaviour
{
    [SerializeField] private List<Zombie> zombiesInProximity = new List<Zombie>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            zombiesInProximity.Add(zombie);
            TriggerAttackSequenceFromZombies(zombiesInProximity, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            TriggerAttackSequenceFromZombies(zombiesInProximity, false);
            zombiesInProximity.Remove(zombie);
        }
    }

    private void TriggerAttackSequenceFromZombies(List<Zombie> zombies,bool val)
    {
        foreach (var zombie in zombies)
        {
            zombie.AttackPlayer(val);
        }
    }
}
