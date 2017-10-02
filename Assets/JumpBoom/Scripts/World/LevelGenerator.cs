using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Transform top;
    public float spawnTo;

    public float nextPlatform = -10;
    public float platformStep = 5;

    public GameObject blockGroupPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        while (top.transform.position.y + spawnTo > nextPlatform)
        {
            //Spawn level
            Debug.Log("Build Layer");
            for (int i = -25; i < 25; i += 5)
            {
                Instantiate(blockGroupPrefab, new Vector3(i, nextPlatform, 0), Quaternion.identity);
            }
            nextPlatform += platformStep;
        }
	}
}
