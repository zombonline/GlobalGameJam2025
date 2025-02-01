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

    List<GameObject> activePlayerObjects = new List<GameObject>();
    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        LoadLevel(Instantiate(levels[Random.Range(0, levels.Length)]));
    }
    private void OnEnable()
    {
        timer.onTimerComplete += () => StartCoroutine(LevelOver(Team.Bubble));
        Health.onDeath += CheckIfTeamEliminated;
    }
    private void OnDisable()
    {
        timer.onTimerComplete -= () => StartCoroutine(LevelOver(Team.Bubble));
        Health.onDeath -= CheckIfTeamEliminated;
    }
    private void SpawnPlayers()
    {
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
            newPlayer.transform.position = currentLevel.GetRandomSpawnPosition(player.team);
            activePlayerObjects.Add(newPlayer.gameObject);
        }
    }
    private void DespawnPlayers()
    {
        foreach(GameObject player in activePlayerObjects)
        {
            Destroy(player.gameObject);
        }
        activePlayerObjects.Clear();
    }
    /// <summary>
    /// Checks if the team that the object that died belonged to is eliminated. If so, the level is over. 
    /// </summary>
    /// <param name="tag"></param>
    private void CheckIfTeamEliminated(string tag)
    {
        if (GameObject.FindGameObjectsWithTag(tag).Length > 1) { return; }
        StartCoroutine(LevelOver(tag == "Spike" ? Team.Bubble : Team.Spike));
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
        Debug.Log("anim event called");
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
        foreach(var h in activePlayerObjects)
        {
            h.GetComponent<Health>().SetCanTakeDamage(false);
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
        timer.SetTime(currentLevel.GetTimeLimit());
        timer.StartTimer();
        SpawnPlayers();
        StartCoroutine(CountdownToLevelBegin());
    }

    public IEnumerator CountdownToLevelBegin()
    {
        timer.PauseTimer();
        foreach(var p in activePlayerObjects)
        {
            p.GetComponent<PlayerMovement>().SetCanMove(false);
        }
        yield return new WaitForSeconds(1f);
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
        foreach (var p in activePlayerObjects)
        {
            p.GetComponent<PlayerMovement>().SetCanMove(true);
        }
        timer.ResumeTimer();
    }
}
