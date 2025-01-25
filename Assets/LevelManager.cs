using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;

    PlayerInputManager inputManager;
    Level currentLevel;

    List<GameObject> activeBubbles = new List<GameObject>(), activeSpikes = new List<GameObject>();

    Timer timer;
    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        timer = GetComponent<Timer>();
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
    }

    private void Start()
    {
        SpawnPlayers();
    }
    private void SpawnPlayers()
    {
        int bubbleCount = 0;
        int spikeCount = 0;
        foreach (var player in SessionManager.players)
        {
            inputManager.playerPrefab = player.prefab;
            var newPlayer =  inputManager.JoinPlayer(player.playerIndex, -1, player.controlScheme, player.devices);
            if (player.prefab.tag == "Bubble")
            {
                activeBubbles.Add(newPlayer.gameObject);
                newPlayer.transform.position = currentLevel.bubbleSpawns[bubbleCount].position;
                bubbleCount++;
            }
            else
            {
                activeSpikes.Add(newPlayer.gameObject);
                newPlayer.transform.position = currentLevel.spikeSpawns[spikeCount].position;
                spikeCount++;
            }
        }
    }
    private void DespawnPlayers()
    {
        foreach(PlayerInput player in FindObjectsOfType<PlayerInput>())
        {
            Destroy(player.gameObject);
        }
    }
    private void Update()
    {
        //if(activeBubbles.Count == 0 || activeSpikes.Count == 0)
        //{
        //    LevelOver();
        //}
    }

    private void LevelOver()
    {
        DespawnPlayers();
        Destroy(currentLevel.gameObject);
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
    }

    private void LoadLevel(Level level)
    {
        currentLevel = level;
        timer.SetTime(currentLevel.timeLimit);
        timer.StartTimer();
        SpawnPlayers();
    }
}
