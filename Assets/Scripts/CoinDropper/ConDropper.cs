using UnityEngine;

public class ConDropper : MonoBehaviour
{   public GameObject coinPrefab;
    public int dropCount = 3; 
    public float dropRadius = 2f;

    [Header("Dropped Coin Value")]
    public int coinValue = 1;
    public coinType coinType = coinType.Bronze;
    public void DropCoin()
    {
        for (int i = 0; i < dropCount; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            Vector3 spawnPosition = transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * dropRadius;

            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            Coin coinScript = coin.GetComponent<Coin>();
            if (coinScript != null)
            {
                coinScript.SetCoinProperty(coinType, coinValue);
            }
        }
        Debug.Log("Coins  dropped!");
    }
}
