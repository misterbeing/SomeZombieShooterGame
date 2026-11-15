using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private List<Coin> collectedCoins = new List<Coin>();
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attachDistance = 0.1f;
    public int totalCollectedCoins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coin>() && !collectedCoins.Contains(other.GetComponent<Coin>()))
        {
            Coin coin = other.GetComponent<Coin>();
            collectedCoins.Add(coin);
            StartCoroutine(CollectCoin(coin));
        }
    }

    private IEnumerator CollectCoin(Coin coin)
    {
        while (coin != null && Vector3.Distance(coin.transform.position, transform.position) > attachDistance)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position, speed * Time.deltaTime);
            yield return null;
        }

        if (coin != null)
        {
            totalCollectedCoins += coin.value;
            collectedCoins.Remove(coin);
            Destroy(coin.gameObject);
        }
    }
}
