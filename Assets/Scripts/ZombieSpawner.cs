using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie[] zombiePrefab;
    public List<Zombie> zombies;
    public Transform playertransform;
    public Transform[] spawnPoints;
    public int spawnCount;
    public float spawnInterval;
    public int zombieSpawnlevel;

    private void OnEnable()
    {
        GameManager.zombieSpawner = this;
        GameManager.onAllZombiesDead += ResetZombies;
    }

    private void OnDisable()
    {
        GameManager.onAllZombiesDead -= ResetZombies;
    }
    private void Update()
    {
        foreach (Zombie zombie in zombies)
        {
            NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                if(zombie.allowMovement) agent.SetDestination(playertransform.position);
                else agent.ResetPath();
            }
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    public IEnumerator SpawnZombies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint =spawnPoints[Random.Range(0, spawnPoints.Length)];
            for(int j = 0; j < zombieSpawnlevel; j++)
            {
                var randomIndex = Random.Range(0, zombieSpawnlevel);
                var zombie = Instantiate(zombiePrefab[randomIndex], spawnPoint.position, Quaternion.identity);
                zombie.playerTransform = playertransform;
                zombies.Add(zombie);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void RemoveZombie(Zombie zombie)
    {
        if (zombies.Contains(zombie))
        {
            zombies.Remove(zombie);
            CheckIfAllZombiesAreDead();
        }
    }

    public void CheckIfAllZombiesAreDead()
    {
        if (zombies.Count == 0)
        {
            Debug.Log("All zombies are dead!");
            GameManager.onAllZombiesDead?.Invoke();
        }
    }

    public void ResetZombies()
    {
        Debug.Log("Resetting zombies!");

        // Capture previous spawnCount so we can detect how many 20-count thresholds were crossed.
        int previousCount = spawnCount;

        // Update spawn count (existing behavior)
        spawnCount = spawnCount * 2;

        // Determine how many full 20-sized thresholds existed before and after the change.
        int previousThresholds = previousCount / 20;
        int newThresholds = spawnCount / 20;

        // If we crossed one or more multiples of 20, increase zombieSpawnlevel accordingly.
        if (newThresholds > previousThresholds)
        {
            zombieSpawnlevel += (newThresholds - previousThresholds);
        }

        StartCoroutine(SpawnZombies());
    }
}
