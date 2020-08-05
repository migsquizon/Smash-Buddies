using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coin {get;set;}
    public TextMeshPro coinText;

    // Start is called before the first frame update
    void Start()
    {
        coin = 1000;
        coinText.SetText(coin.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        coinText.SetText(coin.ToString());
    }

    public void BuyCoin(int price)
    {
        Debug.Log("Buy Coin");
        coin -= price;
    }
}
