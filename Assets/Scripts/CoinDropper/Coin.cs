using UnityEngine;

public class Coin : MonoBehaviour
{
    public coinType type;
    public int value;
    [SerializeField] private Color gold,silver, bronze;
    [SerializeField] private Renderer coinRenderer;

    public void SetCoinProperty(coinType type, int value)
    {
        this.type = type;
        this.value = value;
        switch (type)
        {
            case coinType.Bronze:
                coinRenderer.material.color = bronze;
                break;
            case coinType.Silver:
                coinRenderer.material.color = silver;
                break;
            case coinType.Gold:
                coinRenderer.material.color = gold;
                break;
        }
    }
}
