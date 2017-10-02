using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyCheck : MonoBehaviour {

    public bool isReady = false;
    public PlayerInputController player0;
    public PlayerInputController player1;

    public bool player0Ready;
    public bool player1Ready;

    public Color player0Color;
    public Color player1Color;

    public GameObject[] hideOnGameStart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        player0Ready ^= player0.dropBomb;
        player1Ready ^= player1.dropBomb;
        isReady = player0Ready && player1Ready;

        if (isReady)
        {
            SpawnPlayers();
            HideUI();
            Destroy(this);
        }
    }

    private void HideUI()
    {
        foreach (var gobj in hideOnGameStart)
        {
            Destroy(gobj);
        }
    }

    private void SpawnPlayers()
    {
        GameController.CreateGame(player0Color, player1Color);
    }
}
