using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCheckText : MonoBehaviour {

    public PlayerReadyCheck readyCheck;

    public Text player0Ready;
    public Text player1Ready;

    public string notReadyString;
    public string readyString;

	// Update is called once per frame
	void Update ()
    {

        if (player0Ready && player1Ready)
        {
            player0Ready.text = readyCheck.player0Ready ? readyString : notReadyString;
            player1Ready.text = readyCheck.player1Ready ? readyString : notReadyString;
        }
        else
        {
            Destroy(this);
        }
    }
}
