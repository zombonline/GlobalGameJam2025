using Spine.Unity;
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
    [SerializeField] Timer timer;
    [SerializeField] WinCounter winCounterSpike, winCounterBubble;

    [SerializeField] SkeletonGraphic levelTransition, countdown;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        winCounterBubble.SetMax(SessionManager.GetRequiredWins());
        winCounterSpike.SetMax(SessionManager.GetRequiredWins());
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
        FMODController.UpdateState(1);
    }
    private void OnEnable()
    {
        timer.onTimerComplete += LevelTimeUp;
    }
    private void OnDisable()
    {
        timer.onTimerComplete -= LevelTimeUp;
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
            if (player.team == Team.Bubble)
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
        //checks if this object that died was the last 1 as this is called before rhe obnject is destroyed
        if (GameObject.FindGameObjectsWithTag(tag).Length == 1)
        {
            StartCoroutine(LevelOver(tag == "Spike" ? Team.Bubble : Team.Spike));
        }
    }
    private void LevelTimeUp()
    {
        StartCoroutine(LevelOver(Team.Bubble));
    }
    private IEnumerator LevelOver(Team winningTeam)
    {
        if (currentLevel.levelComplete) { yield break; }
        currentLevel.levelComplete = true;
        SessionManager.AddWin(winningTeam);
        winCounterSpike.UpdateCounter(SessionManager.GetWins(Team.Spike));
        winCounterBubble.UpdateCounter(SessionManager.GetWins(Team.Bubble));
        TogglePlayerHealth();
        PlayerWinLoseAnimations(winningTeam);
        yield return new WaitForSeconds(3);
        levelTransition.freeze = false;
        levelTransition.AnimationState.ClearTracks();
        levelTransition.AnimationState.SetAnimation(0, "animation", false);
        yield return new WaitForSpineEvent(levelTransition.AnimationState, "Scene Change");
        if (SessionManager.GetWins(winningTeam) >= SessionManager.GetRequiredWins())
        {
            levelTransition.freeze = true;
            SceneManager.LoadScene("Game Over");
            yield break;
        }
        DespawnPlayers();
        Destroy(currentLevel.gameObject);
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
    }
    private void TogglePlayerHealth()
    {
        foreach(var h in FindObjectsByType<Health>(FindObjectsSortMode.None))
        {
            h.SetCanTakeDamage(false);
        }
    }

    private void PlayerWinLoseAnimations(Team winningTeam)
    {
        foreach(var p in FindObjectsByType<SpineAnimator>(FindObjectsSortMode.None))
        {
            p.UnlockState();
            if (p.tag == winningTeam.ToString())
            {
                p.PlayWinFace();
            }
            else
            {
                p.PlayLoseFace();
            }
            p.LockState();
        }
    }
    private void LoadLevel(Level level)
    {
        currentLevel = level;

        timer.SetTime(currentLevel.timeLimit);
        Debug.Log("Timer set to " + currentLevel.timeLimit);
        timer.StartTimer();
        SpawnPlayers();
        StartCoroutine(CountdownToLevelBegin());
    }

    public IEnumerator CountdownToLevelBegin()
    {
        timer.PauseTimer();
        foreach(var p in FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None))
        {
            p.SetCanMove(false);
        }
        countdown.gameObject.SetActive(true);
        countdown.Skeleton.SetSkin("Ready");
        countdown.AnimationState.SetAnimation(0, "animation", false);
        yield return new WaitForSeconds(2f);
        countdown.AnimationState.ClearTracks();
        countdown.Skeleton.SetSkin("Go!");
        countdown.AnimationState.SetAnimation(0, "animation", false);
        yield return new WaitForSeconds(1f);
        countdown.AnimationState.ClearTracks();
        countdown.gameObject.SetActive(false);
        foreach (var p in FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None))
        {
            p.SetCanMove(true);
        }
        timer.ResumeTimer();
    }
}
