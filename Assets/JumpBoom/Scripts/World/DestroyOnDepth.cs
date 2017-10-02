﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDepth : MonoBehaviour {

    public float depth = -10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < depth)
        {
            Destroy(this.gameObject);
        }
	}
}
