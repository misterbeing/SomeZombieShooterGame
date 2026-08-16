using System;
using System.Collections.Generic;
using UnityEngine;

public class playerProximityCensor : MonoBehaviour
{
    public List<Zombie> zombiesInProximity = new List<Zombie>();
    public int zombieCount => zombiesInProximity.Count;

    public Action<bool,List<Zombie>> zombiesAttacking;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            zombiesInProximity.Add(zombie);
            TriggerAttackSequenceFromZombies(zombiesInProximity, true);
            zombiesAttacking.Invoke(true, zombiesInProximity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            TriggerAttackSequenceFromZombies(zombiesInProximity, false);
            zombiesInProximity.Remove(zombie);
            zombiesAttacking.Invoke(false, zombiesInProximity);
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
