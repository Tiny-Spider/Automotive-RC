﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinCollect : MonoBehaviour {

    public List<GameObject> coins;
    int totalCoins;

	void Awake () {
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            coins.Add(coin);
        }
        totalCoins = coins.Count;
	}
	
	void Update () {
	
	}
}