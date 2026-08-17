using System;
using System.Collections.Generic;
using UnityEngine;

public class playerProximityCensor : MonoBehaviour
{
    private List<Zombie> zombiesInProximity = new List<Zombie>();
    private List<float> damageInflicts = new List<float>();
    public int zombieCount => zombiesInProximity.Count;

    public Action<bool,List<float>> zombiesAttacking;

    [SerializeField] private GameObject playerMesh;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            if (!zombiesInProximity.Contains(zombie))
            {
                zombiesInProximity.Add(zombie);
                damageInflicts.Add(zombie.damageInflicts);
            }
            TriggerAttackSequenceFromZombies(zombiesInProximity, true);
            zombiesAttacking.Invoke(true, damageInflicts);
            //playerMesh.layer = 8;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            playerMesh.layer = 8;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Zombie>(out var zombie))
        {
            TriggerAttackSequenceFromZombies(zombiesInProximity, false);
            if (zombiesInProximity.Contains(zombie))
            {
                zombiesInProximity.Remove(zombie); 
                damageInflicts.Remove(zombie.damageInflicts);
            }
            zombiesAttacking.Invoke(false, damageInflicts);
            playerMesh.layer = 0;
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
