using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CoinCollect : FreeMode {

    public List<GameObject> coins;
    public List<NetworkPlayer> finishedPlayers;
    public Dictionary<NetworkPlayer, int> coinCounter;
    public int totalCoins;
    public float timeLimit;
    public float totalTime;

    bool isPlaying;

	public override void Awake () {
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            coins.Add(coin);
            Coin tempCoin = coin.AddComponent<Coin>();
            tempCoin.coinCollect = this;
        }

        totalCoins = coins.Count;
	}

    public override void Start()
    {
        isPlaying = true;
    }

    public override void Update()
    {
        time += Time.deltaTime;
        if (totalTime >= timeLimit && isPlaying)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isPlaying = false;
        for (int i = 0; i < coinCounter.Count; ) 
        {
            coinCounter.Values.Max();
        }
    }
}

public class Coin : MonoBehaviour
{
    public CoinCollect coinCollect;
    void OnTriggerEnter(Collider other)
    {
        NetworkPlayer player = other.gameObject.networkView.owner;
        coinCollect.coinCounter[player]++;
        coinCollect.totalCoins--;
        if (coinCollect.totalCoins <= 0)
        {
            coinCollect.GameOver();
        }
    }
}
