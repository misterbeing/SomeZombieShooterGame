using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float playerHealth;
    [SerializeField] private playerProximityCensor playerProximityCensor;
    public List<float> totaDamageInflicts;
    [SerializeField] private float totalDamageimflicted;


    private void OnEnable()
    {
        playerProximityCensor.zombiesAttacking += GetAttackingZombiesDamage;
    }

    private void OnDisable()
    {
        playerProximityCensor.zombiesAttacking -= GetAttackingZombiesDamage;
    }
    public void GetAttackingZombiesDamage(bool value, List<Zombie> zombies)
    {
        if (!value)
        {
            totaDamageInflicts.Clear();
            //return;
        }
        foreach (var zombie in zombies)
        {
            totaDamageInflicts.Add(zombie.damageInflicts);
            float totalDamage = 0;
            foreach (var damage in totaDamageInflicts)
            {
                totalDamage += damage;
                totalDamageimflicted = totalDamage;
                StartCoroutine(StartLosingHealth(value, totalDamage));
            }

        }
    }

    public IEnumerator StartLosingHealth(bool value, float loseHealthValue)
    {
        while (value)
        {
            playerHealth -= loseHealthValue * playerProximityCensor.zombieCount;
            yield return new WaitForSeconds(1f);
        }
    }
}