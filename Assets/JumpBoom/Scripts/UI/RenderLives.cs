using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RenderLives : MonoBehaviour {

    public Text text;
    public string livesFormat = "P1: {0}";
    public int playerId = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < (playerId == 0 ? GameController.Player0Lives : GameController.Player1Lives); ++i)
        {
            builder.Append("*");
        }
        text.text = string.Format(livesFormat, builder.ToString());
	}
}
