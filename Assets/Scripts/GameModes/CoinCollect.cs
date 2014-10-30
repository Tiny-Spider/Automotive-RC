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

    public override void InitializePlayers()
    {
        
    }

    public void GameOver()
    {
        isPlaying = false;
        CalculateResults();
    }

    private void CalculateResults()
    {
        finishedPlayers.Clear();
        int maxValue;
        NetworkPlayer playerToAdd;
        int tempCount = coinCounter.Count;
        for (int i = 0; i < tempCount; i++)
        {
            maxValue = -1;
            playerToAdd = carList[0];
            bool check = false;
            foreach (NetworkPlayer player in coinCounter.Keys)
            {
                if (coinCounter[player] > maxValue)
                {
                    check = true;
                    maxValue = coinCounter[player];
                    playerToAdd = player;
                }
            }
            if (!check)
            {
                Debug.LogError("No MAX value was found in for loop Nr." + i);
            }
            finishedPlayers.Add(playerToAdd);
        }
    }

    void checkScore()
    {

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
