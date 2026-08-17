using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float playerTotalHealth,playerCurrenthealth;
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
    public void GetAttackingZombiesDamage(bool value, List<float> zombies)
    {
        if (!value)
        {
            totaDamageInflicts.Clear();
            totalDamageimflicted = 0;
        }
        totaDamageInflicts = zombies;
        totalDamageimflicted = zombies.Sum();

        if(value)StartCoroutine(StartLosingHealth(value, totalDamageimflicted));
        if(!value) StopAllCoroutines();
    }

    public IEnumerator StartLosingHealth(bool value, float loseHealthValue)
    {
        while (value)
        {
            playerCurrenthealth -= loseHealthValue ;
            GameManager.onUpdateHealth?.Invoke(playerCurrenthealth,playerTotalHealth);
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("losing health: " + value + " " + value);
        //StopCoroutine(StartLosingHealth(value, loseHealthValue));
    }
}