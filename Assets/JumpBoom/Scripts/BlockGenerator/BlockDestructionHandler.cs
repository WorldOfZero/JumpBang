using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestructionHandler : MonoBehaviour {

    public GameObject blockPrefab;

    private int[,] spawnPositionType = new int[,] {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1}
        };
    private List<int[,]> blockTypes = new List<int[,]>
    {
        new int[,] {
            {0}
        },
        new int[,] {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,2,0,0},
        },
        new int[,] {
            {1,1,1,1,1},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,2,0,0},
        },
        new int[,] {
            {1,1,1,1,1}
        },
        new int[,] {
            {1,0,0,0,1}
        },
        new int[,] {
            {1,1,1,1,1},
            {0,0,1,0,0},
            {2,0,1,0,2},
            {0,0,1,0,0},
            {1,1,1,1,1}
        },
        new int[,] {
            {1,1,1,1,1},
            {1,1,1,1,1},
            {1,1,1,1,1},
            {1,1,1,1,1},
            {1,1,1,1,1}
        },
        new int[,] {
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0},
            {0,0,1,0,0}
        },
        new int[,] {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,2,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1}
        }
    };

    public bool isSpawn = false;

	// Use this for initialization
	void Start () {
        int[,] block = blockTypes[Random.Range(0, blockTypes.Count)];
        if (isSpawn)
        {
            block = spawnPositionType;
        }
        for (int y = 0; y < block.GetLength(0); y++)
        {
            for (int x = 0; x < block.GetLength(1); x++)
            {
                switch (block[y, x]) {
                    default:
                    case 0: break;
                    case 1:
                        var spawned = Instantiate(blockPrefab, this.transform.position + Vector3.right * x + Vector3.down * y, Quaternion.identity);
                        break;
                    case 2:
                        GameController.AddSpawnPoint(this.transform.position + Vector3.right * x + Vector3.down * y);
                        break;
                }
            }
        }
        Destroy(this.gameObject);
	}
}
