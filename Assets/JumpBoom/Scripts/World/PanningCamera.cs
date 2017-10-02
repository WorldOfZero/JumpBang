using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningCamera : MonoBehaviour {

    public float speed;
    public float fasterSpeed;

    public float maxDistance;
    public Transform tracked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (tracked.position.y - this.transform.position.y > maxDistance)
        {
            this.transform.position += Vector3.up * fasterSpeed * Time.deltaTime;
            //this.transform.position = new Vector3(this.transform.position.x, tracked.position.y - maxDistance, this.transform.position.z);
        }
        else
        {
            this.transform.position += Vector3.up * speed * Time.deltaTime;
        }
	}
}
