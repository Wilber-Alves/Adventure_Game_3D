using EDGEE.Core.Singleton;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public SOFloat totalCoins;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        if (totalCoins != null)
            totalCoins.valueFloat = 0;
    }

    public void AddCoins(float amount = 0.5f)
    {
        if (totalCoins != null)
        {
            totalCoins.valueFloat += amount;
            Debug.Log("Colected Coin! Total: " + totalCoins.valueFloat);
        }
    }
}