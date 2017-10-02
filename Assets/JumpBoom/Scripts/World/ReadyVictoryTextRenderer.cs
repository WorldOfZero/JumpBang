using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyVictoryTextRenderer : MonoBehaviour {

    public Text text;

    public string defaultWelcome = "Ready Up";
    public string lastVictoryPlayer0 = "Player 1 Wins";
    public string lastVictoryPlayer1 = "Player 2 Wins";

    // Use this for initialization
    void Start () {
        switch (GameController.lastVictory) {
            default:
            case -1:
                text.text = defaultWelcome;
                break;
            case 0:
                text.text = lastVictoryPlayer0;
                break;
            case 1:
                text.text = lastVictoryPlayer1;
                break;
        }
	}
}
