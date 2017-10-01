using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestructionHandler : MonoBehaviour {

    public GameObject blockPrefab;
    private bool[,] block = new bool[,] {
        {true, true, true, true }
    };

	// Use this for initialization
	void Start () {
        for (int y = 0; y < block.GetLength(0); y++)
        {
            for (int x = 0; x < block.GetLength(1); x++)
            {
                if (block[y, x])
                {
                    var spawned = Instantiate(blockPrefab, this.transform.position + Vector3.right * x + Vector3.down * y, Quaternion.identity);
                    spawned.transform.parent = this.transform;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int y = 0; y < block.GetLength(0); y++)
        {
            for (int x = 0; x < block.GetLength(1); x++)
            {
                if (block[y, x])
                {
                    var spawnPoint = this.transform.position + Vector3.right * x + Vector3.down * y;
                    Gizmos.DrawWireCube(spawnPoint, Vector3.one);
                }
            }
        }
    }
}
