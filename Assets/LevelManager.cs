using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;
    PlayerInputManager inputManager;
    Level currentLevel;
    Timer timer;
    [SerializeField] ScoreDisplay scoreDisplay;
    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        timer = GetComponent<Timer>();
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
    }
    private void OnEnable()
    {
        timer.onTimerComplete += LevelTimeUp;
    }
    private void OnDisable()
    {
        timer.onTimerComplete -= LevelTimeUp;
    }
    private void Start()
    {
        SpawnPlayers();
    }
    private void SpawnPlayers()
    {
        int bubbleCount = 0;
        int spikeCount = 0;
        foreach (var player in SessionManager.GetPlayers())
        {
            bool playerExists = false;
            foreach (PlayerInput p in FindObjectsByType<PlayerInput>(FindObjectsSortMode.None))
            {
                if (p.playerIndex == player.playerIndex)
                {
                    playerExists = true;
                }
            }
            if (playerExists)
            {
                continue;
            }
            inputManager.playerPrefab = player.prefab;
            var newPlayer = inputManager.JoinPlayer(player.playerIndex, -1, player.controlScheme, player.devices);
            newPlayer.GetComponent<Health>().onDeath += HandlePlayerDeath;
            if (player.prefab.tag == "Bubble")
            {
                newPlayer.transform.position = currentLevel.bubbleSpawns[bubbleCount].position;
                bubbleCount++;
            }
            else
            {
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
    private void HandlePlayerDeath(GameObject player)
    {
        CheckRemainingPlayersInLevelByTag(player.tag);
    }

    private void CheckRemainingPlayersInLevelByTag(string tag)
    {
        Debug.Log("Checking remaining players with tag " + tag);
        //checks if this onbject that died was the last 1 as this is called before rhe obnject is destroyed
        if (GameObject.FindGameObjectsWithTag(tag).Length == 1)
        {
            if (tag == "Bubble")
            {
                SessionManager.AddSpikeWin();
            }
            else
            {
                SessionManager.AddBubbleWin();
            }
            LevelOver();
        }
    }

    private void LevelTimeUp()
    {
        SessionManager.AddBubbleWin();
        LevelOver();
    }

    private void LevelOver()
    {
        scoreDisplay.UpdateDisplay();
        DespawnPlayers();
        Destroy(currentLevel.gameObject);
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
    }

    private void LoadLevel(Level level)
    {
        currentLevel = level;
        timer.SetTime(currentLevel.timeLimit);
        Debug.Log("Timer set to " + currentLevel.timeLimit);
        timer.StartTimer();
        SpawnPlayers();
    }
}
