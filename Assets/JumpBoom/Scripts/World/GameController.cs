using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public int player0Lives = 3;
    public int player1Lives = 3;

    public static int Player0Lives { get { return instance.player0Lives; } }
    public static int Player1Lives { get { return instance.player1Lives; } }

    public GameObject playerPrefab;

    public GameObject player0;
    public GameObject player1;

    public Color p0Color;
    public Color p1Color;

    public bool isPlaying = false;

    private LinkedList<Vector3> spawnPoints = new LinkedList<Vector3>();

    public SmartCamera smartCamera;
    public LayerMask spawnPointMask;

    public Transform levelBottom;
    public float bottomPadding;

    public static int lastVictory = -1;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            if (player0 == null)
            {
                player0 = SpawnPlayer(0, p0Color);
                player0Lives -= 1;
                if (player0Lives <= 0)
                {
                    GameController.lastVictory = 1;
                    DisableAllExplosions();
                    SceneManager.LoadScene(0);
                }
            }
            if (player1 == null)
            {
                player1 = SpawnPlayer(1, p1Color);
                player1Lives -= 1;
                if (player1Lives <= 0)
                {
                    GameController.lastVictory = 0;
                    DisableAllExplosions();
                    SceneManager.LoadScene(0);
                }
            }
        }

        var deletedSpawnPoints = new LinkedList<Vector3>();
        foreach (var spawn in spawnPoints)
        {
            if (!ValidSpawn(spawn))
            {
                deletedSpawnPoints.AddLast(spawn);
            }
        }
        foreach (var deleted in deletedSpawnPoints)
        {
            spawnPoints.Remove(deleted);
        }

    }

    private void DisableAllExplosions()
    {
        foreach (var effect in GameObject.FindObjectsOfType<DeathEffect>())
        {
            effect.DisableParticles();
        }
    }

    private bool ValidSpawn(Vector3 spawn)
    {
        return Physics2D.Raycast(spawn, Vector2.down, 5, spawnPointMask) && spawn.y > levelBottom.position.y + bottomPadding;
    }

    private GameObject SpawnPlayer(int playerIndex, Color playerColor)
    {
        var spawnPoint = GetSpawnPoint();
        var player = CreatePlayerAtSpawn(spawnPoint, playerIndex, playerColor);
        return player;
    }

    private Vector3 GetSpawnPoint()
    {
        if (spawnPoints.Count == 0)
        {
            return new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
        var spawn = spawnPoints.OrderBy(point => point.y).First();
        spawnPoints.Remove(spawn);
        return spawn;
    }

    private GameObject CreatePlayerAtSpawn(Vector3 spawnPoint, int playerIndex, Color playerColor)
    {
        var player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        var playerTheme = player.GetComponent<PlayerTheme>();
        playerTheme.playerTheme = playerColor;
        var playerInput = player.GetComponent<PlayerInputController>();
        playerInput.playerId = playerIndex;
        return player;
    }

    internal static void AddSpawnPoint(Vector3 spawnPoint)
    {
        instance.spawnPoints.AddLast(spawnPoint);
    }

    public static void CreateGame(Color p1Color, Color p2Color)
    {
        ResetPlayerLives();

        instance.isPlaying = true;
        instance.p0Color = p1Color;
        instance.p1Color = p2Color;
        instance.smartCamera.followedPoints = new Transform[0];

        instance.player0 = instance.SpawnPlayer(0, instance.p0Color);
        instance.player1 = instance.SpawnPlayer(1, instance.p1Color);
}

    private static void ResetPlayerLives()
    {
        instance.player0Lives = 3;
        instance.player1Lives = 3;
    }
}
