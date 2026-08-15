using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;
    public List<Zombie> zombies;
    public Transform playertransform;
    public Transform[] spawnPoints;
    public int spawnCount;
    public float spawnInterval;

    private void OnEnable()
    {
        GameManager.zombieSpawner = this;
    }
    private void Update()
    {
        foreach (Zombie zombie in zombies)
        {
            NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                if(zombie.allowMovement) agent.SetDestination(playertransform.position);
            }
            //zombie.transform.LookAt(playertransform);
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

            var zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
            zombie.playerTransform = playertransform;
            zombies.Add(zombie);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void RemoveZombie(Zombie zombie)
    {
        if (zombies.Contains(zombie))
        {
            zombies.Remove(zombie);
        }
    }
}
