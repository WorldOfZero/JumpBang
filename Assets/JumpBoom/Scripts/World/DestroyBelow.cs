using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBelow : MonoBehaviour {

    public float belowPadding;
    public float destroyForce = 10;
    public float horizontalMax = 25;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var block in GameObject.FindGameObjectsWithTag("Level"))
        {
            if (block.transform.position.y < this.transform.position.y - belowPadding)
            {
                var body = block.GetComponent<Rigidbody2D>();
                if (body != null && body.bodyType != RigidbodyType2D.Dynamic)
                {
                    body.bodyType = RigidbodyType2D.Dynamic;
                    body.AddForce(Random.insideUnitCircle * destroyForce);
                }
            }
        }
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.transform.position.y < this.transform.position.y - belowPadding ||
                Mathf.Abs(player.transform.position.x) > horizontalMax)
            {
                Destroy(player.gameObject);
            }
        }
	}
}
