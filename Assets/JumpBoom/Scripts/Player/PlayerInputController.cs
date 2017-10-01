using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    public int playerId;

    public float x;
    public float y;
    public bool dropBomb;
    public bool explodeBomb;

    private Player player;

    // Use this for initialization
    void Start ()
    {
        player = ReInput.players.GetPlayer(playerId);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        x = player.GetAxis("Horizontal");
        y = player.GetAxis("Vertical");
        dropBomb = player.GetButtonDown("DropBomb");
        explodeBomb = player.GetButtonDown("ExplodeBomb");
    }
}
